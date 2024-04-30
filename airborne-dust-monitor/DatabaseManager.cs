using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace airborne_dust_monitor
{
    internal class DatabaseManager
    {
        private readonly string connectionStringOld;
        private readonly string connectionString;

        public DatabaseManager()
        {
            connectionStringOld = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nagyn\source\repos\airborne-dust-monitor\airborne-dust-monitor\TestDatabase.mdf;Integrated Security=True";
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nagyn\source\repos\airborne-dust-monitor\airborne-dust-monitor\TestDatabaseFinal.mdf;Integrated Security=True";
        }

        public void QueryLatest()
        {
            string query = "SELECT TOP 1 created_at, HUMIDITY, SENSOR_ID FROM TestHumidity ORDER BY created_at DESC";

            using (SqlConnection connection = new SqlConnection(connectionStringOld))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string createdAt = reader.GetString(0);
                            string humidity = reader.GetString(1);
                            string sensorId = reader.GetString(2);

                            Console.WriteLine("Latest Record:");
                            Console.WriteLine("Created At: " + createdAt);
                            Console.WriteLine("Humidity: " + humidity);
                            Console.WriteLine("Sensor ID: " + sensorId);
                        }
                        else
                        {
                            Console.WriteLine("No records found.");
                        }
                    }
                }
            }
        }

        public List<SensorData> TestQueryByDate(string date)
        {
            List<SensorData> sensorDataList = new List<SensorData>();
            string query = $"SELECT * FROM TestTable WHERE \"process-status\" = 'Success' AND \"ttn-received-at\" = '{date}';";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                SensorData sensorData = new SensorData();
                                sensorData.ProcessStatus = reader.GetString(0);
                                sensorData.SensorID = int.Parse(reader.GetString(1));
                                sensorData.Date = DateTime.Parse(reader.GetString(2));
                                sensorData.ParticulateMatter = float.Parse(reader.GetString(3), CultureInfo.InvariantCulture);
                                sensorData.Temperature = float.Parse(reader.GetString(4), CultureInfo.InvariantCulture);
                                sensorData.Humidity = int.Parse(reader.GetString(5));
                                sensorData.BatteryVoltage = float.Parse(reader.GetString(6), CultureInfo.InvariantCulture);
                                sensorData.MeasureInterval = int.Parse(reader.GetString(7));
                                sensorDataList.Add(sensorData);

                                Console.WriteLine("Record: " + sensorData.ProcessStatus);
                                Console.WriteLine("Sensor ID: " + sensorData.SensorID);
                                Console.WriteLine("Date: " + sensorData.Date);
                                Console.WriteLine("Particulate Matter: " + sensorData.ParticulateMatter);
                                Console.WriteLine("Temperature: " + sensorData.Temperature);
                                Console.WriteLine("Humidity: " + sensorData.Humidity);
                                Console.WriteLine("Battery Voltage: " + sensorData.BatteryVoltage);
                                Console.WriteLine("Measure Interval: " + sensorData.MeasureInterval);
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("No records found.");
                        }
                    }
                }
            }
            return sensorDataList;
        }
    }
}
