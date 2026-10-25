using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Interfaces.Services;
using System.Diagnostics;

namespace AirborneDustMonitor.Core.Services
{
    // A Dataservice a beérkező adatok kezeléséért felelős, a megfelelő pipeline-ba helyezi azokat és biztosítja a statisztikák számítását, illetve biztosít egy adat stream-et amivel fel lehet iratkozni a kívánt metrikákra.
    public class DataService : IDataService
    {
        private readonly Dictionary<MetricType, MetricPipeline> _pipelines;

        public DataService(IStatisticCalculatorProvider statisticCalculatorProvider)
        {
            _pipelines = new Dictionary<MetricType, MetricPipeline>
            {
                { MetricType.ParticulateMatter, CreatePipeline(statisticCalculatorProvider, MetricType.ParticulateMatter) },
                { MetricType.Temperature, CreatePipeline(statisticCalculatorProvider, MetricType.Temperature) },
                { MetricType.Humidity, CreatePipeline(statisticCalculatorProvider, MetricType.Humidity) },
                { MetricType.BatteryVoltage, CreatePipeline(statisticCalculatorProvider, MetricType.BatteryVoltage) }
            };
        }

        private static MetricPipeline CreatePipeline(IStatisticCalculatorProvider statisticCalculatorProvider, MetricType metricType)
        {
            var calculators = statisticCalculatorProvider.GetCalculators(metricType);
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
