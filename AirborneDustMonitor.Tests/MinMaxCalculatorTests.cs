using AirborneDustMonitor.Core.Statistics;

namespace AirborneDustMonitor.Tests
{
    public class MinMaxCalculatorTests
    {
        private readonly MinMaxCalculator _calculator = new();

        [Fact]
        public void AddSample_ValidData_CorrectMinMax()
        {
            _calculator.AddSample(10.2m);
            _calculator.AddSample(-5.782m);
            _calculator.AddSample(15.33m);
            Assert.Equal(-5.782m, _calculator.Min);
            Assert.Equal(15.33m, _calculator.Max);
        }

        [Fact]
        public void AddSample_SameValue_CorrectMinMax()
        {
            _calculator.AddSample(10);
            _calculator.AddSample(10);
            _calculator.AddSample(10);
            Assert.Equal(10, _calculator.Min);
            Assert.Equal(10, _calculator.Max);
        }
    }
}