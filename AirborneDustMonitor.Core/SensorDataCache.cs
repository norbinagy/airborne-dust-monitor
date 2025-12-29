using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirborneDustMonitor.Core
{
    public class SensorDataCache
    {
        private readonly Queue<SensorData> _recentSensorData;
        private readonly int _maxSize;

        public SensorDataCache(int maxSize)
        {
            _recentSensorData = new Queue<SensorData>(maxSize);
            _maxSize = maxSize;
        }

        public void Add(SensorData sensorData)
        {
            _recentSensorData.Enqueue(sensorData);

            if (_recentSensorData.Count > _maxSize)
            {
                _recentSensorData.Dequeue();
            }
        }

        public IReadOnlyCollection<SensorData> GetAll()
        {
            return _recentSensorData.ToList();
        }
    }
}
