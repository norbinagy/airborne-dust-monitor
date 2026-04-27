using AirborneDustMonitor.Core.Entities;
using AirborneDustMonitor.Core.Interfaces;
using AirborneDustMonitor.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AirborneDustMonitor.Infrastructure
{

    public class SensorDb : ISensorDb
    {
        private readonly SensorDbContext _context;

        public SensorDb(SensorDbContext context)
        {
            _context = context;
        }

        public Task<List<Sensor>> GetSensorAfterDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Sensor>> GetSensorByDateAsync(DateTime date)
        {
            var result = await _context.RawMeasurements
                .Where(s => s.ProcessStatus == "Success" && s.Date == date)
                .ToListAsync();
            return result;
        }
    }
}
