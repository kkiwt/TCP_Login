using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens; // 💡 Cần cho Token (Key)
using System.IdentityModel.Tokens.Jwt; // 💡 Cần cho JWT
using System.Security.Claims; // 💡 Cần cho Claims Identity

namespace Socket_Cinema_Server
{
    
    public partial class ServerTCP : Form    // 💡 partial vì còn phần giao diện (file .Designer)
    {
        private const string DatabaseName = "USERSTCP";
        private TcpListener server;            // 💡 Lắng nghe kết nối TCP từ client
        private bool isRunning = false;        // 💡 Biến cờ để kiểm tra server đang chạy hay không
        // Sửa lại ConnectionString để trỏ đúng vào DB USERS
        private string connectionString = $@"Data Source=127.0.0.1;Initial Catalog={DatabaseName};Integrated Security=True;TrustServerCertificate=True";
        private const int ServerPort = 8080; // Cổng cố định
        private readonly db database = new db();

        // ⭐️ Hàm Hash SHA256 được đưa vào Server để có thể dùng trong các hàm DB


        public ServerTCP()
        {
            InitializeComponent();       // 💡 Khởi tạo các thành phần giao diện (nút, label, ...)
        }

        private async void MoServer_Click(object sender, EventArgs e)
        {
            if (isRunning) return; // ⭐️ Tránh mở lại Server

            try
            {
                // 🧠 Gọi hàm tạo database và bảng (chạy bất đồng bộ)
                await Header.TaoDatabaseVaBangMoi();

                // ⚙️ Thiết lập IP và Port cho server
                IPAddress ip = IPAddress.Any;    // 💡 Cho phép lắng nghe từ mọi địa chỉ (0.0.0.0)

                // ⚙️ Khởi tạo TcpListener
                server = new TcpListener(ip, ServerPort);
                server.Start();
                isRunning = true;                // 💡 Gắn cờ là server đang hoạt động
                                                 // Trong MoServer_Click
                TrangThai.Invoke((MethodInvoker)(() =>
                {
                    TrangThai.Enabled = true;
                    TrangThai.Text = $"Server đang chạy tại cổng {ServerPort}";
                    TrangThai.ForeColor = System.Drawing.Color.DarkGreen;
                }));


                // ⭐️ Dùng Task.Run để chạy hàm lắng nghe Client bất đồng bộ trên luồng nền
                _ = Task.Run(() => LangNgheClientAsync());
            }
            catch (Exception ex)
            {
                // ⚠️ Nếu có lỗi khi mở server, hiển thị thông báo
                MessageBox.Show($"Lỗi khi mở server: {ex.Message}");
            }
        }

        private async Task LangNgheClientAsync() // ⭐️ Sửa lại thành Async
        {
            while (isRunning)  // 💡 Chạy liên tục cho đến khi server tắt
            {
                try
                {
                    // ⚙️ Chờ client kết nối (AcceptTcpClientAsync là non-blocking)
                    TcpClient client = await server.AcceptTcpClientAsync();

                    // 🧵 Khi có client kết nối, tạo 1 Task/luồng riêng để xử lý
                    _ = XuLyClientAsync(client);
                }
                catch (SocketException)
                {
                    // 💡 Nếu server.Stop() được gọi, AcceptTcpClientAsync sẽ lỗi -> thoát vòng lặp
                }
                catch (Exception)
                {
                    // Xử lý các lỗi khác
                }
            }
        }


        private async Task XuLyClientAsync(TcpClient client)
        {
            using NetworkStream stream = client.GetStream();
            using StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            using StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

            try
            {
                while (!reader.EndOfStream)
                {
                    string message = await reader.ReadLineAsync();
                    if (string.IsNullOrEmpty(message)) continue;

                    string[] parts = message.Split('|');
                    string response = "UNKNOWN_COMMAND";

                    if (parts[0] == "LOGIN" && parts.Length == 3)
                    {
                        // ✅ Gọi hàm trong chính ServerTCP
                        var (success, userData) = await KiemTraDangNhapAsync(parts[1], parts[2]);
                        response = success ? $"LOGIN_OK|{userData}" : $"LOGIN_FAIL|{userData}";
                    }
                    else if (parts[0] == "REGISTER" && parts.Length == 8)
                    {
                        // ✅ Gọi class db qua instance "database"
                        var (success, msg) = await database.DangKyTaiKhoanAsync(
                            parts[1], parts[2], parts[3], parts[4], parts[5], parts[6], parts[7]);

                        response = success ? "REGISTER_OK" : $"REGISTER_FAIL|{msg}";
                    }

                    await writer.WriteLineAsync(response);
                }
            }
            catch { }
            finally
            {
                client.Close();
            }
        }


        private async Task<(bool success, string userData)> KiemTraDangNhapAsync(string username, string passwordHash) // ⭐️ Sửa lại Async và trả về UserData
        {
            // ⭐️ Select tất cả thông tin người dùng
            string sql = @"
        SELECT HoTen, NgaySinh, SDT, Email, KhuVuc
        FROM UserClient 
        WHERE Username = @u AND MaHashCuaMatKhau = @p";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@u", username);
                        cmd.Parameters.AddWithValue("@p", passwordHash);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // 1. Đăng nhập thành công, tạo TOKEN
                                string token = Header.GenerateJwtToken(username);

                                // 2. Lấy thông tin người dùng (Thay thế DBNull.Value bằng chuỗi "N/A" để dễ parse ở Client)
                                string hoTen = reader["HoTen"].ToString();
                                string ngaySinh = reader["NgaySinh"] == DBNull.Value ? "N/A" : ((DateTime)reader["NgaySinh"]).ToString("yyyy-MM-dd"); // ⭐️ Sửa thành "N/A"
                                string sdt = reader["SDT"].ToString();
                                string email = reader["Email"].ToString();
                                string khuVuc = reader["KhuVuc"] == DBNull.Value ? "N/A" : reader["KhuVuc"].ToString(); // ⭐️ Sửa thành "N/A"

                                // 3. Giao thức trả về: HoTen|NgaySinh|SDT|Email|KhuVuc|Username|TOKEN
                                string userData = $"{hoTen}|{ngaySinh}|{sdt}|{email}|{khuVuc}|{username}|{token}";
                                return (true, userData);
                            }
                        }
                    }
                }
                return (false, "Tên đăng nhập hoặc mật khẩu không đúng."); // ⭐️ Trả về lỗi cụ thể
            }
            catch (Exception ex)
            {
                // Trả về lỗi hệ thống
                return (false, $"Lỗi hệ thống Server: {ex.Message}"); // ⭐️ Trả về lỗi hệ thống
            }
        }

        // 🔹 Xử lý khi đóng Form Server
        private void ServerTCP_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isRunning)
            {
                // Dừng listener để giải phóng cổng
                server?.Stop();
                isRunning = false;
            }
        }
    }
}