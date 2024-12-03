using System.Drawing;
using System.Windows.Forms;

namespace BTLNET
{
    partial class ChiTietUD
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Khai báo các hằng số ở cấp độ class
        private const int PADDING = 20;
        private const int LABEL_HEIGHT = 25;
        private const int SECTION_SPACING = 15;
        private const int LABEL_LEFT_WIDTH = 120;
        private const int VALUE_WIDTH = 260;
        private const int BUTTON_WIDTH = 140;
        private const int BUTTON_HEIGHT = 35;
        private const int BUTTON_SPACING = 20;

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
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.lblAppName = new System.Windows.Forms.Label();
            this.lblPath = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblLastModified = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listViewHistory = new System.Windows.Forms.ListView();
            this.progressBar = new System.Windows.Forms.ProgressBar();

            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();

            // Form
            this.ClientSize = new System.Drawing.Size(500, 450);

            // Panel chính
            this.panel1.Size = new System.Drawing.Size(480, 430);
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.BackColor = Color.White;

            // Icon
            this.pictureBoxIcon.Location = new System.Drawing.Point(20, 20);
            this.pictureBoxIcon.Size = new System.Drawing.Size(48, 48);
            this.pictureBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;

            // Labels bên trái
            this.label1.Location = new System.Drawing.Point(80, 20);
            this.label1.Size = new System.Drawing.Size(120, 25);
            this.label1.TextAlign = ContentAlignment.MiddleLeft;
            this.label1.Text = "Tên ứng dụng:";

            this.label2.Location = new System.Drawing.Point(80, 45);
            this.label2.Size = new System.Drawing.Size(120, 25);
            this.label2.TextAlign = ContentAlignment.MiddleLeft;
            this.label2.Text = "Đường dẫn:";

            this.label3.Location = new System.Drawing.Point(80, 70);
            this.label3.Size = new System.Drawing.Size(120, 25);
            this.label3.TextAlign = ContentAlignment.MiddleLeft;
            this.label3.Text = "Dung lượng:";

            this.label4.Location = new System.Drawing.Point(80, 95);
            this.label4.Size = new System.Drawing.Size(120, 25);
            this.label4.TextAlign = ContentAlignment.MiddleLeft;
            this.label4.Text = "Sửa đổi lần cuối:";

            // Value labels
            this.lblAppName.Location = new System.Drawing.Point(200, 20);
            this.lblAppName.Size = new System.Drawing.Size(260, 25);
            this.lblAppName.AutoEllipsis = true;

            this.lblPath.Location = new System.Drawing.Point(200, 45);
            this.lblPath.Size = new System.Drawing.Size(260, 25);
            this.lblPath.AutoEllipsis = true;

            this.lblSize.Location = new System.Drawing.Point(200, 70);
            this.lblSize.Size = new System.Drawing.Size(260, 25);

            this.lblLastModified.Location = new System.Drawing.Point(200, 95);
            this.lblLastModified.Size = new System.Drawing.Size(260, 25);

            // Buttons
            this.btnOpen.Location = new System.Drawing.Point(80, 140);
            this.btnOpen.Size = new System.Drawing.Size(140, 35);
            this.btnOpen.FlatStyle = FlatStyle.System;
            this.btnOpen.Text = "🚀 Mở ứng dụng";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);

            this.btnOpenFolder.Location = new System.Drawing.Point(240, 140);
            this.btnOpenFolder.Size = new System.Drawing.Size(140, 35);
            this.btnOpenFolder.FlatStyle = FlatStyle.System;
            this.btnOpenFolder.Text = "📂 Mở thư mục";
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);

            // List view
            this.listViewHistory.Location = new System.Drawing.Point(80, 190);
            this.listViewHistory.Size = new System.Drawing.Size(380, 180);
            this.listViewHistory.View = View.Details;
            this.listViewHistory.FullRowSelect = true;
            this.listViewHistory.GridLines = true;

            // Columns
            this.listViewHistory.Columns.Add("Thời gian", 150);
            this.listViewHistory.Columns.Add("Thao tác", 150);

            // ProgressBar
            this.progressBar.Location = new System.Drawing.Point(80, 380);
            this.progressBar.Size = new System.Drawing.Size(380, 20);
            this.progressBar.Visible = false;

            // Form properties
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Chi tiết ứng dụng";

            // Add controls to panel
            this.Controls.Add(this.panel1);
            this.panel1.Controls.AddRange(new Control[] {
                this.pictureBoxIcon,
                this.label1, this.label2, this.label3, this.label4,
                this.lblAppName, this.lblPath, this.lblSize, this.lblLastModified,
                this.btnOpen, this.btnOpenFolder, this.listViewHistory,
                this.progressBar
            });

            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
        }

        private void SetupListView()
        {
            listViewHistory.View = View.Details;
            listViewHistory.FullRowSelect = true;
            listViewHistory.GridLines = true;
            listViewHistory.Columns.Clear();

            // Thêm 2 cột với độ rộng cố định
            listViewHistory.Columns.Add("⌚ Thời gian", 200);
            listViewHistory.Columns.Add("📝 Thao tác", listViewHistory.Width - 204); // Trừ đi độ rộng cột thời gian và thanh cuộn

            this.Resize += (s, e) => AdjustColumnWidths();
        }

        private void AdjustColumnWidths()
        {
            if (listViewHistory.Columns.Count >= 2)
            {
                // Giữ cột thời gian cố định
                listViewHistory.Columns[0].Width = 200;
                // Điều chỉnh cột thao tác theo độ rộng còn lại
                listViewHistory.Columns[1].Width = listViewHistory.ClientSize.Width - 204;
            }
        }

        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblLastModified;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listViewHistory;
        private System.Windows.Forms.ProgressBar progressBar;

        #endregion
    }
}