using System;

namespace airborne_dust_monitor
{
    internal class SensorData
    {
        public string ProcessStatus { get; set; }
        public int SensorID { get; set; }
        public DateTime Date { get; set; }
        public float ParticulateMatter { get; set; }
        public float Temperature { get; set; }
        public int Humidity { get; set; }
        public float BatteryVoltage { get; set; }
        public int MeasureInterval { get; set; }
    }
}
