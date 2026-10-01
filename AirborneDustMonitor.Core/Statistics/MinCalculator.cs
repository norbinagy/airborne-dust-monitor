using AirborneDustMonitor.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirborneDustMonitor.Core.Statistics
{
    public class MinCalculator : IStatisticCalculator
    {
        public StatisticCalculatorType Type => StatisticCalculatorType.MinCalculator;

        public decimal Value { get; private set; } = decimal.MaxValue;

        public void AddSample(decimal sample)
        {
            if (sample < Value) Value = sample;
        }
    }
}
