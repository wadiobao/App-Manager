using System.Windows.Forms;
using System;

namespace BTLNET
{
    partial class TrangChu
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(100, 100);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 120);
            this.button1.TabIndex = 0;
            this.button1.Text = "📱 Ứng dụng đã cài đặt";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.FlatAppearance.BorderSize = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(320, 100);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(200, 120);
            this.button2.TabIndex = 1;
            this.button2.Text = "📋 Lịch sử ứng dụng";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.FlatAppearance.BorderSize = 0;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(540, 100);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(200, 120);
            this.button3.TabIndex = 2;
            this.button3.Text = "⌨️ Lịch sử bàn phím";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            this.button3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.FlatAppearance.BorderSize = 0;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(760, 100);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(200, 120);
            this.button4.TabIndex = 3;
            this.button4.Text = "⚙️ Cài đặt";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            this.button4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.FlatAppearance.BorderSize = 0;
            // 
            // TrangChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Text = "Trang Chủ";
            this.Name = "TrangChu";
            // Header Panel
            System.Windows.Forms.Panel headerPanel = new System.Windows.Forms.Panel();
            headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            headerPanel.Height = 70;
            headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));

            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTitle.Text = "QUẢN LÝ ỨNG DỤNG";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            headerPanel.Controls.Add(lblTitle);

            // Main Panel để chứa các button
            System.Windows.Forms.Panel mainPanel = new System.Windows.Forms.Panel();
            mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            mainPanel.Padding = new System.Windows.Forms.Padding(20);
            mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));

            // Thêm các controls vào form
            this.Controls.Clear();
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                mainPanel,
                headerPanel
            });
            mainPanel.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.button1,
                this.button2,
                this.button3,
                this.button4
            });

            // Khởi tạo tray icon
            InitializeTrayIcon();

            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        System.Windows.Forms.Label lblTitle;

        // Thêm biến thành viên cho NotifyIcon
        private NotifyIcon trayIcon;

        // Thêm phương thức khởi tạo tray icon
        private void InitializeTrayIcon()
        {
            trayIcon = new NotifyIcon();
            trayIcon.Icon = this.Icon;
            trayIcon.Text = "Quản lý ứng dụng";
            trayIcon.Visible = true;

            // Tạo context menu cho tray icon
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add("Mở", null, OnTrayIconOpen);
            menu.Items.Add("Thoát", null, OnTrayIconExit);

            trayIcon.ContextMenuStrip = menu;
            trayIcon.DoubleClick += OnTrayIconDoubleClick;
        }

        // Thêm các phương thức xử lý sự kiện
        private void OnTrayIconOpen(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }


        private void OnTrayIconDoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
    }
}