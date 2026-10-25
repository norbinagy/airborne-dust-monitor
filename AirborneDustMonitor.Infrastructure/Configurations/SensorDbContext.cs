using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Interfaces.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AirborneDustMonitor.Infrastructure.Configurations
{
    public class SensorDbContext : DbContext
    {
        public DbSet<Sensor> RawMeasurements { get; set; }
        private readonly string? _connectionString;
        private readonly ILogger<SensorDbContext> _logger;

        public SensorDbContext(IAppSettingsService appSettingsService, ILogger<SensorDbContext> logger)
        {
            _connectionString = appSettingsService.Current.Database.ConnectionString;
            _logger = logger;
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
