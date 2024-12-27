using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;

namespace airborne_dust_monitor
{
    internal class DatabaseManager
    {
        private readonly string connectionString;

        public DatabaseManager()
        {
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nagyn\Source\Repos\norbinagy\airborne-dust-monitor\airborne-dust-monitor\TestDatabaseFinal.mdf;Integrated Security=True";
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
