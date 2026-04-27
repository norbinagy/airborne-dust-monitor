using AirborneDustMonitor.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Globalization;

namespace AirborneDustMonitor.Infrastructure.Configurations
{
    public class SensorEntityTypeConfiguration : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            builder
                .ToTable("RawMeasurements")
                .HasNoKey();

            builder
                .Property(s => s.ProcessStatus)
                .HasColumnName("process-status")
                .HasColumnType("varchar(50)");

            builder
                .Property(s => s.SensorID)
                .HasColumnName("diversen-id")
                .HasColumnType("varchar(50)");

            builder
                .Property(s => s.Date)
                .HasColumnName("ttn-received-at")
                .HasColumnType("varchar(50)")
                .HasConversion(
                    v => v.ToString("yyyy-MM-dd HH:mm:ss.fffffff"),
                    v => DateTime.ParseExact(v, "yyyy-MM-dd HH:mm:ss.fffffff", CultureInfo.InvariantCulture));

            builder
                .Property(s => s.ParticulateMatter)
                .HasColumnName("particulate-matter")
                .HasColumnType("varchar(50)");

            builder
                .Property(s => s.Temperature)
                .HasColumnName("temperature")
                .HasColumnType("varchar(50)");

            builder
                .Property(s => s.Humidity)
                .HasColumnName("humidity")
                .HasColumnType("varchar(50)");

            builder
                .Property(s => s.BatteryVoltage)
                .HasColumnName("bat-voltage")
                .HasColumnType("varchar(50)");

            builder
                .Property(s => s.MeasureInterval)
                .HasColumnName("measure-interval")
                .HasColumnType("varchar(50)");
        }
    }
}
