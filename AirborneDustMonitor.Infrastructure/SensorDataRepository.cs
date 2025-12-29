using AirborneDustMonitor.Core;
using System.Globalization;
using Microsoft.Data.SqlClient;

namespace AirborneDustMonitor.Infrastructure
{
    public class SensorDataRepository : ISensorDataRepository
    {
        private readonly string _connectionString;

        public SensorDataRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<SensorData>> QueryByDateAsync(DateTime date)
        {
            var result = new List<SensorData>();

            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = @"
                           SELECT *
                           FROM RawMeasurements
                           WHERE [process-status] = 'Success' AND [ttn-received-at] = @date
                         ";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@date", System.Data.SqlDbType.DateTime2).Value = date;

            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(MapFromReader(reader));
            }

            return result;
        }

        public async Task<IEnumerable<SensorData>> QueryAfterDateAsync(DateTime date)
        {
            var result = new List<SensorData>();

            using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            string sql = @"
                           SELECT *
                           FROM RawMeasurements
                           WHERE [process-status] = 'Success' AND [ttn-received-at] > @date
                           ORDER BY [ttn-received-at] ASC
                         ";

            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@date", System.Data.SqlDbType.DateTime2).Value = date;

            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(MapFromReader(reader));
            }

            return result;
        }

        private SensorData MapFromReader(SqlDataReader reader)
        {
            return new SensorData
            {
                ProcessStatus = reader["process-status"].ToString()!,
                SensorID = Convert.ToInt32(reader["diversen-id"]),
                Date = Convert.ToDateTime(reader["ttn-received-at"]),
                ParticulateMatter = Convert.ToDecimal(reader["particulate-matter"], CultureInfo.InvariantCulture),
                Temperature = Convert.ToDecimal(reader["temperature"], CultureInfo.InvariantCulture),
                Humidity = Convert.ToInt32(reader["humidity"]),
                BatteryVoltage = Convert.ToDecimal(reader["bat-voltage"], CultureInfo.InvariantCulture),
                MeasureInterval = Convert.ToInt32(reader["measure-interval"])
            };
        }
    }
}
