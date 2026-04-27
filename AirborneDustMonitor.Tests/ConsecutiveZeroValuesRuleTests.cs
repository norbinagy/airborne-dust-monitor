using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Interfaces;
using AirborneDustMonitor.Core.Rules;
using AirborneDustMonitor.Core.Settings;
using Moq;

namespace AirborneDustMonitor.Tests
{
    public class ConsecutiveZeroValuesRuleTests
    {
        private readonly ConsecutiveZeroValuesRule rule;

        public ConsecutiveZeroValuesRuleTests()
        {
            var settingsMock = new Mock<IAppSettingsService>();
            var defaultSettings = new AppSettings();
            settingsMock.Setup(s => s.Current).Returns(defaultSettings);
            rule = new ConsecutiveZeroValuesRule(settingsMock.Object);
        }

        [Fact]
        public void Evaluate_LessThanRequiredCount_NoAlert()
        {
            for (int i = 0; i < 4; i++)
            {
                var alert = rule.Evaluate(MetricType.Humidity, 1, 0);
                Assert.Null(alert);
            }
        }

        [Fact]
        public void Evaluate_RequiredCount_AlertRaised()
        {
            for (int i = 0; i < 5; i++)
            {
                var alert = rule.Evaluate(MetricType.Humidity, 1, 0);
                if (i < 4)
                {
                    Assert.Null(alert);
                }
                else
                {
                    Assert.NotNull(alert);
                    Assert.Equal("Szenzor 1, Humidity típusú mérése 5 egymást követő nulla értéket jelzett.", alert.Message);
                }
            }
        }

        [Fact]
        public void Evaluate_NonZeroValue_ResetsCount()
        {
            for (int i = 0; i < 4; i++)
            {
                var alert = rule.Evaluate(MetricType.Humidity, 1, 0);
                Assert.Null(alert);
            }

            var resetAlert = rule.Evaluate(MetricType.Humidity, 1, 10);
            Assert.Null(resetAlert);

            for (int i = 0; i < 5; i++)
            {
                var alert = rule.Evaluate(MetricType.Humidity, 1, 0);
                if (i < 4)
                {
                    Assert.Null(alert);
                }
                else
                {
                    Assert.NotNull(alert);
                    Assert.Equal("Szenzor 1, Humidity típusú mérése 5 egymást követő nulla értéket jelzett.", alert.Message);
                }
            }
        }
    }
}