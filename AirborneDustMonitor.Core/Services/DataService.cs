using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Interfaces;
using AirborneDustMonitor.Core.Statistics;
using System.Diagnostics;

namespace AirborneDustMonitor.Core.Services
{
    // A Dataservice a beérkező adatok kezeléséért felelős, a megfelelő pipeline-ba helyezi azokat és biztosítja a statisztikák számítását, illetve biztosít egy adat stream-et amivel fel lehet iratkozni a kívánt metrikákra.
    public class DataService
    {
        private readonly Dictionary<MetricType, MetricPipeline> _pipelines;

        public DataService(IAppSettingsService appSettingsService)
        {
            _pipelines = new Dictionary<MetricType, MetricPipeline>
            {
                { MetricType.ParticulateMatter, CreatePipeline(appSettingsService) },
                { MetricType.Temperature, CreatePipeline(appSettingsService) },
                { MetricType.Humidity, CreatePipeline(appSettingsService) },
                { MetricType.BatteryVoltage, CreatePipeline(appSettingsService) }
            };
        }

        private MetricPipeline CreatePipeline(IAppSettingsService appSettingsService)
        {
            var calculators = new List<IStatisticCalculator>
            {
                new MinCalculator(),
                new MaxCalculator(),
                new SimpleMovingAverageCalculator(appSettingsService)
            };
            return new MetricPipeline(calculators);
        }

        public IObservable<MetricData> GetStream(MetricType metricType)
        {
            return _pipelines[metricType].GetStream();
        }

        public void AddSample(decimal value, MetricType metricType, DateTime date, int sensorID)
        {
            _pipelines[metricType].AddSample(value, metricType, date, sensorID);
            Debug.WriteLine($"DataService.AddSample - Aktuális szál ID: {Thread.CurrentThread.ManagedThreadId} | Háttérszál? {Thread.CurrentThread.IsThreadPoolThread}");
        }
    }
}
