namespace AirborneDustMonitor.Core
{
    public class SensorData
    {
        public required string ProcessStatus { get; set; }
        public int SensorID { get; set; }
        public DateTime Date { get; set; }
        public decimal ParticulateMatter { get; set; }
        public decimal Temperature { get; set; }
        public int Humidity { get; set; }
        public decimal BatteryVoltage { get; set; }
        public int MeasureInterval { get; set; }
    }
}
