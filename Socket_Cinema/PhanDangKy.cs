using Socket_Cinema;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Socket_Cinema
{
    public partial class PhanDangKy : Form
    {
        private const string ServerIP = "127.0.0.1";
        private const int ServerPort = 8080;

        public PhanDangKy()
        {
            InitializeComponent();
        }

        private async Task<string> SendAndReceiveAsync(string message)
        {
            using TcpClient client = new TcpClient();
            await client.ConnectAsync(ServerIP, ServerPort);
            using NetworkStream stream = client.GetStream();
            using StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
            using StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            await writer.WriteLineAsync(message);
            string response = await reader.ReadLineAsync();
            return response?.Trim() ?? "";
        }

        // ================================= //
        // 🔹 Nút "Đăng ký"
        // ================================= //
        private async void NutDangKy_Click(object sender, EventArgs e)
        {
            string hoTen = HoTen.Text.Trim();
            string username = Username.Text.Trim();
            string password = MatKhau.Text;
            string confirm = XacNhanMatKhau.Text;
            string email = Email.Text.Trim();
            string sdt = SoDienThoai.Text.Trim();
            DateTime ngaySinh = NgayThangNamSinh.Value.Date;
            string khuVuc = KhuVuc.SelectedItem?.ToString() ?? "Chưa có";

            // ⚙️ Gọi hàm kiểm tra hợp lệ trong AuthHelper
            string error = AuthHelper.ValidateInput(hoTen, username, password, confirm, email, sdt, ngaySinh);
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "Lỗi nhập liệu");
                return;
            }

            try
            {
                string passwordHash = AuthHelper.ToSha256(password);
                string request = $"REGISTER|{username}|{passwordHash}|{email}|{hoTen}|{ngaySinh:yyyy-MM-dd}|{sdt}|{khuVuc}";

                string response = await SendAndReceiveAsync(request);

                if (response == "REGISTER_OK")
                {
                    MessageBox.Show("Đăng ký thành công! Hãy đăng nhập.", "Thành công");
                    PhanDangNhap form = new PhanDangNhap();
                    this.Hide();
                    form.Show();
                    form.FormClosed += (s, args) => this.Close();
                }
                else if (response.StartsWith("REGISTER_FAIL"))
                {
                    string msg = response.Split('|').Length > 1 ? response.Split('|')[1] : "Lỗi không xác định.";
                    MessageBox.Show($"Đăng ký thất bại: {msg}", "Lỗi Đăng ký");
                }
                else
                {
                    MessageBox.Show("Lỗi giao tiếp với Server hoặc phản hồi không xác định.", "Lỗi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi");
            }
        }

        // ================================= //
        // 🔹 Link "Đăng nhập"
        // ================================= //
        private void NutDangNhap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PhanDangNhap form = new PhanDangNhap();
            this.Hide();
            form.Show();
            form.FormClosed += (s, args) => this.Close();
        }
    }
}
