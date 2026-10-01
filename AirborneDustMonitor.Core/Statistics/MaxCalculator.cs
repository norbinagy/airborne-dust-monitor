using AirborneDustMonitor.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirborneDustMonitor.Core.Statistics
{
    public class MaxCalculator : IStatisticCalculator
    {
        public StatisticCalculatorType Type => StatisticCalculatorType.MaxCalculator;

        public decimal Value { get; private set; } = decimal.MinValue;

        public void AddSample(decimal sample)
        {
            if (sample > Value) Value = sample;
        }
    }
}
