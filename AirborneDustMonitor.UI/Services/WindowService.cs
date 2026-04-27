using AirborneDustMonitor.UI.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace AirborneDustMonitor.UI.Services
{
    public class WindowService : IWindowService
    {
        private readonly IServiceProvider _serviceProvider;
        private MapWindow? _mapWindow;

        public WindowService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ShowMapWindow()
        {
            if (_mapWindow != null && _mapWindow.IsLoaded)
            {
                _mapWindow.Activate();
                return;
            }

            _mapWindow = _serviceProvider.GetRequiredService<MapWindow>();
            _mapWindow.Owner = Application.Current.MainWindow;

            _mapWindow.Closed += (s, e) => _mapWindow = null;

            _mapWindow.Show();
        }

        public void ShowSettingsWindow()
        {
            var window = _serviceProvider.GetRequiredService<SettingsWindow>();
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
        }
    }
}
