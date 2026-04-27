using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Interfaces;
using AirborneDustMonitor.Core.Rules;
using AirborneDustMonitor.Core.Settings;
using Moq;

namespace AirborneDustMonitor.Tests
{
    public class BatteryVoltageRuleTests
    {
        private readonly BatteryVoltageRule rule;

        public BatteryVoltageRuleTests()
        {
            var settingsMock = new Mock<IAppSettingsService>();
            var defaultSettings = new AppSettings();
            settingsMock.Setup(s => s.Current).Returns(defaultSettings);
            rule = new BatteryVoltageRule(settingsMock.Object);
        }

        [Fact]
        public void Evaluate_LessThanRequiredCount_NoAlert()
        {
            for (int i = 0; i < 2; i++)
            {
                var alert = rule.Evaluate(MetricType.BatteryVoltage, 1, 3.5m);
                Assert.Null(alert);
            }
        }

        [Fact]
        public void Evaluate_RequiredCount_AlertRaised()
        {
            for (int i = 0; i < 3; i++)
            {
                var alert = rule.Evaluate(MetricType.BatteryVoltage, 1, 3.5m);
                if (i < 2)
                {
                    Assert.Null(alert);
                }
                else
                {
                    Assert.NotNull(alert);
                    Assert.Equal("Szenzor 1: Akkumulátor feszültség túl alacsony/magas!", alert.Message);
                }
            }
        }
    }
}
