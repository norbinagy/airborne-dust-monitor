using AirborneDustMonitor.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace AirborneDustMonitor.UI.Views
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow(SettingsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is SettingsViewModel viewModel)
                viewModel.Settings.Email.Password = PasswordBox.Password;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is SettingsViewModel viewModel)
            {
                PasswordBox.Password = viewModel.Settings.Email.Password;
            }
        }
    }
}
