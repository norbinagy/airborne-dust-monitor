using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AirborneDustMonitor.Infrastructure.Configurations
{
    public class SensorDbContext : DbContext
    {
        public DbSet<Sensor> RawMeasurements { get; set; }
        private readonly string? _connectionString;

        public SensorDbContext(IAppSettingsService appSettingsService)
        {
            _connectionString = appSettingsService.Current.Database.ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new SensorEntityTypeConfiguration().Configure(modelBuilder.Entity<Sensor>());
        }
    }
}
