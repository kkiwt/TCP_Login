using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens; 
using System.IdentityModel.Tokens.Jwt; 
using System.Security.Claims; 

namespace Socket_Cinema_Server
{
    
    public partial class ServerTCP : Form   
    {
        private const string DatabaseName = "USERSTCP";
        private TcpListener server;            // Lắng nghe kết nối TCP từ client
        private bool isRunning = false;        
        private string connectionString = $@"Data Source=127.0.0.1;Initial Catalog={DatabaseName};Integrated Security=True;TrustServerCertificate=True";
        private const int ServerPort = 8080; // Cổng cố định
        private readonly db database = new db();




        public ServerTCP()
        {
            InitializeComponent();       
        }

        private async void MoServer_Click(object sender, EventArgs e)
        {
            if (isRunning) return; // Tránh mở lại Server

            try
            {
                // Gọi hàm tạo database và bảng (chạy bất đồng bộ)
                await Header.TaoDatabaseVaBangMoi();


                IPAddress ip = IPAddress.Any;    // Cho phép lắng nghe từ mọi địa chỉ (0.0.0.0)

                // Khởi tạo TcpListener
                server = new TcpListener(ip, ServerPort);
                server.Start();
                isRunning = true;                

                TrangThai.Invoke((MethodInvoker)(() =>
                {
                    TrangThai.Enabled = true;
                    TrangThai.Text = $"Server đang chạy tại cổng {ServerPort}";
                    TrangThai.ForeColor = System.Drawing.Color.DarkGreen;
                }));


                // Dùng Task.Run để chạy hàm lắng nghe Client bất đồng bộ trên luồng nền
                _ = Task.Run(() => LangNgheClientAsync());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở server: {ex.Message}");
            }
        }

        private async Task LangNgheClientAsync() 
        {
            while (isRunning)  // Chạy liên tục cho đến khi server tắt
            {
                try
                {
                    // AcceptTcpClientAsync là non-blocking
                    TcpClient client = await server.AcceptTcpClientAsync();

                    // Khi có client kết nối, tạo 1 Task/luồng riêng để xử lý
                    _ = XuLyClientAsync(client);
                }
                catch (SocketException)
                {
                }
                catch (Exception)
                {
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
                        // Gọi hàm trong chính ServerTCP
                        var (success, userData) = await KiemTraDangNhapAsync(parts[1], parts[2]);
                        response = success ? $"LOGIN_OK|{userData}" : $"LOGIN_FAIL|{userData}";
                    }
                    else if (parts[0] == "REGISTER" && parts.Length == 8)
                    {
                        // Gọi class db qua instance "database"
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
            // ⭐Select tất cả thông tin người dùng
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
                                // Đăng nhập thành công, tạo TOKEN
                                string token = Header.GenerateJwtToken(username);

                                // Lấy thông tin người dùng 
                                string hoTen = reader["HoTen"].ToString();
                                string ngaySinh = reader["NgaySinh"] == DBNull.Value ? "N/A" : ((DateTime)reader["NgaySinh"]).ToString("yyyy-MM-dd"); // ⭐️ Sửa thành "N/A"
                                string sdt = reader["SDT"].ToString();
                                string email = reader["Email"].ToString();
                                string khuVuc = reader["KhuVuc"] == DBNull.Value ? "N/A" : reader["KhuVuc"].ToString(); // ⭐️ Sửa thành "N/A"

                                // Trả về: HoTen|NgaySinh|SDT|Email|KhuVuc|Username|TOKEN
                                string userData = $"{hoTen}|{ngaySinh}|{sdt}|{email}|{khuVuc}|{username}|{token}";
                                return (true, userData);
                            }
                        }
                    }
                }
                return (false, "Tên đăng nhập hoặc mật khẩu không đúng."); 
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi hệ thống Server: {ex.Message}"); //  Trả về lỗi hệ thống
            }
        }


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