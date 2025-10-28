namespace Socket_Cinema
{
    partial class PhanDangKy
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PhanDangKy));
            NutDangKy = new Button();
            label1 = new Label();
            NutDangNHap = new LinkLabel();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            HoTen = new TextBox();
            SoDienThoai = new TextBox();
            Email = new TextBox();
            Username = new TextBox();
            MatKhau = new TextBox();
            KhuVuc = new ComboBox();
            label9 = new Label();
            XacNhanMatKhau = new TextBox();
            label10 = new Label();
            NgayThangNamSinh = new DateTimePicker();
            SuspendLayout();
            // 
            // NutDangKy
            // 
            NutDangKy.BackColor = Color.Red;
            NutDangKy.Font = new Font("Arial", 19.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            NutDangKy.ForeColor = SystemColors.Window;
            NutDangKy.Location = new Point(691, 978);
            NutDangKy.Margin = new Padding(3, 2, 3, 2);
            NutDangKy.Name = "NutDangKy";
            NutDangKy.Size = new Size(290, 85);
            NutDangKy.TabIndex = 0;
            NutDangKy.Text = "ĐĂNG KÝ";
            NutDangKy.UseVisualStyleBackColor = false;
            NutDangKy.Click += NutDangKy_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ActiveCaptionText;
            label1.Font = new Font("Arial", 22.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Gold;
            label1.Location = new Point(779, 56);
            label1.Name = "label1";
            label1.Size = new Size(488, 51);
            label1.TabIndex = 1;
            label1.Text = "ĐĂNG KÝ TÀI KHOẢN";
            // 
            // NutDangNHap
            // 
            NutDangNHap.ActiveLinkColor = SystemColors.WindowText;
            NutDangNHap.AutoSize = true;
            NutDangNHap.BackColor = SystemColors.WindowText;
            NutDangNHap.Font = new Font("Arial", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            NutDangNHap.ForeColor = SystemColors.Window;
            NutDangNHap.LinkColor = Color.White;
            NutDangNHap.Location = new Point(1270, 1012);
            NutDangNHap.Name = "NutDangNHap";
            NutDangNHap.Size = new Size(126, 26);
            NutDangNHap.TabIndex = 2;
            NutDangNHap.TabStop = true;
            NutDangNHap.Text = "Đăng Nhập";
            NutDangNHap.VisitedLinkColor = Color.Transparent;
            NutDangNHap.LinkClicked += NutDangNhap_LinkClicked;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.WindowText;
            label2.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.HighlightText;
            label2.Location = new Point(659, 194);
            label2.Name = "label2";
            label2.Size = new Size(128, 29);
            label2.TabIndex = 3;
            label2.Text = "Họ và tên:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.WindowText;
            label3.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.Window;
            label3.Location = new Point(659, 276);
            label3.Name = "label3";
            label3.Size = new Size(264, 29);
            label3.TabIndex = 3;
            label3.Text = "Ngày/tháng/năm sinh:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.WindowText;
            label4.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.Window;
            label4.Location = new Point(659, 370);
            label4.Name = "label4";
            label4.Size = new Size(173, 29);
            label4.TabIndex = 3;
            label4.Text = "Số điện thoại:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.WindowText;
            label5.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = SystemColors.Window;
            label5.Location = new Point(659, 470);
            label5.Name = "label5";
            label5.Size = new Size(84, 29);
            label5.TabIndex = 3;
            label5.Text = "Email:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = SystemColors.WindowText;
            label6.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = SystemColors.Window;
            label6.Location = new Point(662, 564);
            label6.Name = "label6";
            label6.Size = new Size(117, 29);
            label6.TabIndex = 3;
            label6.Text = "Khu vực:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = SystemColors.WindowText;
            label7.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = SystemColors.Window;
            label7.Location = new Point(659, 761);
            label7.Name = "label7";
            label7.Size = new Size(125, 29);
            label7.TabIndex = 3;
            label7.Text = "Mật khẩu:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = SystemColors.WindowText;
            label8.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = SystemColors.Window;
            label8.Location = new Point(659, 856);
            label8.Name = "label8";
            label8.Size = new Size(273, 29);
            label8.TabIndex = 3;
            label8.Text = "Xác nhận lại mật khẩu:";
            // 
            // HoTen
            // 
            HoTen.BackColor = SystemColors.WindowText;
            HoTen.Font = new Font("Arial", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            HoTen.ForeColor = SystemColors.Window;
            HoTen.Location = new Point(953, 176);
            HoTen.Margin = new Padding(3, 5, 3, 5);
            HoTen.Multiline = true;
            HoTen.Name = "HoTen";
            HoTen.Size = new Size(442, 53);
            HoTen.TabIndex = 4;

            // 
            // SoDienThoai
            // 
            SoDienThoai.BackColor = SystemColors.WindowText;
            SoDienThoai.Font = new Font("Arial", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SoDienThoai.ForeColor = SystemColors.Window;
            SoDienThoai.Location = new Point(953, 352);
            SoDienThoai.Margin = new Padding(3, 5, 3, 5);
            SoDienThoai.Multiline = true;
            SoDienThoai.Name = "SoDienThoai";
            SoDienThoai.Size = new Size(442, 53);
            SoDienThoai.TabIndex = 4;
            // 
            // Email
            // 
            Email.BackColor = SystemColors.WindowText;
            Email.Font = new Font("Arial", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Email.ForeColor = SystemColors.Window;
            Email.Location = new Point(953, 452);
            Email.Margin = new Padding(3, 5, 3, 5);
            Email.Multiline = true;
            Email.Name = "Email";
            Email.Size = new Size(442, 53);
            Email.TabIndex = 4;
            // 
            // Username
            // 
            Username.BackColor = SystemColors.WindowText;
            Username.Font = new Font("Arial", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Username.ForeColor = SystemColors.Window;
            Username.Location = new Point(953, 644);
            Username.Margin = new Padding(3, 5, 3, 5);
            Username.Multiline = true;
            Username.Name = "Username";
            Username.Size = new Size(442, 53);
            Username.TabIndex = 4;
            // 
            // MatKhau
            // 
            MatKhau.BackColor = SystemColors.WindowText;
            MatKhau.Font = new Font("Arial", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MatKhau.ForeColor = SystemColors.Window;
            MatKhau.Location = new Point(953, 744);
            MatKhau.Margin = new Padding(3, 5, 3, 5);
            MatKhau.Multiline = true;
            MatKhau.Name = "MatKhau";
            MatKhau.PasswordChar = '*';
            MatKhau.Size = new Size(442, 53);
            MatKhau.TabIndex = 4;
            // 
            // KhuVuc
            // 
            KhuVuc.BackColor = SystemColors.WindowText;
            KhuVuc.Font = new Font("Arial", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            KhuVuc.ForeColor = SystemColors.Window;
            KhuVuc.FormattingEnabled = true;
            KhuVuc.Items.AddRange(new object[] { "Thành Phố Hà Nội\t", "Thành Phố Huế", "Quảng Ninh", "Cao Bằng", "Lạng Sơn", "Lai Châu", "Điện Biên", "Sơn La", "Thanh Hóa", "Nghệ An", "Hà Tĩnh\t", "Tuyên Quang", "Lào Cai", "Thái Nguyên", "Phú Thọ\t", "Bắc Ninh", "Hưng Yên", "Thành Phố Hải Phòng", "Ninh Bình", "Quảng Trị", "Thành Phố Đà Nẵng", "Quảng Ngãi", "Gia Lai", "Khánh Hòa\t", "Lâm Đồng\t", "Đắk Lắk\t", "Thành Phố Hồ Chí Minh", "Đồng Nai\t", "Tây Ninh", "Thành Phố Cần Thơ", "Vĩnh Long", "Đồng Tháp", "Cà Mau", "An Giang" });
            KhuVuc.Location = new Point(953, 564);
            KhuVuc.Margin = new Padding(3, 5, 3, 5);
            KhuVuc.Name = "KhuVuc";
            KhuVuc.Size = new Size(442, 33);
            KhuVuc.TabIndex = 5;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = SystemColors.WindowText;
            label9.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = SystemColors.Window;
            label9.Location = new Point(659, 661);
            label9.Name = "label9";
            label9.Size = new Size(187, 29);
            label9.TabIndex = 3;
            label9.Text = "Tên Tài Khoản:";
            // 
            // XacNhanMatKhau
            // 
            XacNhanMatKhau.BackColor = SystemColors.WindowText;
            XacNhanMatKhau.Font = new Font("Arial", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            XacNhanMatKhau.ForeColor = SystemColors.Window;
            XacNhanMatKhau.Location = new Point(953, 839);
            XacNhanMatKhau.Margin = new Padding(3, 5, 3, 5);
            XacNhanMatKhau.Multiline = true;
            XacNhanMatKhau.Name = "XacNhanMatKhau";
            XacNhanMatKhau.PasswordChar = '*';
            XacNhanMatKhau.Size = new Size(442, 53);
            XacNhanMatKhau.TabIndex = 4;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = SystemColors.WindowText;
            label10.Font = new Font("Arial", 10.2F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label10.ForeColor = SystemColors.Window;
            label10.Location = new Point(1037, 1014);
            label10.Name = "label10";
            label10.Size = new Size(212, 24);
            label10.TabIndex = 6;
            label10.Text = "Bạn đã có tài khoản?";
            // 
            // NgayThangNamSinh
            // 
            NgayThangNamSinh.CalendarForeColor = SystemColors.InfoText;
            NgayThangNamSinh.CalendarMonthBackground = SystemColors.InactiveCaptionText;
            NgayThangNamSinh.CalendarTitleBackColor = SystemColors.WindowText;
            NgayThangNamSinh.CalendarTitleForeColor = SystemColors.MenuBar;
            NgayThangNamSinh.CustomFormat = "";
            NgayThangNamSinh.Font = new Font("Arial", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            NgayThangNamSinh.Format = DateTimePickerFormat.Short;
            NgayThangNamSinh.Location = new Point(953, 278);
            NgayThangNamSinh.Margin = new Padding(3, 5, 3, 5);
            NgayThangNamSinh.Name = "NgayThangNamSinh";
            NgayThangNamSinh.Size = new Size(442, 32);
            NgayThangNamSinh.TabIndex = 7;

            // 
            // PhanDangKy
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1477, 1126);
            Controls.Add(NgayThangNamSinh);
            Controls.Add(label10);
            Controls.Add(KhuVuc);
            Controls.Add(XacNhanMatKhau);
            Controls.Add(MatKhau);
            Controls.Add(Username);
            Controls.Add(Email);
            Controls.Add(SoDienThoai);
            Controls.Add(HoTen);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label9);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(NutDangNHap);
            Controls.Add(label1);
            Controls.Add(NutDangKy);
            ForeColor = SystemColors.WindowText;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "PhanDangKy";
            Text = "Đăng ký tài khoản";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button NutDangKy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel NutDangNHap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox HoTen;
        private System.Windows.Forms.TextBox SoDienThoai;
        private System.Windows.Forms.TextBox Email;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.TextBox MatKhau;
        private System.Windows.Forms.ComboBox KhuVuc;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox XacNhanMatKhau;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker NgayThangNamSinh;
    }
}

