using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTLNET
{
    public interface ILanguageChangeable
    {
        void ApplyLanguage(string language);
    }

    public static class LanguageChange
    {
        public static event EventHandler<string> LanguageChanged;

        public static string CurrentLanguage
        {
            get => Properties.Settings.Default.Language;
            set
            {
                Properties.Settings.Default.Language = value;
                Properties.Settings.Default.Save();
                LanguageChanged?.Invoke(null, value);
            }
        }

        private static readonly Dictionary<string, Dictionary<string, string>> Translations = new Dictionary<string, Dictionary<string, string>>
        {
            ["Common"] = new Dictionary<string, string>
            {
                ["Settings"] = "Cài đặt|Settings",
                ["Apply"] = "Áp dụng|Apply",
                ["Theme"] = "Giao diện|Theme",
                ["Language"] = "Ngôn ngữ|Language",
                ["ChangePassword"] = "Đổi mật khẩu|Change Password",
                ["CurrentPassword"] = "Mật khẩu hiện tại|Current Password",
                ["NewPassword"] = "Mật khẩu mới|New Password",
                ["Light"] = "Sáng|Light",
                ["Dark"] = "Tối|Dark",
                ["Success"] = "Thành công|Success",
                ["Error"] = "Lỗi|Error",
                ["Search"] = "Tìm kiếm|Search",
                ["Export"] = "Xuất|Export",
                ["DiskSpace"] = "Dung lượng|Disk Space",
                ["ModifyDate"] = "Ngày chỉnh sửa|Modify Date",
                ["Vietnamese"] = "Tiếng Việt|Vietnamese",
                ["English"] = "Tiếng Anh|English",
                ["InstalledApps"] = "Ứng dụng đã cài đặt|Installed Apps",
                ["AppHistory"] = "Lịch sử ứng dụng|App History",
                ["KeyboardHistory"] = "Lịch sử bàn phím|Keyboard History",
                ["AppName"] = "Tên ứng dụng|App Name",
                ["Path"] = "Đường dẫn|Path",
                ["Home"] = "Trang chủ|Home",
                ["ApplicationManager"] = "Quản lý ứng dụng|Application Management",
                ["EnterPass"] = "Nhập mật khẩu để xem lịch sử:|Enter password to view log:",
                ["Confirm"] = "Xác nhận|Confirm",
                ["CreateDate"] = "Ngày tạo|Create Date",
                ["LastModified"] = "Lần sửa cuối|Last Modified",
                ["LogFileList"] = "Danh sách file log|List of log files",
                ["UsedApp"] = "Ứng dụng đã dùng|Used applications"

            }
        };

        public static string GetTranslation(string key, string category = "Common")
        {
            if (Translations.TryGetValue(category, out var categoryDict) &&
                categoryDict.TryGetValue(key, out var translation))
            {
                string[] parts = translation.Split('|');
                return CurrentLanguage == "Vietnamese" ? parts[0] : parts[1];
            }
            return key;
        }
    }
}
