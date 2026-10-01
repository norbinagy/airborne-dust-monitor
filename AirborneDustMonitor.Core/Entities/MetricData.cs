namespace AirborneDustMonitor.Core.Entities
{
    public record MetricData(
        MetricType MetricType,
        decimal Value,
        DateTime Date,
        int SensorID,
        IReadOnlyDictionary<StatisticCalculatorType, decimal> Statistics
    );
}
