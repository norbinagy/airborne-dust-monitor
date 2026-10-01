using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Statistics;

namespace AirborneDustMonitor.Core.Services
{
    // A MetricPipeline egy adott metrikához tartozó adatfolyamot és statisztika kalkulátorokat tartalmaz, felelős a beérkező adatok kezeléséért, a statisztikák frissítéséért és az adat stream publikálásáért.
    public class MetricPipeline
    {
        private readonly List<IStatisticCalculator> _calculators;
        private readonly MetricStream _metricStream;

        public MetricPipeline(IEnumerable<IStatisticCalculator> calculators)
        {
            this._calculators = calculators.ToList();
            _metricStream = new MetricStream();
        }

        public IObservable<MetricData> GetStream()
        {
            return _metricStream;
        }

        public void AddSample(decimal value, MetricType metricType, DateTime date, int sensorID)
        {
            foreach (var calculator in _calculators)
            {
                calculator.AddSample(value);
            }

            Dictionary<StatisticCalculatorType, decimal> statistics = new Dictionary<StatisticCalculatorType, decimal>();

            foreach (var calculator in _calculators)
            {
                statistics[calculator.Type] = calculator.Value;
            }

            var data = new MetricData(
                metricType,
                value,
                date,
                sensorID,
                statistics
            );

            _metricStream.Publish(data);
        }
    }
}
