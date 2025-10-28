using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Socket_Cinema_Server
{
    class db
    {
        static string DatabaseName = "USERSTCP";
        static string connectionString = $@"Data Source=127.0.0.1;Initial Catalog={DatabaseName};Integrated Security=True;TrustServerCertificate=True";

        public async Task<(bool success, string errorMsg)> DangKyTaiKhoanAsync(
            string username, string passwordHash, string email, string fullName, string birthday, string sdt, string khuVuc)
        {
            if (await CheckUsernameExistsAsync(username))
                return (false, "Tên đăng nhập đã tồn tại.");

            string sql = @"
                INSERT INTO UserClient (Username, MaHashCuaMatKhau, Email, HoTen, SDT, NgaySinh, KhuVuc) 
                VALUES (@u, @p, @e, @h, @s, @n, @k)";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@u", username);
                        cmd.Parameters.AddWithValue("@p", passwordHash);
                        cmd.Parameters.AddWithValue("@e", email);
                        cmd.Parameters.AddWithValue("@h", fullName);
                        cmd.Parameters.AddWithValue("@s", sdt);
                        cmd.Parameters.AddWithValue("@k", khuVuc ?? "Chưa có");

                        if (DateTime.TryParse(birthday, out DateTime dob))
                            cmd.Parameters.AddWithValue("@n", dob);
                        else
                            cmd.Parameters.AddWithValue("@n", DBNull.Value);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi hệ thống: {ex.Message}");
            }
        }
        // 🔹 Hàm kiểm tra Username đã tồn tại (ASYNC)
        public static async Task<bool> CheckUsernameExistsAsync(string username)
        {
            string sqlCheck = "SELECT COUNT(1) FROM UserClient WHERE Username = @Username";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(sqlCheck, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    return (int)await cmd.ExecuteScalarAsync() > 0;
                }
            }
        }
    }
}
