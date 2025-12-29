using AirborneDustMonitor.Core;
using AirborneDustMonitor.UI;
using AirborneDustMonitor.Infrastructure;
using AirborneDustMonitor.UI.ViewModels.Charts;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows.Threading;
using System.Reflection.Emit;

namespace AirborneDustMonitor.UI.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly DispatcherTimer _refreshTimer;


        public TemperatureChartViewModel TemperatureVM { get; }
        public HumidityChartViewModel HumidityVM { get; }
        public PMChartViewModel PMVM { get; }
        public BatteryChartViewModel BatteryVM { get; }


        private ISensorDataRepository repo;
        private SensorDataCache cache;
        private SensorDataService service;
        private SensorDataStatistics statistics;

        private DateTime lastFetchTime;


        public MainWindowViewModel()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            repo = new SensorDataRepository(connectionString);
            cache = new SensorDataCache(100);
            service = new SensorDataService(repo, cache);
            statistics = new SensorDataStatistics(cache);
            lastFetchTime = DateTime.Parse("2024-03-28 14:34:00");

            TemperatureVM = new TemperatureChartViewModel();
            HumidityVM = new HumidityChartViewModel();
            PMVM = new PMChartViewModel();
            BatteryVM = new BatteryChartViewModel();


            _refreshTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _refreshTimer.Tick += async (s, e) => await FetchDataAsync();
            _refreshTimer.Tick += (s, e) => TemperatureVM.UpdateChart(service.GetCachedData());
            _refreshTimer.Tick += (s, e) => BatteryVM.UpdateChart(service.GetCachedData());
            _refreshTimer.Tick += (s, e) => PMVM.UpdateChart(service.GetCachedData());
            _refreshTimer.Tick += (s, e) => HumidityVM.UpdateChart(service.GetCachedData());
            _refreshTimer.Start();
        }

        private async Task FetchDataAsync()
        {
            await service.UpdateCacheByDateAsync(lastFetchTime);
            lastFetchTime = lastFetchTime.AddMinutes(1);
        }

    }
}
