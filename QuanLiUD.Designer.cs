using System;
using System.Drawing;
using System.Windows.Forms;

namespace BTLNET
{
    partial class QuanLiUD
    {
        private System.ComponentModel.IContainer components = null;
        private ListView lvInstalledApps;
        private Button btnShowAll;
        private ComboBox cboSizeFilter;
        private ComboBox cboDriveSelect;
        private ComboBox cboSortOrder;
        private Button btnViewHistory;
        private Panel headerPanel;
        private Panel controlPanel;
        private Panel mainPanel;
        private Panel spacerPanel;
        private Label lblTitle;
        private Label lblGuide;
        private Button btnRefresh;
        private Button btnViewLogs;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // Khởi tạo tất cả controls
            this.lvInstalledApps = new ListView();
            this.btnShowAll = new Button();
            this.cboSizeFilter = new ComboBox();
            this.cboDriveSelect = new ComboBox();
            this.cboSortOrder = new ComboBox();
            this.btnViewHistory = new Button();
            this.headerPanel = new Panel();
            this.controlPanel = new Panel();
            this.mainPanel = new Panel();
            this.spacerPanel = new Panel();
            this.lblTitle = new Label();
            this.lblGuide = new Label();
            this.btnRefresh = new Button();

            this.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.controlPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();

            // Thiết lập form
            this.Text = "Ứng dụng đã cài đặt";
            this.Size = new Size(1000, 600);
            this.MinimumSize = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Header Panel
            this.headerPanel.Dock = DockStyle.Top;
            this.headerPanel.Height = 70;
            this.headerPanel.BackColor = Color.FromArgb(45, 45, 48);

            // Label Title
            this.lblTitle.Text = "ỨNG DỤNG ĐÃ CÀI ĐẶT";
            this.lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(20, 20);
            this.headerPanel.Controls.Add(this.lblTitle);

            // Control Panel
            this.controlPanel.Dock = DockStyle.Top;
            this.controlPanel.Height = 60;
            this.controlPanel.BackColor = Color.White;
            this.controlPanel.Padding = new Padding(20, 10, 20, 10);

            // Size Filter ComboBox
            this.cboSizeFilter.Size = new Size(120, 30);
            this.cboSizeFilter.Location = new Point(20, 15);
            this.cboSizeFilter.Font = new Font("Segoe UI", 10);
            this.cboSizeFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboSizeFilter.Items.AddRange(new string[] { "💾 Tất cả", "💾 >100MB", "💾 >500MB", "💾 >1GB" });
            this.cboSizeFilter.SelectedIndex = 0;

            // Drive Select ComboBox
            this.cboDriveSelect.Size = new Size(80, 30);
            this.cboDriveSelect.Location = new Point(150, 15);
            this.cboDriveSelect.Font = new Font("Segoe UI", 10);
            this.cboDriveSelect.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboDriveSelect.Items.AddRange(new string[] { "💿 C:\\", "💿 D:\\" });
            this.cboDriveSelect.SelectedIndex = 0;

            // Sort Order ComboBox
            this.cboSortOrder.Size = new Size(150, 30);
            this.cboSortOrder.Location = new Point(240, 15);
            this.cboSortOrder.Font = new Font("Segoe UI", 10);
            this.cboSortOrder.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboSortOrder.Items.AddRange(new string[] {
                "🔤 Tên A-Z", "🔤 Tên Z-A", "📊 Dung lượng ↑",
                "📊 Dung lượng ↓", "📅 Mới nhất", "📅 Cũ nhất"
            });
            this.cboSortOrder.SelectedIndex = 0;

            // Show All Button
            this.btnShowAll.Size = new Size(130, 35);
            this.btnShowAll.Location = new Point(400, 13);
            this.btnShowAll.Text = "🔍 Hiển thị";
            this.btnShowAll.Font = new Font("Segoe UI", 10);
            this.btnShowAll.BackColor = Color.FromArgb(0, 120, 215);
            this.btnShowAll.ForeColor = Color.White;
            this.btnShowAll.FlatStyle = FlatStyle.Flat;
            this.btnShowAll.FlatAppearance.BorderSize = 0;

            // Refresh Button
            this.btnRefresh.Size = new Size(130, 35);
            this.btnRefresh.Location = new Point(540, 13);
            this.btnRefresh.Text = "🔄 Làm mới";
            this.btnRefresh.Font = new Font("Segoe UI", 10);
            this.btnRefresh.BackColor = Color.FromArgb(76, 175, 80);
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderSize = 0;


            // Spacer Panel
            this.spacerPanel.Dock = DockStyle.Top;
            this.spacerPanel.Height = 20;
            this.spacerPanel.BackColor = Color.FromArgb(240, 240, 240);

            // Main Panel
            this.mainPanel.Dock = DockStyle.Fill;
            this.mainPanel.Padding = new Padding(20);
            this.mainPanel.BackColor = Color.FromArgb(240, 240, 240);

            // ListView
            this.lvInstalledApps.Dock = DockStyle.None;
            this.lvInstalledApps.View = View.Details;
            this.lvInstalledApps.FullRowSelect = true;
            this.lvInstalledApps.GridLines = true;
            this.lvInstalledApps.Location = new Point(20, 0);
            this.lvInstalledApps.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.lvInstalledApps.Font = new Font("Segoe UI", 9);
            this.lvInstalledApps.BackColor = Color.White;

            // Add columns
            this.lvInstalledApps.Columns.Add("Tên ứng dụng", 200);
            this.lvInstalledApps.Columns.Add("Đường dẫn", 350);
            this.lvInstalledApps.Columns.Add("Dung lượng", 100);
            this.lvInstalledApps.Columns.Add("Ngày chỉnh sửa", 120);


            // Add controls to panels
            this.mainPanel.Controls.Add(this.lvInstalledApps);
            this.controlPanel.Controls.AddRange(new Control[] {
                this.cboSizeFilter,
                this.cboDriveSelect,
                this.cboSortOrder,
                this.btnShowAll,
                this.btnRefresh
            });

            // Add panels to form
            this.Controls.AddRange(new Control[] {
                this.mainPanel,
                this.spacerPanel,
                this.controlPanel,
                this.headerPanel
            });

            // Add event handlers
            this.btnShowAll.Click += new EventHandler(this.btnShowAll_Click);
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);
            this.lvInstalledApps.MouseClick += new MouseEventHandler(this.lvInstalledApps_MouseClick);
            this.lvInstalledApps.MouseDoubleClick += new MouseEventHandler(this.lvInstalledApps_DoubleClick);
            this.Resize += new EventHandler(this.Form2_Resize);

            // Resume layouts
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.controlPanel.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            if (this.lvInstalledApps.Columns.Count >= 5)
            {
                int totalWidth = this.lvInstalledApps.ClientSize.Width;
                this.lvInstalledApps.Columns[0].Width = (int)(totalWidth * 0.25);
                this.lvInstalledApps.Columns[1].Width = (int)(totalWidth * 0.35);
                this.lvInstalledApps.Columns[2].Width = (int)(totalWidth * 0.1);
                this.lvInstalledApps.Columns[3].Width = (int)(totalWidth * 0.15);
                this.lvInstalledApps.Columns[4].Width = (int)(totalWidth * 0.15);
            }
        }

        
    }
}