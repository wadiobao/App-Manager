using System;
using System.Drawing;
using System.Windows.Forms;

namespace BTLNET
{
    partial class UDDaDung
    {
        private System.ComponentModel.IContainer components = null;
        private ListView lvHistory;
        private Label lblTitle;
        private ProgressBar progressBar;
        private TextBox txtSearch;
        private Button btnSearch;
        private ComboBox cmbFilter;
        private Panel headerPanel;
        private Panel searchPanel;
        private Panel mainPanel;
        private Panel spacerPanel;
        private System.Windows.Forms.Button btnExport;

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
            this.lvHistory = new System.Windows.Forms.ListView();
            this.timeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.appNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pathColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblTitle = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbFilter = new System.Windows.Forms.ComboBox();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.spacerPanel = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.headerPanel.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvHistory
            // 
            this.lvHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvHistory.BackColor = System.Drawing.Color.White;
            this.lvHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.timeColumn,
            this.appNameColumn,
            this.pathColumn});
            this.lvHistory.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lvHistory.FullRowSelect = true;
            this.lvHistory.GridLines = true;
            this.lvHistory.HideSelection = false;
            this.lvHistory.Location = new System.Drawing.Point(20, 0);
            this.lvHistory.Name = "lvHistory";
            this.lvHistory.Size = new System.Drawing.Size(903, 397);
            this.lvHistory.TabIndex = 0;
            this.lvHistory.UseCompatibleStateImageBehavior = false;
            this.lvHistory.View = System.Windows.Forms.View.Details;
            // 
            // timeColumn
            // 
            this.timeColumn.Text = "⌚ Thời gian";
            this.timeColumn.Width = 150;
            // 
            // appNameColumn
            // 
            this.appNameColumn.Text = "📱 Tên ứng dụng";
            this.appNameColumn.Width = 200;
            // 
            // pathColumn
            // 
            this.pathColumn.Text = "📂 Đường dẫn";
            this.pathColumn.Width = 400;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(421, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ỨNG DỤNG ĐÃ DÙNG";
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(0, 550);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(982, 3);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 10;
            this.progressBar.Visible = false;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location = new System.Drawing.Point(20, 15);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(250, 30);
            this.txtSearch.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(460, 15);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 30);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // cmbFilter
            // 
            this.cmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbFilter.Items.AddRange(new object[] {
            "🕒 Tất cả",
            "📅 Hôm nay",
            "📅 7 ngày qua",
            "📅 30 ngày qua"});
            this.cmbFilter.Location = new System.Drawing.Point(290, 15);
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.Size = new System.Drawing.Size(150, 31);
            this.cmbFilter.TabIndex = 1;
            this.cmbFilter.SelectedIndexChanged += new System.EventHandler(this.CmbFilter_SelectedIndexChanged);
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.headerPanel.Controls.Add(this.lblTitle);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(982, 70);
            this.headerPanel.TabIndex = 9;
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.Color.White;
            this.searchPanel.Controls.Add(this.btnExport);
            this.searchPanel.Controls.Add(this.txtSearch);
            this.searchPanel.Controls.Add(this.cmbFilter);
            this.searchPanel.Controls.Add(this.btnSearch);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(0, 70);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.searchPanel.Size = new System.Drawing.Size(982, 60);
            this.searchPanel.TabIndex = 8;
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.mainPanel.Controls.Add(this.lvHistory);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 150);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(20);
            this.mainPanel.Size = new System.Drawing.Size(982, 400);
            this.mainPanel.TabIndex = 6;
            // 
            // spacerPanel
            // 
            this.spacerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.spacerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.spacerPanel.Location = new System.Drawing.Point(0, 130);
            this.spacerPanel.Name = "spacerPanel";
            this.spacerPanel.Size = new System.Drawing.Size(982, 20);
            this.spacerPanel.TabIndex = 7;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = Color.FromArgb(76, 175, 80);
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(566, 15);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(120, 30);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Xuất File";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // UDDaDung
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(982, 553);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.spacerPanel);
            this.Controls.Add(this.searchPanel);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.progressBar);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "UDDaDung";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ứng dụng đã dùng";
            this.Resize += new System.EventHandler(this.UDDaDung_Resize);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void UDDaDung_Resize(object sender, EventArgs e)
        {
            if (this.lvHistory != null && this.lvHistory.Columns.Count >= 3)
            {
                int totalWidth = this.lvHistory.ClientSize.Width;
                this.lvHistory.Columns[0].Width = 150;
                this.lvHistory.Columns[1].Width = 200;
                this.lvHistory.Columns[2].Width = totalWidth - 370;
            }
        }

        private ColumnHeader timeColumn;
        private ColumnHeader appNameColumn;
        private ColumnHeader pathColumn;
    }
}