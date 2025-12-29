using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirborneDustMonitor.Core
{
    public interface ISensorDataRepository
    {
        Task<IEnumerable<SensorData>> QueryByDateAsync(DateTime date);
        Task<IEnumerable<SensorData>> QueryAfterDateAsync(DateTime date);
    }
}
