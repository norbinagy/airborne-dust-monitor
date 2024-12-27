using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace airborne_dust_monitor
{
    internal static class ThresholdManager
    {
        private static float particulateMatterThreshold;
        private static float temperatureThreshold;
        private static int humidityThreshold;
        private static float batteryVoltageThreshold;

        public static void CheckThresholdValues(SensorData sensorData, Chart particulateMatterChart, Chart temperatureChart, Chart humidityChart, Chart batteryVoltageChart)
        {
            if (sensorData != null)
            {
                if (ParticulateMatterThreshold != 0 && sensorData.ParticulateMatter > ParticulateMatterThreshold)
                {
                    particulateMatterChart.Series["ParticulateMatterSeries" + sensorData.SensorID].Color = System.Drawing.Color.Red;
                }
                if (TemperatureThreshold != 0 && sensorData.Temperature > TemperatureThreshold)
                {
                    temperatureChart.Series["TemperatureSeries" + sensorData.SensorID].Color = System.Drawing.Color.Red;
                }
                if (HumidityThreshold != 0 && sensorData.Humidity > HumidityThreshold)
                {
                    humidityChart.Series["HumiditySeries" + sensorData.SensorID].Color = System.Drawing.Color.Red;
                }
                if (BatteryVoltageThreshold != 0 && sensorData.BatteryVoltage > BatteryVoltageThreshold)
                {
                    batteryVoltageChart.Series["BatteryVoltageSeries" + sensorData.SensorID].Color = System.Drawing.Color.Red;
                }
            }
        }

        public static float ParticulateMatterThreshold
        {
            get => particulateMatterThreshold;
            set
            {
                if (value > 0)
                {
                    particulateMatterThreshold = value;
                }
            }
        }

        public static float TemperatureThreshold
        {
            get => temperatureThreshold;
            set
            {
                if (value > -50 && value < 50)
                {
                    temperatureThreshold = value;
                }
            }
        }

        public static int HumidityThreshold
        {
            get => humidityThreshold;
            set
            {
                if (value >= 0 && value <= 100)
                {
                    humidityThreshold = value;
                }
            }
        }

        public static float BatteryVoltageThreshold
        {
            get => batteryVoltageThreshold;
            set
            {
                if (value > 3 && value < 5)
                {
                    batteryVoltageThreshold = value;
                }
            }
        }
    }
}
