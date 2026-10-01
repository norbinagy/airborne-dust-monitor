using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Services;
using AirborneDustMonitor.UI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace AirborneDustMonitor.UI.ViewModels
{
    // ViewModel a főablakhoz, amely kezeli a különböző metrikák megjelenítését és az alert üzeneteket.
    public partial class MainWindowViewModel : ObservableObject, IDisposable
    {
        public ObservableCollection<MetricDisplayViewModel> MetricDisplays { get; }
        private readonly AlertService _alertService;
        private readonly SensorMonitor _monitor;
        private readonly IWindowService _windowService;

        [ObservableProperty]
        private string _toastMessage = "";

        [ObservableProperty]
        private bool _isToastOpen;

        private readonly DispatcherTimer _timer = new();
        private bool _busy;

        public MainWindowViewModel(SensorMonitor monitor, DataService provider, AlertService alertService, IWindowService windowService)
        {
            MetricDisplays = new ObservableCollection<MetricDisplayViewModel>();
            _alertService = alertService;
            _windowService = windowService;
            _monitor = monitor;

            var particulateMatterDisplay = new MetricDisplayViewModel("Szállópor", MetricType.ParticulateMatter, alertService);
            var temperatureDisplay = new MetricDisplayViewModel("Hőmérséklet (°C)", MetricType.Temperature, alertService);
            var humidityDisplay = new MetricDisplayViewModel("Páratartalom (%)", MetricType.Humidity, alertService);
            var batteryDisplay = new MetricDisplayViewModel("Akkumulátor (V)", MetricType.BatteryVoltage, alertService);

            particulateMatterDisplay.Subscribe(provider.GetStream(MetricType.ParticulateMatter));
            temperatureDisplay.Subscribe(provider.GetStream(MetricType.Temperature));
            humidityDisplay.Subscribe(provider.GetStream(MetricType.Humidity));
            batteryDisplay.Subscribe(provider.GetStream(MetricType.BatteryVoltage));

            MetricDisplays.Add(particulateMatterDisplay);
            MetricDisplays.Add(temperatureDisplay);
            MetricDisplays.Add(humidityDisplay);
            MetricDisplays.Add(batteryDisplay);

            alertService.AlertRaised += AlertHandler;

            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += async (_, _) =>
            {
                if (_busy) return;
                _busy = true;
                try { await _monitor.PollOnceAsync(); }
                catch (Exception ex) { Debug.Write(ex, "Poll failed"); }
                finally { _busy = false; }
            };
            _timer.Start();

        }

        public void DisposeMetricDisplays()
        {
            foreach (var display in MetricDisplays)
            {
                display.Unsubscribe();
            }
            _alertService.AlertRaised -= AlertHandler;
        }

        public async void AlertHandler(Alert alert)
        {
            if (alert.Type == AlertType.PeakValue)
            {
                await ShowToastAsync($"{alert.Message}\n{alert.MetricType}\nSzenzor: {alert.SensorID}\n{alert.Date}");
            }
        }

        [RelayCommand]
        private async Task ShowToastAsync(string message)
        {
            ToastMessage = message;
            IsToastOpen = true;
            
            try
            {
                await Task.Delay(5000);
            }
            finally
            {
                IsToastOpen = false;
            }
        }

        [RelayCommand]
        private void OpenSettings()
        {
            _windowService.ShowSettingsWindow();
        }

        [RelayCommand]
        private void OpenMap()
        {
            _windowService.ShowMapWindow();
        }

        public void Dispose()
        {
            DisposeMetricDisplays();
        }
    }
}
