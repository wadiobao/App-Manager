using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;

namespace BTLNET
{
    public partial class QuanLiUD : Form, ILanguageChangeable
    {
        // Thêm connection string
        private readonly string connectionString = @"Server=LAPTOP-4PEOQ6OV\BAO;Database=master;Trusted_Connection=True;";
        private readonly string databaseName = "app_manager_db";


        // Thêm biến NotifyIcon
        private NotifyIcon trayIcon;


        public QuanLiUD()
        {
            InitializeComponent();
            LanguageChange.LanguageChanged += (s, lang) => ApplyLanguage(lang);
            ApplyLanguage(LanguageChange.CurrentLanguage);
            InitializeDatabase();

        }

        public void ApplyLanguage(string language)
        {
            
            btnShowAll.Text = LanguageChange.GetTranslation(language == "Vietnamese" ?
                "🔍 Hiển thị" : "🔍 Show");
            btnRefresh.Text = LanguageChange.GetTranslation(language == "Vietnamese" ?
                "🔄 Làm mới" : "🔄 Refresh");

            lblTitle.Text = LanguageChange.GetTranslation("InstalledApps").ToUpper();
            this.Text = LanguageChange.GetTranslation("InstalledApps");
            cboSortOrder.Items.Clear();
            cboSizeFilter.Items.Clear();
            if (language == "Vietnamese")
            {
                cboSizeFilter.Items.AddRange(new string[] { "💾 Tất cả", "💾 >100MB", "💾 >500MB", "💾 >1GB" });
                cboSizeFilter.SelectedIndex = 0;

                cboSortOrder.Items.AddRange(new string[] {
                    "🔤 Tên A-Z", "🔤 Tên Z-A", "📊 Dung lượng ↑",
                    "📊 Dung lượng ↓", "📅 Mới nhất", "📅 Cũ nhất"
                    });
                cboSortOrder.SelectedIndex = 0;
            }
            else
            {
                cboSizeFilter.Items.AddRange(new string[] { "💾 All", "💾 >100MB", "💾 >500MB", "💾 >1GB" });
                cboSizeFilter.SelectedIndex = 0;

                cboSortOrder.Items.AddRange(new string[] {
                    "🔤 Name A-Z", "🔤 Name Z-A", "📊 Disk Space ↑",
                    "📊 Disk Space ↓", "📅 Lastest", "📅 Oldest"
                    });
                cboSortOrder.SelectedIndex = 0;
            }
            

            // Cập nhật các column header
            if (lvInstalledApps.Columns.Count >= 4)
            {
                lvInstalledApps.Columns[0].Text = LanguageChange.GetTranslation("AppName");
                lvInstalledApps.Columns[1].Text = LanguageChange.GetTranslation("Path");
                lvInstalledApps.Columns[2].Text = LanguageChange.GetTranslation("DiskSpace");
                lvInstalledApps.Columns[3].Text = LanguageChange.GetTranslation("ModifyDate");
            }
            else
            {
                // Thêm cột nếu chưa đủ
                lvInstalledApps.Columns.Add("AppName", 100, HorizontalAlignment.Left);
                lvInstalledApps.Columns.Add("Path", 200, HorizontalAlignment.Left);
                lvInstalledApps.Columns.Add("DiskSpace", 100, HorizontalAlignment.Left);
                lvInstalledApps.Columns.Add("ModifyDate", 100, HorizontalAlignment.Left);

                // Cập nhật lại tiêu đề cột
                lvInstalledApps.Columns[0].Text = LanguageChange.GetTranslation("AppName");
                lvInstalledApps.Columns[1].Text = LanguageChange.GetTranslation("Path");
                lvInstalledApps.Columns[2].Text = LanguageChange.GetTranslation("DiskSpace");
                lvInstalledApps.Columns[3].Text = LanguageChange.GetTranslation("ModifyDate");
            }
        }

        private void InitializeDatabase()
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Kiểm tra và tạo database nếu chưa tồn tại
                    string checkDb = $@"
                        IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{databaseName}')
                        BEGIN
                            CREATE DATABASE {databaseName}
                        END";

                    using (var command = new SqlCommand(checkDb, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Chuyển đến database vừa tạo
                    connection.ChangeDatabase(databaseName);

                    // Tạo bảng installed_apps
                    string createTableQuery = @"
                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'installed_apps')
                        BEGIN
                            CREATE TABLE installed_apps (
                                id INT IDENTITY(1,1) PRIMARY KEY,
                                app_name NVARCHAR(255) NOT NULL,
                                install_location NVARCHAR(512) NOT NULL,
                                size_mb BIGINT,
                                install_date DATETIME,
                                last_modified DATETIME,
                                created_at DATETIME DEFAULT GETDATE()
                            )
                        END";

                    using (var command = new SqlCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Tạo bảng app_history
                    string createHistoryTableQuery = @"
                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'app_history')
                        BEGIN
                            CREATE TABLE app_history (
                                id INT IDENTITY(1,1) PRIMARY KEY,
                                app_name NVARCHAR(255) NOT NULL,
                                action_type NVARCHAR(50),
                                action_time DATETIME DEFAULT GETDATE()
                            )
                        END";

                    using (var command = new SqlCommand(createHistoryTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo database: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }













        public static long GetDirectorySize(string directoryPath)
        {
            long size = 0;

            try
            {
                if (string.IsNullOrEmpty(directoryPath) || !Directory.Exists(directoryPath))
                {
                    return 0;
                }

                DirectoryInfo di = new DirectoryInfo(directoryPath);

                // Lấy kích thước các file
                foreach (FileInfo file in di.GetFiles())
                {
                    try
                    {
                        size += file.Length;
                    }
                    catch (Exception)
                    {
                        // Bỏ qua lỗi khi không thể đọc kích thước file
                        continue;
                    }
                }

                // Đệ quy các thư mục con
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    try
                    {
                        size += GetDirectorySize(dir.FullName);
                    }
                    catch (Exception)
                    {
                        // Bỏ qua lỗi khi không thể đọc thư mục con
                        continue;
                    }
                }
            }
            catch (Exception)
            {
                // Trả về 0 nếu có lỗi với thư mục
                return 0;
            }

            return size;
        }

        private List<Tuple<string, string, long, DateTime>> GetInstalledApplications(long sizeCondition, string driveLetter)
        {
            List<Tuple<string, string, long, DateTime>> installedApps = new List<Tuple<string, string, long, DateTime>>();

            try
            {
                // Xử lý Registry CurrentUser
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
                {
                    if (key != null)
                    {
                        ProcessRegistryKey(key, installedApps, sizeCondition, driveLetter);
                    }
                }

                // Xử lý Registry LocalMachine 32-bit
                using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32)
                    .OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
                {
                    if (key != null)
                    {
                        ProcessRegistryKey(key, installedApps, sizeCondition, driveLetter);
                    }
                }

                // Xử lý Registry LocalMachine 64-bit
                using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64)
                    .OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
                {
                    if (key != null)
                    {
                        ProcessRegistryKey(key, installedApps, sizeCondition, driveLetter);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đọc Registry: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return installedApps;
        }

        private void ProcessRegistryKey(RegistryKey key, List<Tuple<string, string, long, DateTime>> installedApps,
            long sizeCondition, string driveLetter)
        {
            foreach (string keyName in key.GetSubKeyNames())
            {
                try
                {
                    using (RegistryKey subkey = key.OpenSubKey(keyName))
                    {
                        if (subkey == null) continue;

                        string displayName = subkey.GetValue("DisplayName") as string;
                        string installLocation = subkey.GetValue("InstallLocation") as string;

                        if (string.IsNullOrEmpty(displayName) || string.IsNullOrEmpty(installLocation))
                            continue;

                        // Chuẩn hóa đường dẫn
                        installLocation = installLocation.Trim().TrimEnd('\\');

                        if (!installLocation.StartsWith(driveLetter, StringComparison.OrdinalIgnoreCase))
                            continue;

                        // Kiểm tra đường dẫn hợp lệ
                        if (!Directory.Exists(installLocation))
                            continue;

                        long size = GetDirectorySize(installLocation);
                        if (sizeCondition == 0 || size >= sizeCondition)
                        {
                            DateTime lastModified;
                            try
                            {
                                lastModified = Directory.GetLastWriteTime(installLocation);
                            }
                            catch
                            {
                                lastModified = DateTime.Now;
                            }

                            installedApps.Add(Tuple.Create(displayName, installLocation, size, lastModified));
                        }
                    }
                }
                catch (Exception)
                {
                    // Bỏ qua các key gây lỗi
                    continue;
                }
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            long sizeCondition = 0;


            if (cboSizeFilter.SelectedItem.ToString() != "💾 Tất cả")
            {
                switch (cboSizeFilter.SelectedItem.ToString())
                {
                    case "💾 >100MB":
                        sizeCondition = 100L * 1024 * 1024;
                        break;
                    case "💾 >500MB":
                        sizeCondition = 500L * 1024 * 1024;
                        break;
                    case "💾 >1GB":
                        sizeCondition = 1L * 1024 * 1024 * 1024;
                        break;
                }
            }

            string driveLetter = cboDriveSelect.SelectedItem.ToString().Substring(3);
            List<Tuple<string, string, long, DateTime>> apps;

            // Thêm loading indicator
            Cursor = Cursors.WaitCursor;
            btnShowAll.Enabled = false;
            btnShowAll.Text = "🔄 Đang tải...";

            try
            {
                // Kiểm tra xem có dữ liệu trong database không
                if (HasDataInDatabase())
                {
                    apps = GetAppsFromDatabase(sizeCondition, driveLetter);
                    Console.WriteLine("Database");
                }
                else
                {
                    // Nếu không có dữ liệu, quét registry và lưu vào database
                    apps = GetInstalledApplications(sizeCondition, driveLetter);
                    UpdateDatabase(apps);
                    Console.WriteLine("Registry");
                }

                // Sắp xếp dữ liệu
                string sortOrder = cboSortOrder.SelectedItem.ToString();
                apps = SortApps(apps, sortOrder);

                // Hiển thị dữ liệu
                DisplayApps(apps);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnShowAll.Enabled = true;
                btnShowAll.Text = "🔍 Hiển thị";
            }
        }

        private List<Tuple<string, string, long, DateTime>> SortApps(
            List<Tuple<string, string, long, DateTime>> apps, string sortOrder)
        {
            switch (sortOrder)
            {
                case "🔤 Tên A-Z":
                    return apps.OrderBy(a => a.Item1).ToList();
                case "🔤 Tên Z-A":
                    return apps.OrderByDescending(a => a.Item1).ToList();
                case "📊 Dung lượng ↑":
                    return apps.OrderBy(a => a.Item3).ToList();
                case "📊 Dung lượng ↓":
                    return apps.OrderByDescending(a => a.Item3).ToList();
                case "📅 Mới nhất":
                    return apps.OrderByDescending(a => a.Item4).ToList();
                case "📅 Cũ nhất":
                    return apps.OrderBy(a => a.Item4).ToList();
                default:
                    return apps;
            }
        }

        private void DisplayApps(List<Tuple<string, string, long, DateTime>> apps)
        {
            lvInstalledApps.Items.Clear();

            foreach (var app in apps)
            {
                ListViewItem item = new ListViewItem(app.Item1);
                item.SubItems.Add(app.Item2);
                item.SubItems.Add((app.Item3 / (1024 * 1024)).ToString() + " MB");
                item.SubItems.Add(app.Item4.ToString("dd/MM/yyyy"));
                item.Tag = app.Item2;

                lvInstalledApps.Items.Add(item);
            }

            if (lvInstalledApps.Items.Count == 0)
            {
                MessageBox.Show("Không tìm thấy ứng dụng nào phù hợp với điều kiện lọc.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void OpenApplication(string path)
        {
            try
            {
                Process.Start(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở ứng dụng: " + ex.Message);
            }
        }

        private void lvInstalledApps_MouseClick(object sender, MouseEventArgs e)
        {

        }



        private void Form2_Load(object sender, EventArgs e)
        {
            // Các thao tác khởi tạo khác nếu cần
        }



        private List<Tuple<string, string, long, DateTime>> GetAppsFromDatabase(long sizeCondition, string driveLetter)
        {
            var apps = new List<Tuple<string, string, long, DateTime>>();

            try
            {
                using (var connection = new SqlConnection($"{connectionString};Database={databaseName}"))
                {
                    connection.Open();
                    string query = @"
                        SELECT app_name, install_location, size_mb, last_modified 
                        FROM installed_apps 
                        WHERE install_location LIKE @driveLetter + '%'
                        AND (@sizeCondition = 0 OR size_mb >= @sizeCondition)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@driveLetter", driveLetter);
                        command.Parameters.AddWithValue("@sizeCondition", sizeCondition / (1024 * 1024));

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var app = Tuple.Create(
                                    reader.GetString(0),
                                    reader.GetString(1),
                                    (long)(reader.GetInt64(2) * 1024 * 1024),
                                    reader.GetDateTime(3)
                                );
                                apps.Add(app);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đọc database: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return apps;
        }

        private void UpdateDatabase(List<Tuple<string, string, long, DateTime>> apps)
        {
            try
            {
                using (var connection = new SqlConnection($"{connectionString};Database={databaseName}"))
                {
                    connection.Open();

                    // Xóa dữ liệu cũ
                    using (var command = new SqlCommand("TRUNCATE TABLE installed_apps", connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Thêm dữ liệu mới
                    foreach (var app in apps)
                    {
                        string insertQuery = @"
                            INSERT INTO installed_apps 
                            (app_name, install_location, size_mb, last_modified)
                            VALUES (@name, @location, @size, @lastModified)";

                        using (var command = new SqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@name", app.Item1);
                            command.Parameters.AddWithValue("@location", app.Item2);
                            command.Parameters.AddWithValue("@size", app.Item3 / (1024 * 1024));

                            // Kiểm tra và giới hạn DateTime trong phạm vi hợp lệ của SQL Server
                            DateTime lastModified = app.Item4;
                            if (lastModified < new DateTime(1753, 1, 1))
                                lastModified = new DateTime(1753, 1, 1);
                            if (lastModified > new DateTime(9999, 12, 31))
                                lastModified = new DateTime(9999, 12, 31);

                            command.Parameters.AddWithValue("@lastModified", lastModified);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật database: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Thêm nút Refresh để cập nhật lại dữ liệu từ Registry
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                string driveLetter = cboDriveSelect.SelectedItem.ToString().Substring(3);
                var apps = GetInstalledApplications(0, driveLetter);
                UpdateDatabase(apps);

                MessageBox.Show("Đã cập nhật dữ liệu thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Tự động hiển thị lại danh sách
                btnShowAll_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        // Thêm phương thức mới để kiểm tra dữ liệu trong database
        private bool HasDataInDatabase()
        {
            try
            {
                using (var connection = new SqlConnection($"{connectionString};Database={databaseName}"))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM installed_apps";
                    using (var command = new SqlCommand(query, connection))
                    {
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kiểm tra database: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void lvInstalledApps_DoubleClick(object sender, EventArgs e)
        {
            if (lvInstalledApps.SelectedItems.Count > 0)
            {
                ListViewItem item = lvInstalledApps.SelectedItems[0];
                string name = item.Text;
                string path = item.Tag.ToString();
                long size = long.Parse(item.SubItems[2].Text.Replace(" MB", "")) * 1024 * 1024;
                DateTime lastModified = DateTime.ParseExact(item.SubItems[3].Text,
                    "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var detailForm = new ChiTietUD(name, path, size, lastModified);
                detailForm.ShowDialog();
            }
        }

    }

}