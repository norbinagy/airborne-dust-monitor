namespace AirborneDustMonitor.Core.Entities
{
    public class Alert
    {
        public string Message { get; }
        public MetricType MetricType { get; }
        public int SensorID { get; }
        public DateTime Date { get; }
        public AlertType Type { get; }
        public AlertStatus Status { get; }

        public Alert(string message, MetricType metricType, int sensorID, AlertType type, AlertStatus status)
        {
            Message = message;
            MetricType = metricType;
            SensorID = sensorID;
            Date = DateTime.Now;
            Type = type;
            Status = status;
        }
    }
}
