using AirborneDustMonitor.Core.Interfaces.Configuration;
using AirborneDustMonitor.Core.Settings;
using AirborneDustMonitor.Infrastructure;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Controls;

namespace AirborneDustMonitor.UI.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        private readonly IAppSettingsService _appSettingsService;

        public AppSettings Settings { get; private set; }

        public SettingsViewModel(IAppSettingsService appSettingsService)
        {
            _appSettingsService = appSettingsService;
            Settings = _appSettingsService.Current;
        }

        [RelayCommand]
        private void SaveAndClose(Window window)
        {
            var passwordBox = window.FindName("PasswordBox") as PasswordBox;
            if (passwordBox?.Password != null)
            {
                Settings.Email.ProtectedPassword = DataProtectionHelper.Protect(passwordBox.Password);
            }
            _appSettingsService.Save();
            MessageBox.Show("Kérjük indítsa újra az alkalmazást a módosítások érvénybe léptetéséhez!", "Mentés", MessageBoxButton.OK, MessageBoxImage.Information);
            window?.Close();
        }
    }
}
