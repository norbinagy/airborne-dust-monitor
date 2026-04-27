using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Services;
using AirborneDustMonitor.UI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;

namespace AirborneDustMonitor.UI.ViewModels
{
    // ViewModel a főablakhoz, amely kezeli a különböző metrikák megjelenítését és az alert üzeneteket.
    public partial class MainWindowViewModel : ObservableObject
    {
        public ObservableCollection<MetricDisplayViewModel> MetricDisplays { get; }
        private readonly AlertService _alertService;
        private readonly IWindowService _windowService;

        [ObservableProperty]
        private string _toastMessage = "";

        [ObservableProperty]
        private bool _isToastOpen;

        public MainWindowViewModel(DataService provider, AlertService alertService, IWindowService windowService)
        {
            MetricDisplays = new ObservableCollection<MetricDisplayViewModel>();
            _alertService = alertService;
            _windowService = windowService;

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
        }

        public void DisposeMetricDisplays()
        {
            foreach (var display in MetricDisplays)
            {
                display.Unsubscribe();
            }
            _alertService.AlertRaised -= AlertHandler;
        }

        public void AlertHandler(Alert alert)
        {
            if (alert.Type == AlertType.PeakValue)
            {
                ShowToast($"{alert.Message}\n{alert.MetricType}\nSzenzor: {alert.SensorID}\n{alert.Date}");
            }
        }

        private async void ShowToast(string message)
        {
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                ToastMessage = message;
                IsToastOpen = true;

                await Task.Delay(5000);

                IsToastOpen = false;
            });
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
    }
}
