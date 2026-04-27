using AirborneDustMonitor.Core.Email;
using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Interfaces;
using AirborneDustMonitor.Core.Rules;
using AirborneDustMonitor.Core.Services;
using AirborneDustMonitor.Core.Settings;
using Moq;

namespace AirborneDustMonitor.Tests
{
    public class AlertServiceTests
    {
        [Fact]
        public void ProcessData_ShouldRaiseAlert_WhenThresholdIsExceeded()
        {
            var settingsMock = new Mock<IAppSettingsService>();
            var emailSenderMock = new Mock<IEmailSender>();
            var settings = new AppSettings();
            settingsMock.Setup(s => s.Current).Returns(settings);
            settingsMock.Object.Current.Alert.EnableEmailAlerts = true;

            var alertService = new AlertService(emailSenderMock.Object, settingsMock.Object);
            alertService.GetRule<ThresholdRule>()!.SetThreshold(MetricType.Humidity, 50);

            var raisedAlerts = new List<Alert>();
            alertService.AlertRaised += (alert) => raisedAlerts.Add(alert);

            var badData = new MetricData(MetricType.Humidity, 100, new DateTime(2024, 1, 1), 2, 0, 0, 0);
            alertService.ProcessSample(badData.MetricType, badData.SensorID, badData.Value);

            Assert.NotNull(raisedAlerts);
            Assert.Contains(raisedAlerts, a => a.Type == AlertType.PeakValue);
            Assert.Contains(raisedAlerts, a => a.Type == AlertType.ThresholdExceeded);
            Assert.Equal(2, raisedAlerts.Count);

            emailSenderMock.Verify(m => m.SendAsync(It.IsAny<EmailMessage>(), It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
