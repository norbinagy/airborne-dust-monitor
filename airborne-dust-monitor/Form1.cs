using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace airborne_dust_monitor
{
    public partial class Form1 : Form
    {
        private DatabaseManager databaseManager;
        private int entryId;
        private double humidityThreshold = 0;

        public Form1()
        {
            InitializeComponent();
            InitializeChart();
        }

        private void InitializeChart()
        {
            // Set up chart properties
            chart1.ChartAreas.Add(new ChartArea("HumidityChartArea"));
            chart1.Series.Add(new Series("HumiditySeries"));
            chart1.Series["HumiditySeries"].ChartType = SeriesChartType.Line;
            chart1.Series["HumiditySeries"].BorderWidth = 2;
        }

        private void UpdateChartWithTestData(TestSensorData sensorData)
        {

            double humidity = sensorData.humidity;
            
            // Add data point to the chart
            chart1.Series["HumiditySeries"].Points.AddY(humidity);

            // Limit number of points displayed
            if (chart1.Series["HumiditySeries"].Points.Count > 20)
            {
                chart1.Series["HumiditySeries"].Points.RemoveAt(0);
            }

            // Check if humidity exceeds threshold
            if (humidityThreshold > 0 && humidity >= humidityThreshold)
            {
                ShowPopup($"Humidity reached {humidity}%!");
            }
        }

        private void ShowPopup(string message)
        {
            MessageBox.Show(message, "Humidity Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            databaseManager.QueryLatest();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TestSensorData sensorData = new TestSensorData();
            sensorData = databaseManager.QueryByEntryID(entryId.ToString());
            UpdateChartWithTestData(sensorData);
            entryId++;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            databaseManager = new DatabaseManager();
            entryId = 207;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            humidityThreshold = (double)numericUpDown1.Value;
        }
    }
}
