using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace airborne_dust_monitor
{
    internal class MovingAverageCalculator
    {
        private Queue<double> dataQueue = new Queue<double>();
        private int windowSize;
        private double sum = 0;

        public MovingAverageCalculator(int windowSize)
        {
            this.windowSize = windowSize;
        }

        public double AddDataPoint(double newDataPoint)
        {
            if (dataQueue.Count == windowSize)
            {
                sum -= dataQueue.Dequeue();
            }

            dataQueue.Enqueue(newDataPoint);
            sum += newDataPoint;

            return sum / dataQueue.Count;
        }
    }
}
