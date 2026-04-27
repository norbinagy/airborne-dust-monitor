namespace AirborneDustMonitor.Core.Entities
{
    public record MetricData(
        MetricType MetricType,
        decimal Value,
        DateTime Date,
        int SensorID,
        decimal Min,
        decimal Max,
        decimal MovingAverage
    );
}
