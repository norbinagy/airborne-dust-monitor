using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Rules;

namespace AirborneDustMonitor.Tests
{
    public class ThresholdRuleTests
    {
        private readonly ThresholdRule _thresholdRule;

        public ThresholdRuleTests()
        {
            _thresholdRule = new ThresholdRule();
        }

        [Fact]
        public void Evaluate_AboveThreshold_ReturnsAlert()
        {
            int sensorID = 1;
            MetricType metricType = MetricType.ParticulateMatter;
            decimal value = 6.9m;
            decimal threshold = 6.7m;

            _thresholdRule.SetThreshold(metricType, threshold);
            var alert = _thresholdRule.Evaluate(metricType, sensorID, value);

            Assert.NotNull(alert);
            Assert.Equal(AlertType.ThresholdExceeded, alert.Type);
            Assert.Equal($"Figyelem, szenzor {sensorID} ({metricType}, {value}) átlépte a határértéket: {threshold}.", alert.Message);
        }

        [Fact]
        public void Evaluate_BelowThreshold_NoAlert()
        {
            int sensorID = 2;
            MetricType metricType = MetricType.Humidity;
            decimal value = 55m;
            decimal threshold = 60m;

            _thresholdRule.SetThreshold(metricType, threshold);
            var alert = _thresholdRule.Evaluate(metricType, sensorID, value);

            Assert.Null(alert);
        }
    }
}