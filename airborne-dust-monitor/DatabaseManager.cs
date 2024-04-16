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
        private string connectionString;

        public DatabaseManager()
        {
            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\nagyn\source\repos\airborne-dust-monitor\airborne-dust-monitor\TestDatabase.mdf;Integrated Security=True";
        }

        public void QueryLatest()
        {
            // SQL query to retrieve the latest record
            string query = "SELECT TOP 1 created_at, HUMIDITY, SENSOR_ID FROM TestHumidity ORDER BY created_at DESC";

            // Create and open a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a command to execute the query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Execute the query and retrieve the data
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if there is any data
                        if (reader.Read())
                        {
                            // Retrieve values from the reader
                            string createdAt = reader.GetString(0);
                            string humidity = reader.GetString(1);
                            string sensorId = reader.GetString(2);

                            // Output the retrieved data
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

            // SQL query to retrieve the latest record
            string query = "SELECT created_at, entry_id, TEMPERATURE, HUMIDITY FROM TestHumidity WHERE entry_id = " + entryID;

            // Create and open a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a command to execute the query
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Execute the query and retrieve the data
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if there is any data
                        if (reader.Read())
                        {
                            // Retrieve values from the reader
                            string createdAt = reader.GetString(0);
                            string entryId = reader.GetString(1);
                            string temperature = reader.GetString(2);
                            string humidity = reader.GetString(3);

                            sensorData.createdAt = DateTime.Parse(createdAt);
                            sensorData.entryId = Int32.Parse(entryId);
                            sensorData.temperature = Double.Parse(temperature, CultureInfo.InvariantCulture);
                            sensorData.humidity = Double.Parse(humidity, CultureInfo.InvariantCulture);

                            // Output the retrieved data
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

        }
}
