using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        private double minHumidity = 100;
        private double maxHumidity = 0;
        private int chartIndex = 0;
        private DateTime lastQueryTime;

        public Form1()
        {
            InitializeComponent();
            InitializeCharts();
        }

        private void InitializeCharts()
        {
            
            chart1.Series.Clear();
            chart1.ChartAreas.Add(new ChartArea("ParticulateMatterChartArea"));
            for (int i = 0; i < 9; i++)
            {
                chart1.Series.Add(new Series("ParticulateMatterSeries"+i));
                chart1.Series["ParticulateMatterSeries"+i].ChartType = SeriesChartType.Line;
                chart1.Series["ParticulateMatterSeries"+i].BorderWidth = 2;
                chart1.Series["ParticulateMatterSeries"+i].LegendText = "Szállópor"+i;
            }

            chart2.Series.Clear();
            chart2.ChartAreas.Add(new ChartArea("TemperatureChartArea"));
            for (int i = 0;i < 9;i++)
            {
                chart2.Series.Add(new Series("TemperatureSeries" + i));
                chart2.Series["TemperatureSeries"+i].ChartType = SeriesChartType.Line;
                chart2.Series["TemperatureSeries" + i].BorderWidth = 2;
                chart2.Series["TemperatureSeries"+i].LegendText = "Hőmérséklet"+i;
            }
            

            chart3.Series.Clear();
            chart3.ChartAreas.Add(new ChartArea("HumidityChartArea"));
            for (int i = 0; i< 9; i++)
            {
                chart3.Series.Add(new Series("HumiditySeries" + i));
                chart3.Series["HumiditySeries" + i].ChartType = SeriesChartType.Line;
                chart3.Series["HumiditySeries" + i].BorderWidth = 2;
                chart3.Series["HumiditySeries" + i].LegendText = "Páratartalom" + i;
            }

            chart4.Series.Clear();
            chart4.ChartAreas.Add(new ChartArea("BatteryVoltageChartArea"));
            for (int i = 0; i < 9; i++)
            {
                chart4.Series.Add(new Series("BatteryVoltageSeries" + i));
                chart4.Series["BatteryVoltageSeries" + i].ChartType = SeriesChartType.Line;
                chart4.Series["BatteryVoltageSeries" + i].BorderWidth = 2;
                chart4.Series["BatteryVoltageSeries" + i].LegendText = "Bat Voltage" + i;
            }
        }

        private void UpdateChartsWithData(List<SensorData> sensorDataList)
        {
            foreach (SensorData sensorData in sensorDataList)
            {
                chart1.Series["ParticulateMatterSeries"+sensorData.SensorID].Points.AddXY(chartIndex, sensorData.ParticulateMatter);
                if (chart1.Series["ParticulateMatterSeries"+sensorData.SensorID].Points.Count > 20)
                {
                    chart1.Series["ParticulateMatterSeries" + sensorData.SensorID].Points.RemoveAt(0);
                }

                chart2.Series["TemperatureSeries"+sensorData.SensorID].Points.AddXY(chartIndex, sensorData.Temperature);
                if (chart2.Series["TemperatureSeries" + sensorData.SensorID].Points.Count > 20)
                {
                    chart2.Series["TemperatureSeries"+sensorData.SensorID].Points.RemoveAt(0);
                }

                chart3.Series["HumiditySeries" + sensorData.SensorID].Points.AddXY(chartIndex, sensorData.Humidity);
                if (chart3.Series["HumiditySeries" + sensorData.SensorID].Points.Count > 20)
                {
                    chart3.Series["HumiditySeries" + sensorData.SensorID].Points.RemoveAt(0);
                }

                chart4.Series["BatteryVoltageSeries" + sensorData.SensorID].Points.AddXY(chartIndex, sensorData.BatteryVoltage);
                if (chart4.Series["BatteryVoltageSeries" + sensorData.SensorID].Points.Count > 20)
                {
                    chart4.Series["BatteryVoltageSeries" + sensorData.SensorID].Points.RemoveAt(0);
                }
            }

            chartIndex++;
            //if (humidityThreshold > 0 && humidity >= humidityThreshold)
            //{
            //    ShowPopup($"Páratartalom: {humidity}%!");
            //}

            //if (humidity < minHumidity)
            //{
            //    minHumidity = humidity;
            //    label2.Text = "Min: " + minHumidity;
            //}

            //if (humidity > maxHumidity)
            //{
            //    maxHumidity = humidity;
            //    label3.Text = "Max: " + maxHumidity;
            //}
        }

        private void ShowPopup(string message)
        {
            MessageBox.Show(message, "Figyelem!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            //TestSensorData sensorData = new TestSensorData();
            //sensorData = databaseManager.QueryByEntryID(entryId.ToString());
            //UpdateChartWithTestData(sensorData);
            //entryId++;
            List<SensorData> sensorData = databaseManager.TestQueryByDate(lastQueryTime.ToString("yyyy.MM.dd H:mm"));
            UpdateChartsWithData(sensorData);
            lastQueryTime = lastQueryTime.AddMinutes(1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            databaseManager = new DatabaseManager();
            entryId = 207;
            lastQueryTime = DateTime.Parse("2024.03.27 13:56");
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            humidityThreshold = (double)numericUpDown1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            minHumidity = 100;
            maxHumidity = 0;
            label2.Text = "Min:";
            label3.Text = "Max:";
        }
    }
}
