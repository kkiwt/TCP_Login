using System;
using System.Net.Sockets; // Cần cho TCP/Socket
using System.Text;         // Cần cho Encoding.UTF8
using System.Threading.Tasks; // Cần cho Async/Await
using System.Windows.Forms;
using System.Security.Cryptography; // Cần cho SHA256
using System.Drawing; // Cần cho Color
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace Socket_Cinema
{

    public partial class PhanDangNhap : Form
    {
        private const string ServerIP = "127.0.0.1"; // IP của Server
        private const int ServerPort = 8080;         // Cổng của Server

        public PhanDangNhap()
        {
            InitializeComponent();
        }

        // =============================================== //
        // 🔹 HÀM GIAO TIẾP VỚI SERVER (ASYNC)             //
        // =============================================== //
        /// <summary>
        /// Kết nối, gửi tin nhắn và nhận phản hồi từ Server qua Socket.
        /// </summary>
        /// <param name="message">Lệnh cần gửi (ví dụ: LOGIN|user|hash)</param>
        /// <returns>Phản hồi từ Server (ví dụ: LOGIN_OK|data|token)</returns>
        private async Task<string> GuiNhanServerAsync(string message)
        {
            try
            {
                using TcpClient client = new TcpClient();
                await client.ConnectAsync(ServerIP, ServerPort);

                using NetworkStream stream = client.GetStream();
                using StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
                using StreamReader reader = new StreamReader(stream, Encoding.UTF8);

                await writer.WriteLineAsync(message); // gửi kèm \n
                string response = await reader.ReadLineAsync(); // đọc đến khi gặp \n

                return response?.Trim() ?? "ERROR|Phản hồi rỗng từ Server";
            }
            catch (Exception ex)
            {
                return $"ERROR|Lỗi kết nối Server: {ex.Message}";
            }
        }



        // 🔹 Hàm Hash SHA256 (Giữ nguyên)
        public static string ToSha256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        // Hàm này không còn được sử dụng vì đã chuyển sang giao tiếp Socket


        private void label1_Click(object sender, EventArgs e)
        {
            // ...
        }


        // =============================================== //
        // 🔹 HÀM XỬ LÝ NÚT ĐĂNG NHẬP (ASYNC)              //
        // =============================================== //
        private async void button1_Click(object sender, EventArgs e)
        {
            // Giả định TenDangNhap và MatKhau là các control TextBox
            string username = TenDangNhap.Text.Trim();
            string matKhauNhap = MatKhau.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(matKhauNhap))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu.", "Lỗi nhập liệu");
                return;
            }

            // 💡 1. Hash mật khẩu (Client hash trước khi gửi)
            string passwordHash = ToSha256(matKhauNhap);

            // 💡 2. Tạo lệnh LOGIN theo giao thức: LOGIN|Username|PasswordHash
            string command = $"LOGIN|{username}|{passwordHash}";

            // 💡 3. Gửi lệnh qua Socket và chờ phản hồi (Async)
            string response = await GuiNhanServerAsync(command);

            if (response.StartsWith("LOGIN_OK"))
            {
                // 💡 4. Phản hồi thành công: LOGIN_OK|HoTen|NgaySinh|SDT|Email|KhuVuc|Username|TOKEN
                string[] parts = response.Split('|');

                // Kiểm tra đủ 8 phần tử: Command (0) + 6 thuộc tính User + Token (7)
                if (parts.Length == 8)
                {
                    // 💡 5. Trích xuất thông tin User và TOKEN
                    UserInfo currentUser = new UserInfo
                    {
                        HoTen = parts[1],
                        // Chuyển NgaySinh từ chuỗi sang DateTime (hoặc DateTime.MinValue nếu lỗi)
                        NgaySinh = DateTime.TryParse(parts[2], out DateTime dob) ? dob : DateTime.MinValue,
                        SDT = parts[3],
                        Email = parts[4],
                        KhuVuc = parts[5],
                        Username = parts[6],
                        AuthToken = parts[7] // ⭐️ Lấy TOKEN từ phần tử cuối cùng
                    };

                    MessageBox.Show($"Đăng nhập thành công! Token đã nhận.", "Thành công");

                    // 💡 6. Chuyển sang giao diện chính
                    GiaoDienSauKhiDaDangNhapHoacDangKyXong GiaoDien = new GiaoDienSauKhiDaDangNhapHoacDangKyXong(currentUser);
                    this.Hide();
                    GiaoDien.Show();
                    GiaoDien.FormClosed += (s, args) => this.Close();
                }
                else
                {
                    MessageBox.Show("Lỗi định dạng phản hồi từ Server. Số lượng thông tin không đúng.", "Lỗi hệ thống");
                }
            }
            else if (response.StartsWith("LOGIN_FAIL"))
            {
                MessageBox.Show("Tên đăng nhập hoặc Mật khẩu không đúng.", "Lỗi Đăng nhập");
            }
            else
            {
                // Xử lý lỗi kết nối hoặc lệnh không xác định
                MessageBox.Show($"Lỗi giao tiếp Server: {response}", "Lỗi hệ thống");
            }
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PhanDangKy DangKy = new PhanDangKy();
            this.Hide();
            DangKy.Show();
            DangKy.FormClosed += (s, args) => this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // ...
        }

        private void PhanDangNhap_Load(object sender, EventArgs e)
        {
            // ...
        }
    }
}