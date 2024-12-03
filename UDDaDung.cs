using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using IWshRuntimeLibrary;
using File = System.IO.File;
using Path = System.IO.Path;

namespace BTLNET
{
    public partial class UDDaDung : Form, ILanguageChangeable
    {

        private List<ListViewItem> allItems; // Lưu trữ tất cả items để tìm kiếm
        private string exportPath = $"Logfile/LogApp/logUsedApp{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}.txt";

        public UDDaDung()
        {
            InitializeComponent();
            LanguageChange.LanguageChanged += (s, lang) => ApplyLanguage(lang);
            ApplyLanguage(LanguageChange.CurrentLanguage);
            allItems = new List<ListViewItem>();
            LoadHistory();
        }

        public void ApplyLanguage(string language)
        {
            btnExport.Text = LanguageChange.GetTranslation("Export");

            if (lvHistory.Columns.Count >= 3)
            {
                lvHistory.Columns[0].Text = LanguageChange.GetTranslation("Time");
                lvHistory.Columns[1].Text = LanguageChange.GetTranslation("AppName");
                lvHistory.Columns[2].Text = LanguageChange.GetTranslation("Path");
            }
            else
            {
                // Thêm cột nếu chưa đủ
                lvHistory.Columns.Add("Time", 150, HorizontalAlignment.Left);
                lvHistory.Columns.Add("AppName", 200, HorizontalAlignment.Left);
                lvHistory.Columns.Add("Path", 400, HorizontalAlignment.Left);


                // Cập nhật lại tiêu đề cột
                lvHistory.Columns[0].Text = LanguageChange.GetTranslation("Time");
                lvHistory.Columns[1].Text = LanguageChange.GetTranslation("AppName");
                lvHistory.Columns[2].Text = LanguageChange.GetTranslation("Path");
            }

            btnSearch.Text = LanguageChange.GetTranslation("Search");
            lblTitle.Text = LanguageChange.GetTranslation("UsedApp").ToUpper();
            Text = LanguageChange.GetTranslation("UsedApp");

            cmbFilter.Items.Clear();
            if (language == "Vietnamese")
            {
                cmbFilter.Items.AddRange(new object[] {
                    "🕒 Tất cả",
                    "📅 Hôm nay",
                    "📅 7 ngày qua",
                    "📅 30 ngày qua"});
                
            }
            else
            {
                cmbFilter.Items.AddRange(new object[] {
                    "🕒 All",
                    "📅 Today",
                    "📅 Last 7 days",
                    "📅 Last 30 dáys"});
                
            }
        }

        // Thêm phương thức mới để điều chỉnh độ rộng cột
        private void AdjustColumnWidths()
        {
            if (lvHistory.Columns.Count >= 3)
            {
                int totalWidth = lvHistory.ClientSize.Width;
                lvHistory.Columns[0].Width = 150; // Thời gian
                lvHistory.Columns[1].Width = 200; // Tên ứng dụng
                lvHistory.Columns[2].Width = totalWidth - 370; // Đường dẫn (phần còn lại)
            }
        }

        private void LoadHistory()
        {
            progressBar.Visible = true;
            progressBar.Value = 0;
            lvHistory.Items.Clear();
            allItems.Clear();

            Task.Run(() =>
            {
                try
                {
                    string[] recentApps = GetRecentApplications();
                    int total = recentApps.Length;
                    int current = 0;
                    int skippedFiles = 0; // Đếm số file bị từ chối truy cập

                    foreach (string appPath in recentApps)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(appPath) && IsValidPath(appPath))
                            {
                                // Kiểm tra quyền truy cập
                                if (!HasFileAccess(appPath))
                                {
                                    skippedFiles++;
                                    continue;
                                }

                                string appName = Path.GetFileNameWithoutExtension(appPath);
                                if (!string.IsNullOrEmpty(appName))
                                {
                                    DateTime lastAccessTime = File.GetLastAccessTime(appPath);

                                    ListViewItem item = new ListViewItem(lastAccessTime.ToString("dd/MM/yyyy HH:mm:ss"));
                                    item.SubItems.Add(appName);
                                    item.SubItems.Add(appPath);

                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        allItems.Add(item);
                                        current++;
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

                    this.Invoke((MethodInvoker)delegate
                    {
                        ApplyFilterAndSearch();
                        progressBar.Visible = false;

                        // Hiển thị thông báo nếu có file bị bỏ qua
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
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        MessageBox.Show($"Lỗi khi đọc lịch sử: {ex.Message}");
                        progressBar.Visible = false;
                    });
                }
            });
        }

        private void ApplyFilterAndSearch()
        {
            var filteredItems = allItems.ToList();

            // Áp dụng bộ lọc thời gian
            switch (cmbFilter.SelectedIndex)
            {
                case 1: // Hôm nay
                    filteredItems = filteredItems.Where(x =>
                    {
                        try
                        {
                            return DateTime.ParseExact(x.Text,
                                "dd/MM/yyyy HH:mm:ss",
                                System.Globalization.CultureInfo.InvariantCulture).Date == DateTime.Today;
                        }
                        catch
                        {
                            return false;
                        }
                    }).ToList();
                    break;
                case 2: // 7 ngày qua
                    filteredItems = filteredItems.Where(x =>
                    {
                        try
                        {
                            return DateTime.ParseExact(x.Text,
                                "dd/MM/yyyy HH:mm:ss",
                                System.Globalization.CultureInfo.InvariantCulture) >= DateTime.Now.AddDays(-7);
                        }
                        catch
                        {
                            return false;
                        }
                    }).ToList();
                    break;
                case 3: // 30 ngày qua
                    filteredItems = filteredItems.Where(x =>
                    {
                        try
                        {
                            return DateTime.ParseExact(x.Text,
                                "dd/MM/yyyy HH:mm:ss",
                                System.Globalization.CultureInfo.InvariantCulture) >= DateTime.Now.AddDays(-30);
                        }
                        catch
                        {
                            return false;
                        }
                    }).ToList();
                    break;
            }

            // Áp dụng tìm kiếm
            string searchText = txtSearch.Text.ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                filteredItems = filteredItems.Where(x =>
                    x.SubItems[1].Text.ToLower().Contains(searchText)).ToList();
            }

            // Sắp xếp theo thời gian
            filteredItems = filteredItems.OrderByDescending(x =>
            {
                try
                {
                    return DateTime.ParseExact(x.Text,
                        "dd/MM/yyyy HH:mm:ss",
                        System.Globalization.CultureInfo.InvariantCulture);
                }
                catch
                {
                    return DateTime.MinValue; // Trả về giá trị mặc định nếu parse thất bại
                }
            }).ToList();

            // Hiển thị kết quả
            lvHistory.Items.Clear();
            lvHistory.Items.AddRange(filteredItems.ToArray());
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ApplyFilterAndSearch();
        }

        private void CmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilterAndSearch();
        }

        private bool IsValidPath(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path)) return false;
                if (path.IndexOfAny(Path.GetInvalidPathChars()) >= 0) return false;

                // Chỉ chấp nhận file .exe
                if (!path.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)) return false;

                Path.GetFullPath(path);
                return File.Exists(path);
            }
            catch
            {
                return false;
            }
        }

        private string[] GetRecentApplications()
        {
            List<string> recentApps = new List<string>();
            List<string> accessDeniedPaths = new List<string>(); // Danh sách các đường dẫn bị từ chối

            try
            {
                // 1. Đọc từ Registry RunMRU
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

                // 2. Đọc từ thư mục Program Files
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
                            foreach (var file in exeFiles)
                            {
                                try
                                {
                                    if (HasFileAccess(file))
                                    {
                                        recentApps.Add(file);
                                    }
                                    else
                                    {
                                        accessDeniedPaths.Add(file);
                                    }
                                }
                                catch (UnauthorizedAccessException)
                                {
                                    accessDeniedPaths.Add(file);
                                    continue;
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                            }
                        }
                        catch (UnauthorizedAccessException)
                        {
                            // Bỏ qua thư mục không có quyền truy cập
                            continue;
                        }
                    }
                }

                // 3. Đọc từ thư mục Recent
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

                // 4. Đọc từ AppData
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                if (Directory.Exists(appDataPath))
                {
                    var exeFiles = Directory.GetFiles(appDataPath, "*.exe", SearchOption.AllDirectories);
                    recentApps.AddRange(exeFiles);
                }

                // 5. Đọc từ Start Menu
                string startMenuPath = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
                string commonStartMenuPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);

                foreach (string menuPath in new[] { startMenuPath, commonStartMenuPath })
                {
                    if (Directory.Exists(menuPath))
                    {
                        // Lấy cả file .exe và shortcut
                        var files = Directory.GetFiles(menuPath, "*.*", SearchOption.AllDirectories)
                            .Where(f => f.EndsWith(".exe") || f.EndsWith(".lnk"));

                        foreach (string file in files)
                        {
                            if (file.EndsWith(".lnk"))
                            {
                                try
                                {
                                    var shell = new WshShell();
                                    var shortcut = (IWshShortcut)shell.CreateShortcut(file);
                                    string targetPath = shortcut.TargetPath;
                                    if (IsValidPath(targetPath))
                                    {
                                        recentApps.Add(targetPath);
                                    }
                                }
                                catch { continue; }
                            }
                            else
                            {
                                recentApps.Add(file);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi quét ứng dụng: {ex.Message}");
            }

            // Ghi log các file bị từ chối truy cập
            if (accessDeniedPaths.Count > 0)
            {
                try
                {
                    string logPath = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "AccessDeniedLog.txt"
                    );
                    File.WriteAllLines(logPath, accessDeniedPaths);
                }
                catch { } // Bỏ qua lỗi ghi log
            }

            return recentApps.Distinct().ToArray();
        }

        // Cập nhật phương thức UpdateHistory đ duy trì thứ tự sắp xếp
        public void UpdateHistory(string appName)
        {
            ListViewItem item = new ListViewItem(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            item.SubItems.Add($"{appName}");
            lvHistory.Items.Insert(0, item); // Thêm vào đầu danh sách vì là mục mới nhất
        }

        private void UDDaDung_Load(object sender, EventArgs e)
        {

        }

        private void ExportToNotepad()
        {
            if (string.IsNullOrEmpty(exportPath))
            {
                MessageBox.Show("Vui lòng chọn đường dẫn lưu file trước khi xuất.",
                              "Thông báo",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(exportPath, false, Encoding.UTF8))
                {
                    writer.WriteLine("LỊCH SỬ SỬ DỤNG ỨNG DỤNG");
                    writer.WriteLine("Xuất ngày: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    writer.WriteLine("----------------------------------------");
                    writer.WriteLine();

                    foreach (ListViewItem item in lvHistory.Items)
                    {
                        writer.WriteLine($"Thời gian: {item.SubItems[0].Text}");
                        writer.WriteLine($"Tên ứng dụng: {item.SubItems[1].Text}");
                        if (item.SubItems.Count > 2)
                        {
                            writer.WriteLine($"Đường dẫn: {item.SubItems[2].Text}");
                        }
                        writer.WriteLine("----------------------------------------");
                    }
                }
                MessageBox.Show("Xuất file thành công!",
                              "Thông báo",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất file: {ex.Message}",
                              "Lỗi",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        public void SetExportPath(string path)
        {
            exportPath = path;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportToNotepad();
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
    }
}
