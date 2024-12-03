using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Win32;
using IWshRuntimeLibrary;
using System.Linq;
using System.Threading.Tasks;
using File = System.IO.File;
using Microsoft.WindowsAPICodePack.Shell;
using System.Text.RegularExpressions;

namespace BTLNET
{
    public partial class ChiTietUD : Form
    {
        private string appPath;
        private string historyFilePath;
        private List<ListViewItem> allItems;
        private string name;

        public ChiTietUD(string name, string path, long size, DateTime lastModified)
        {
            InitializeComponent();
            this.Text = "Chi tiết ứng dụng";
            this.appPath = path;
            this.allItems = new List<ListViewItem>();

            // Thiết lập đường dẫn file lịch sử
            string historyDir = Path.Combine(Application.StartupPath, "History");
            if (!Directory.Exists(historyDir))
                Directory.CreateDirectory(historyDir);
            this.historyFilePath = Path.Combine(historyDir,
                Path.GetFileNameWithoutExtension(path) + "_history.txt");

            // Hiển thị thông tin
            lblAppName.Text = name;
            lblPath.Text = path;
            lblSize.Text = FormatSize(size);
            lblLastModified.Text = lastModified.ToString("dd/MM/yyyy HH:mm:ss");
            this.name = name;
            // Thiết lập ListView
            SetupListView();

            // Đăng ký sự kiện Load
            this.Load += ChiTietUD_Load;
        }



        private void ChiTietUD_Load(object sender, EventArgs e)
        {
            LoadHistory();
        }

        private void LoadHistory()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(LoadHistory));
                return;
            }

            try
            {
                progressBar.Visible = true;
                progressBar.Value = 0;
                listViewHistory.Items.Clear();
                allItems.Clear();

                // Đọc lịch sử từ file trước
                LoadHistoryFromFile();

                Task.Run(() =>
                {
                    try
                    {
                        string[] recentApps = GetRecentApplications();
                        int total = recentApps.Length;
                        int current = 0;
                        int skippedFiles = 0;
                        List<ListViewItem> tempItems = new List<ListViewItem>();

                        foreach (string path in recentApps)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(path) && IsValidPath(path) && path == this.appPath)
                                {
                                    if (!HasFileAccess(path))
                                    {
                                        skippedFiles++;
                                        continue;
                                    }

                                    var fileInfo = new FileInfo(path);
                                    var lastAccessTimes = new List<DateTime>
                                    {
                                        fileInfo.LastAccessTime,
                                        fileInfo.LastWriteTime,
                                        fileInfo.CreationTime
                                    };

                                    lastAccessTimes.Sort((x, y) => y.CompareTo(x));

                                    foreach (var accessTime in lastAccessTimes)
                                    {
                                        ListViewItem item = new ListViewItem(accessTime.ToString("dd/MM/yyyy HH:mm:ss"));
                                        item.SubItems.Add(Path.GetFileNameWithoutExtension(path));
                                        item.SubItems.Add(path);
                                        tempItems.Add(item);
                                        current++;
                                    }

                                    // Cập nhật progress bar an toàn
                                    if (this.IsHandleCreated)
                                    {
                                        this.BeginInvoke((MethodInvoker)delegate
                                        {
                                            progressBar.Value = (int)((float)current / total * 100);
                                        });
                                    }
                                }
                            }
                            catch (UnauthorizedAccessException)
                            {
                                skippedFiles++;
                                continue;
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }

                        // Cập nhật UI một lần duy nhất sau khi hoàn thành
                        if (this.IsHandleCreated)
                        {
                            this.BeginInvoke((MethodInvoker)delegate
                            {
                                allItems.AddRange(tempItems);
                                ApplyFilterAndSearch();
                                progressBar.Visible = false;

                                if (skippedFiles > 0)
                                {
                                    MessageBox.Show(
                                        $"Đã bỏ qua {skippedFiles} file do không có quyền truy cập.",
                                        "Thông báo",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                                }
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        if (this.IsHandleCreated)
                        {
                            this.BeginInvoke((MethodInvoker)delegate
                            {
                                MessageBox.Show($"Lỗi khi đọc lịch sử: {ex.Message}", "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                progressBar.Visible = false;
                            });
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar.Visible = false;
            }
        }

        // Thêm phương thức kiểm tra quyền truy cập file
        private bool HasFileAccess(string filePath)
        {
            try
            {
                // Thử mở file để đọc
                using (FileStream fs = File.OpenRead(filePath))
                {
                    return true;
                }
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Thêm phương thức ApplyFilterAndSearch
        private void ApplyFilterAndSearch()
        {
            // Sắp xếp theo thời gian mới nhất
            allItems.Sort((x, y) =>
            {
                DateTime dx = DateTime.ParseExact(x.Text, "dd/MM/yyyy HH:mm:ss",
                    System.Globalization.CultureInfo.InvariantCulture);
                DateTime dy = DateTime.ParseExact(y.Text, "dd/MM/yyyy HH:mm:ss",
                    System.Globalization.CultureInfo.InvariantCulture);
                return dy.CompareTo(dx);
            });

            // Loại bỏ các mục trùng lặp dựa trên thời gian
            var uniqueItems = allItems
                .GroupBy(x => x.Text)
                .Select(g => g.First())
                .ToList();

            // Hiển thị items
            listViewHistory.Items.Clear();
            listViewHistory.Items.AddRange(uniqueItems.ToArray());
        }

        private string[] GetRecentApplications()
        {
            List<string> recentApps = new List<string>();

            try
            {
                // Đọc từ Registry RunMRU
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Explorer\RunMRU"))
                {
                    if (key != null)
                    {
                        foreach (string valueName in key.GetValueNames())
                        {
                            try
                            {
                                string value = (string)key.GetValue(valueName);
                                if (!string.IsNullOrEmpty(value))
                                {
                                    string appPath = value.Split(new[] { '\0' })[0];
                                    if (IsValidPath(appPath))
                                    {
                                        recentApps.Add(appPath);
                                    }
                                }
                            }
                            catch { continue; }
                        }
                    }
                }

                // Thêm các nguồn khác như trong UDDaDung
                // Program Files
                string[] programPaths = {
                    Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                    Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)
                };

                foreach (string programPath in programPaths)
                {
                    if (Directory.Exists(programPath))
                    {
                        try
                        {
                            var exeFiles = Directory.GetFiles(programPath, "*.exe", SearchOption.AllDirectories);
                            recentApps.AddRange(exeFiles);
                        }
                        catch { continue; }
                    }
                }

                // Recent folder
                string recentPath = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
                if (Directory.Exists(recentPath))
                {
                    foreach (string shortcutPath in Directory.GetFiles(recentPath, "*.lnk"))
                    {
                        try
                        {
                            var shell = new WshShell();
                            var shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
                            string targetPath = shortcut.TargetPath;
                            if (IsValidPath(targetPath))
                            {
                                recentApps.Add(targetPath);
                            }
                        }
                        catch { continue; }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi quét ứng dụng: {ex.Message}");
            }

            return recentApps.Distinct().ToArray();
        }

        private bool IsValidPath(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path)) return false;

                // Kiểm tra các ký tự không hợp lệ trong đường dẫn
                char[] invalidChars = Path.GetInvalidPathChars().Concat(new[] { '<', '>', '|', '"', '*', '?' }).ToArray();
                if (path.IndexOfAny(invalidChars) >= 0) return false;

                // Kiểm tra phần mở rộng
                if (!path.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)) return false;

                // Chuẩn hóa đường dẫn
                string fullPath = Path.GetFullPath(path);

                // Kiểm tra file có tồn tại không
                if (!File.Exists(fullPath)) return false;

                // Kiểm tra độ dài đường dẫn
                if (fullPath.Length > 260) return false; // Windows MAX_PATH

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Thêm phương thức mới để đọc lịch sử từ file
        private void LoadHistoryFromFile()
        {
            try
            {
                if (File.Exists(historyFilePath))
                {
                    string[] lines = File.ReadAllLines(historyFilePath);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length >= 3)
                        {
                            ListViewItem item = new ListViewItem(parts[0]); // Thời gian
                            item.SubItems.Add(parts[1]); // Thao tác
                            allItems.Add(item);
                        }
                    }
                    ApplyFilterAndSearch();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đọc file lịch sử: {ex.Message}", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Sửa lại phương thức SaveHistory để lưu thêm action
        private void SaveHistory(string action)
        {
            try
            {
                string timeStamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                ListViewItem item = new ListViewItem(timeStamp);
                item.SubItems.Add(action);

                listViewHistory.Items.Insert(0, item);
                allItems.Insert(0, item);

                // Lưu vào file
                string historyEntry = $"{timeStamp}|{action}|{appPath}\n";
                File.AppendAllText(historyFilePath, historyEntry);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể lưu lịch sử: {ex.Message}", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string FormatSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (true)
                {

                    //SaveHistory("Mở ứng dụng");

                    // Tìm trong AppsFolder
                    var appsFolderId = new Guid("{1e87508d-89c2-42f0-8a7e-645a0f50ca58}");
                    IKnownFolder appsFolder = KnownFolderHelper.FromKnownFolderId(appsFolderId);

                    string appName = Path.GetFileNameWithoutExtension(appPath).ToLower();
                    Console.WriteLine(appName);
                    bool found = false;
                    foreach (var app in appsFolder)
                    {
                        // Sửa lại cách so sánh tên ứng dụng
                        if (this.name == app.Name)
                        {
                            Process.Start("explorer.exe", $"shell:appsFolder\\{app.ParsingName}");
                            SaveHistory("Chạy ứng dụng");
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        MessageBox.Show("Không tìm thấy ứng dụng trong hệ thống.", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể mở ứng dụng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", $"/select,\"{appPath}\"");
                SaveHistory("Mở thư mục");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể mở thư mục: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sửa lại sự kiện FormClosing
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Không cần làm gì đặc biệt vì dữ liệu đã được lưu trong file
            base.OnFormClosing(e);
        }

        
    }
}

