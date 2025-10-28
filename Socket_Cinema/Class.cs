using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace Socket_Cinema
{
    internal class Class1
    {
    }
    public class UserInfo
    {
        public string Username { get; set; }
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public string KhuVuc { get; set; }
        public DateTime NgaySinh { get; set; }
        public string AuthToken { get; set; } // 🔹 JWT Token, dùng để xác thực sau này
    }

    
        public static class AuthHelper
        {
            // ================================= //
            // 🔹 Hàm mã hóa SHA256
            // ================================= //
            public static string ToSha256(string input)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(input);
                    byte[] hashBytes = sha256.ComputeHash(bytes);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }
            }

            // ================================= //
            // 🔹 Hàm kiểm tra hợp lệ các trường
            // ================================= //
            public static string ValidateInput(
                string fullName,
                string username,
                string password,
                string confirmPassword,
                string email,
                string phone,
                DateTime birthday)
            {
                // ⚠️ Kiểm tra trống
                if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(username) ||
                    string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword) ||
                    string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone))
                    return "Vui lòng nhập đầy đủ thông tin.";

                // ⚠️ Họ tên hợp lệ
                if (!Regex.IsMatch(fullName, @"^[a-zA-ZÀ-ỹ\s]+$"))
                    return "Họ tên không hợp lệ (chỉ cho phép chữ).";

                // ⚠️ Số điện thoại hợp lệ
                if (!Regex.IsMatch(phone, @"^(0\d{9}|\+84\d{9})$"))
                    return "Số điện thoại không hợp lệ.";

                // ⚠️ Email hợp lệ
                if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    return "Email không hợp lệ.";

                // ⚠️ Mật khẩu mạnh
                if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$"))
                    return "Mật khẩu phải có ít nhất 8 ký tự, có chữ hoa, chữ thường, số và ký tự đặc biệt.";

                // ⚠️ Xác nhận mật khẩu
                if (password != confirmPassword)
                    return "Xác nhận mật khẩu không trùng khớp.";

                // ⚠️ Ngày sinh hợp lệ
                if (birthday > DateTime.Today)
                    return "Ngày sinh không hợp lệ (Không được là ngày trong tương lai).";

                return ""; // ✅ Hợp lệ
            }
        
    }

}
