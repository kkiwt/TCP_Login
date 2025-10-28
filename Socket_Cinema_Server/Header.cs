using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace Socket_Cinema_Server
{
    class Header
    {
        public static string ToSha256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public static string GenerateJwtToken(string username)
        {
            // Tạo Security Key từ Secret
            string JwtSecret = "day-la-khoa-bi-mat-cua-server-hay-thay-the-no-bang-mot-chuoi-dai-hon-va-bao-mat-hon-123456";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //  Định nghĩa Claims (Thông tin User)
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // JWT ID

            };

            //  Tạo Token
            var token = new JwtSecurityToken(

                issuer: "SocketCinemaServer",
                audience: "SocketCinemaClient",
                claims: claims,
                expires: DateTime.Now.AddHours(1), //  Token hết hạn sau 1 giờ
                signingCredentials: credentials);

            // Trả về Token dưới dạng chuỗi
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        static string DatabaseName = "USERSTCP";
        static string connectionString = $@"Data Source=127.0.0.1;Initial Catalog={DatabaseName};Integrated Security=True;TrustServerCertificate=True";
        public static async Task TaoDatabaseVaBangMoi()
        {
            string dbName = DatabaseName;

            // Tạo database nếu chưa có
            string masterConn = @"Data Source=127.0.0.1;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True";
            using (SqlConnection connMaster = new SqlConnection(masterConn))
            {
                await connMaster.OpenAsync();
                string sqlCreateDb = $@"
            IF DB_ID('{dbName}') IS NULL
            BEGIN
                CREATE DATABASE [{dbName}];
            END";
                using (SqlCommand cmd = new SqlCommand(sqlCreateDb, connMaster))
                {
                    await cmd.ExecuteNonQueryAsync();
                }
            }

            // Cập nhật connection string tới database mới
            string connectionString = $@"Data Source=127.0.0.1;Initial Catalog={dbName};Integrated Security=True;TrustServerCertificate=True";

            // Tạo bảng UserClient nếu chưa có
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                string sqlCreateTable = @"
            IF OBJECT_ID('UserClient', 'U') IS NULL
            CREATE TABLE UserClient
            (
                UserID INT PRIMARY KEY IDENTITY(1,1),
                Username NVARCHAR(50) NOT NULL UNIQUE,
                HoTen NVARCHAR(100) NULL,
                NgaySinh DATE NULL,
                SDT VARCHAR(20) NULL,
                Email NVARCHAR(100) NULL,
                KhuVuc NVARCHAR(100) NULL,
                MaHashCuaMatKhau CHAR(64) NOT NULL,
                NgayTaoTaiKhoan DATETIME DEFAULT GETDATE()
            )";
                using (SqlCommand cmd = new SqlCommand(sqlCreateTable, conn))
                {
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
