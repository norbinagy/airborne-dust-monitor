using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirborneDustMonitor.Core
{
    public class SensorDataService
    {
        private readonly ISensorDataRepository _repository;
        private readonly SensorDataCache _cache;
        private DateTime _lastSeenDate;

        public SensorDataService(ISensorDataRepository repository, SensorDataCache cache)
        {
            _repository = repository;
            _cache = cache;
            _lastSeenDate = DateTime.MinValue;
        }

        public async Task UpdateCacheAsync()
        {
            IEnumerable<SensorData> latest = await _repository.QueryAfterDateAsync(_lastSeenDate);
            if(latest.Any())
            {
                foreach(SensorData item in latest)
                {
                    _cache.Add(item);
                }
                _lastSeenDate = latest.Max(d => d.Date);
            }
        }

        public async Task UpdateCacheByDateAsync(DateTime date)
        {
            IEnumerable<SensorData> result = await _repository.QueryByDateAsync(date);
            if(result.Any())
            {
                foreach (var item in result)
                {
                    _cache.Add(item);
                }
            }
        }

        public IEnumerable<SensorData> GetCachedData()
        {
            return _cache.GetAll();
        }
    }
}
