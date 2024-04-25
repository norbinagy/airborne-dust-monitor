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

        public TestSensorData QueryByEntryID(string entryID)
        {
            TestSensorData sensorData = new TestSensorData();
            string query = "SELECT created_at, entry_id, TEMPERATURE, HUMIDITY FROM TestHumidity WHERE entry_id = " + entryID;

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
                            string entryId = reader.GetString(1);
                            string temperature = reader.GetString(2);
                            string humidity = reader.GetString(3);

                            sensorData.createdAt = DateTime.Parse(createdAt);
                            sensorData.entryId = Int32.Parse(entryId);
                            sensorData.temperature = Double.Parse(temperature, CultureInfo.InvariantCulture);
                            sensorData.humidity = Double.Parse(humidity, CultureInfo.InvariantCulture);

                            Console.WriteLine("Record:");
                            Console.WriteLine("Created At: " + sensorData.createdAt);
                            Console.WriteLine("Entry ID: " + sensorData.entryId);
                            Console.WriteLine("Temperature: " + sensorData.temperature);
                            Console.WriteLine("Humidity: " + sensorData.humidity);
                        }
                        else
                        {
                            Console.WriteLine("No records found.");
                        }
                    }
                }
            }
            return sensorData;
        }

        public void TestQuery(string date)
        {
            SensorData sensorData = new SensorData();
            string query = "SELECT * FROM TestTable WHERE ttn-recieved-at = " + date;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sensorData.ProcessStatus = reader.GetString(0);
                            sensorData.SensorID = int.Parse(reader.GetString(1));
                            sensorData.Date = DateTime.Parse(reader.GetString(2));
                            sensorData.ParticulateMatter = float.Parse(reader.GetString(3));
                            sensorData.Temperature = float.Parse(reader.GetString(4));
                            sensorData.Humidity = int.Parse(reader.GetString(5));
                            sensorData.BatteryVoltage = float.Parse(reader.GetString(6));
                            sensorData.MeasureInterval = int.Parse(reader.GetString(7));

                            Console.WriteLine("Record: " + sensorData.ProcessStatus);
                        }
                        else
                        {
                            Console.WriteLine("No records found.");
                        }
                    }
                }
            }
        }
    }
}
