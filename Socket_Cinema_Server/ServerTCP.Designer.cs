namespace Socket_Cinema_Server
{
    partial class ServerTCP
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MoServer = new Button();
            TrangThai = new Label();
            SuspendLayout();
            // 
            // MoServer
            // 
            MoServer.Location = new Point(248, 36);
            MoServer.Margin = new Padding(2, 2, 2, 2);
            MoServer.Name = "MoServer";
            MoServer.Size = new Size(122, 41);
            MoServer.TabIndex = 0;
            MoServer.Text = "Mở Server";
            MoServer.UseVisualStyleBackColor = true;
            MoServer.Click += MoServer_Click;
            // 
            // TrangThai
            // 
            TrangThai.AutoSize = true;
            TrangThai.Enabled = false;
            TrangThai.Location = new Point(279, 90);
            TrangThai.Margin = new Padding(2, 0, 2, 0);
            TrangThai.Name = "TrangThai";
            TrangThai.Size = new Size(50, 20);
            TrangThai.TabIndex = 1;
            TrangThai.Text = "Bấm nút trên để mở server";
            // 
            // ServerTCP
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(640, 130);
            Controls.Add(TrangThai);
            Controls.Add(MoServer);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(2, 2, 2, 2);
            Name = "ServerTCP";
            Text = "Server";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button MoServer;
        private Label TrangThai;
    }
}
