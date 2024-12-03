using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTLNET
{
    public partial class CaiDat : Form,ILanguageChangeable
    {
        private readonly Color lightTheme = Color.FromArgb(240, 240, 240);
        private readonly Color darkTheme = Color.FromArgb(45, 45, 48);

        public event EventHandler<ThemeChangedEventArgs> ThemeChanged;
        public event EventHandler<LanguageChangedEventArgs> LanguageChanged;

        public CaiDat()
        {
            InitializeComponent();
            LoadCurrentSettings();
            LanguageChange.LanguageChanged += (s, lang) => ApplyLanguage(lang);
            ApplyLanguage(LanguageChange.CurrentLanguage);
        }

        private void LoadCurrentSettings()
        {
            radioButtonLight.Checked = Properties.Settings.Default.Theme == "Light";
            radioButtonDark.Checked = Properties.Settings.Default.Theme == "Dark";
            radioButtonVietnamese.Checked = Properties.Settings.Default.Language == "Vietnamese";
            radioButtonEnglish.Checked = Properties.Settings.Default.Language == "English";
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string selectedTheme = radioButtonLight.Checked ? "Light" : "Dark";
            Properties.Settings.Default.Theme = selectedTheme;
            ThemeChanged?.Invoke(this, new ThemeChangedEventArgs(selectedTheme));

            string selectedLanguage = radioButtonVietnamese.Checked ? "Vietnamese" : "English";
            LanguageChange.CurrentLanguage = selectedLanguage;

            SaveSettings();
            ShowSuccessMessage();
        }

        private void SaveSettings()
        {
            string selectedTheme = radioButtonLight.Checked ? "Light" : "Dark";
            string selectedLanguage = radioButtonVietnamese.Checked ? "Vietnamese" : "English";

            Properties.Settings.Default.Theme = selectedTheme;
            Properties.Settings.Default.Language = selectedLanguage;
            Properties.Settings.Default.Save();

            ThemeChanged?.Invoke(this, new ThemeChangedEventArgs(selectedTheme));
            LanguageChanged?.Invoke(this, new LanguageChangedEventArgs(selectedLanguage));
        }

        private void ShowSuccessMessage()
        {
            bool isVietnamese = Properties.Settings.Default.Language == "Vietnamese";
            MessageBox.Show(
                isVietnamese ? "Đã lưu cài đặt thành công!" : "Settings saved successfully!",
                isVietnamese ? "Thông báo" : "Notice",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtnChangePassword_Click(object sender, EventArgs e)
        {
            if (!ValidateCurrentPassword())
            {
                ShowErrorMessage();
                return;
            }

            SaveNewPassword();
            ShowPasswordChangeSuccess();
            ClearPasswordFields();
        }

        private bool ValidateCurrentPassword()
        {
            return txtCurrentPassword.Text == Properties.Settings.Default.KeyloggerPassword;
        }

        private void SaveNewPassword()
        {
            Properties.Settings.Default.KeyloggerPassword = txtNewPassword.Text;
            Properties.Settings.Default.Save();
        }

        private void ShowErrorMessage()
        {
            bool isVietnamese = Properties.Settings.Default.Language == "Vietnamese";
            MessageBox.Show(
                isVietnamese ? "Mật khẩu hiện tại không đúng!" : "Current password is incorrect!",
                isVietnamese ? "Lỗi" : "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void ShowPasswordChangeSuccess()
        {
            bool isVietnamese = Properties.Settings.Default.Language == "Vietnamese";
            MessageBox.Show(
                isVietnamese ? "Đổi mật khẩu thành công!" : "Password changed successfully!",
                isVietnamese ? "Thông báo" : "Notice",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void ClearPasswordFields()
        {
            txtCurrentPassword.Clear();
            txtNewPassword.Clear();
        }

        public void ApplyLanguage(string language)
        {
            btnChangePassword.Text = LanguageChange.GetTranslation("ChangePassword");
            groupBoxLanguage.Text = LanguageChange.GetTranslation("Language");
            groupBoxTheme.Text = LanguageChange.GetTranslation("Theme");
            lblCurrentPassword.Text = LanguageChange.GetTranslation("CurrentPassword");
            lblNewPassword.Text = LanguageChange.GetTranslation("NewPassword");
            radioButtonDark.Text = LanguageChange.GetTranslation("Dark");
            radioButtonLight.Text = LanguageChange.GetTranslation("Light");
            radioButtonEnglish.Text = LanguageChange.GetTranslation("English");
            radioButtonVietnamese.Text = LanguageChange.GetTranslation("Vietnamese");
            groupBoxPassword.Text = LanguageChange.GetTranslation("ChangePassword");
            btnApply.Text = LanguageChange.GetTranslation("Apply");
            Text = LanguageChange.GetTranslation("Settings");
        }
    }

    public class ThemeChangedEventArgs : EventArgs
    {
        public string Theme { get; }
        public ThemeChangedEventArgs(string theme)
        {
            Theme = theme;
        }
    }

    public class LanguageChangedEventArgs : EventArgs
    {
        public string Language { get; }
        public LanguageChangedEventArgs(string language)
        {
            Language = language;
        }
    }
}
