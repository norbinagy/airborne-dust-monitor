using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirborneDustMonitor.Core
{
    internal class MovingAverageCalculator
    {
        private Queue<double> dataQueue = new Queue<double>();
        private int windowSize;
        private double sum = 0;
        private double average = 0;

        public double Average
        {
            get
            {
                return average;
            }
        }

        public MovingAverageCalculator(int windowSize)
        {
            this.windowSize = windowSize;
        }

        public void AddDataPoint(double newDataPoint)
        {
            if (dataQueue.Count == windowSize)
            {
                sum -= dataQueue.Dequeue();
            }

            dataQueue.Enqueue(newDataPoint);
            sum += newDataPoint;

            average = sum / dataQueue.Count;
        }

        public override string ToString()
        {
            return average.ToString("0.000");
        }
    }
}
