using AirborneDustMonitor.Core.Interfaces;
using AirborneDustMonitor.Core.Settings;
using AirborneDustMonitor.Core.Statistics;
using Moq;

namespace AirborneDustMonitor.Tests
{
    public class SimpleMovingAverageCalculatorTests
    {
        private readonly SimpleMovingAverageCalculator _calculator;
        private readonly Mock<IAppSettingsService> _settingsMock;

        public SimpleMovingAverageCalculatorTests()
        {
            _settingsMock = new Mock<IAppSettingsService>();
            var defaultSettings = new AppSettings();
            _settingsMock.Setup(s => s.Current).Returns(defaultSettings);
            _calculator = new SimpleMovingAverageCalculator(_settingsMock.Object);
        }

        [Fact]
        public void AddSample_ValidData_CorrectAverage()
        {
            _calculator.AddSample(59.631m);
            Assert.Equal(59.631m, _calculator.Average);

            _calculator.AddSample(-16.919m);
            Assert.Equal(21.356m, Math.Round(_calculator.Average, 3));

            _calculator.AddSample(-2.255m);
            Assert.Equal(13.486m, Math.Round(_calculator.Average, 3));

            _calculator.AddSample(29.514m);
            Assert.Equal(3.447m, Math.Round(_calculator.Average, 3));
        }

        [Fact]
        public void AddSample_SameValue_CorrectAverage()
        {
            _calculator.AddSample(10);
            _calculator.AddSample(10);
            _calculator.AddSample(10);
            Assert.Equal(10, _calculator.Average);
        }

        [Fact]
        public void DefaultAverage_NoData_CorrectAverage()
        {
            Assert.Equal(0, _calculator.Average);
        }
    }
}