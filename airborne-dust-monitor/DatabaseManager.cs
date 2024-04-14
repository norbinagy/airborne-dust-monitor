using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
