using AirborneDustMonitor.Core.Entities;

namespace AirborneDustMonitor.Core.Rules
{
    public class PeakRule : IAlertRule
    {
        private readonly Dictionary<MetricType, decimal> _peaks = new();

        public Alert? Evaluate(MetricType metricType, int sensorID, decimal value)
        {
            if (!_peaks.TryGetValue(metricType, out var currentPeak) || value > currentPeak)
            {
                _peaks[metricType] = value;
                return new Alert($"Új csúcsérték: {value}", metricType, sensorID, AlertType.PeakValue, AlertStatus.Alerting);
            }
            return null;
        }
    }
}
