using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirborneDustMonitor.Core
{
    public class SensorDataStatistics
    {
        private readonly SensorDataCache _sensorDataCache;

        public SensorDataStatistics(SensorDataCache sensorDataCache)
        {
            _sensorDataCache = sensorDataCache;
        }

        public decimal GetMin(SensorDataFields field)
        {
            switch (field)
            {
                case SensorDataFields.ParticulateMatter:
                    return _sensorDataCache.GetAll().Count != 0 ? _sensorDataCache.GetAll().Min(d => d.ParticulateMatter) : decimal.Zero;

                case SensorDataFields.Temperature:
                    return _sensorDataCache.GetAll().Count != 0 ? _sensorDataCache.GetAll().Min(d => d.Temperature) : decimal.Zero;

                case SensorDataFields.Humidity:
                    return _sensorDataCache.GetAll().Count != 0 ? _sensorDataCache.GetAll().Min(d => d.Humidity) : decimal.Zero;

                case SensorDataFields.BatteryVoltage:
                    return _sensorDataCache.GetAll().Count != 0 ? _sensorDataCache.GetAll().Min(d => d.BatteryVoltage) : decimal.Zero;

                default:
                    throw new ArgumentException("Invalid field for min calculation");
            }
        }

        public decimal GetMovingAverage(int windowSize)
        {
            var data = _sensorDataCache.GetAll();
            if (data.Count < windowSize) return decimal.Zero;

            return data.TakeLast(windowSize).Average(d => d.Temperature);
        }
    }
}
