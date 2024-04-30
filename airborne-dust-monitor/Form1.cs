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
        private DateTime lastQueryTime;
        private Dictionary<string, (float min, float max)> minMaxValues;
        private List<SensorData> sensorDataList = new List<SensorData>();
        private List<SensorData> sensorDataListFull = new List<SensorData>();
        private MovingAverageCalculator movingAverageCalculator = new MovingAverageCalculator(3);
        private Dictionary<string, (double particle, double temp, double humidity, double voltage)> movingAverages;

        public Form1()
        {
            InitializeComponent();
            InitializeCharts();
        }

        private void InitializeCharts()
        {
            chart1.Titles.Add("Szálló por");
            chart1.Series.Clear();
            chart1.ChartAreas.Add(new ChartArea("ParticulateMatterChartArea"));
            chart1.ChartAreas[0].AxisX.Interval = 1;
            for (int i = 0; i < 9; i++)
            {
                chart1.Series.Add(new Series("ParticulateMatterSeries"+i));
                chart1.Series["ParticulateMatterSeries"+i].ChartType = SeriesChartType.Line;
                chart1.Series["ParticulateMatterSeries"+i].BorderWidth = 2;
                chart1.Series["ParticulateMatterSeries"+i].LegendText = "Szenzor"+i;
            }

            chart2.Titles.Add("Hőmérséklet (°C)");
            chart2.Series.Clear();
            chart2.ChartAreas.Add(new ChartArea("TemperatureChartArea"));
            chart2.ChartAreas[0].AxisX.Interval = 1;
            for (int i = 0;i < 9;i++)
            {
                chart2.Series.Add(new Series("TemperatureSeries" + i));
                chart2.Series["TemperatureSeries"+i].ChartType = SeriesChartType.Line;
                chart2.Series["TemperatureSeries" + i].BorderWidth = 2;
                chart2.Series["TemperatureSeries"+i].LegendText = "Szenzor"+i;
            }
            
            chart3.Titles.Add("Páratartalom (%)");
            chart3.Series.Clear();
            chart3.ChartAreas.Add(new ChartArea("HumidityChartArea"));
            chart3.ChartAreas[0].AxisX.Interval = 1;
            for (int i = 0; i< 9; i++)
            {
                chart3.Series.Add(new Series("HumiditySeries" + i));
                chart3.Series["HumiditySeries" + i].ChartType = SeriesChartType.Line;
                chart3.Series["HumiditySeries" + i].BorderWidth = 2;
                chart3.Series["HumiditySeries" + i].LegendText = "Szenzor" + i;
            }

            chart4.Titles.Add("Akkumulátor feszültség (V)");
            chart4.Series.Clear();
            chart4.ChartAreas.Add(new ChartArea("BatteryVoltageChartArea"));
            chart4.ChartAreas[0].AxisX.Interval = 1;
            for (int i = 0; i < 9; i++)
            {
                chart4.Series.Add(new Series("BatteryVoltageSeries" + i));
                chart4.Series["BatteryVoltageSeries" + i].ChartType = SeriesChartType.Line;
                chart4.Series["BatteryVoltageSeries" + i].BorderWidth = 2;
                chart4.Series["BatteryVoltageSeries" + i].LegendText = "Szenzor" + i;
            }
        }

        private void UpdateChartsWithData(List<SensorData> sensorDataList)
        {
            foreach (SensorData sensorData in sensorDataList)
            {
                if (sensorData != null)
                {
                    chart2.ChartAreas[0].AxisX.Interval = 1;
                    chart1.Series["ParticulateMatterSeries" + sensorData.SensorID].Points.AddXY(sensorData.Date.ToShortTimeString(), sensorData.ParticulateMatter);
                    if (chart1.Series["ParticulateMatterSeries" + sensorData.SensorID].Points.Count > 11)
                    {
                        chart1.Series["ParticulateMatterSeries" + sensorData.SensorID].Points.RemoveAt(0);
                    }

                    chart2.Series["TemperatureSeries" + sensorData.SensorID].Points.AddXY(sensorData.Date.ToShortTimeString(), sensorData.Temperature);
                    if (chart2.Series["TemperatureSeries" + sensorData.SensorID].Points.Count > 11)
                    {
                        chart2.Series["TemperatureSeries" + sensorData.SensorID].Points.RemoveAt(0);
                    }

                    chart3.Series["HumiditySeries" + sensorData.SensorID].Points.AddXY(sensorData.Date.ToShortTimeString(), sensorData.Humidity);
                    if (chart3.Series["HumiditySeries" + sensorData.SensorID].Points.Count > 11)
                    {
                        chart3.Series["HumiditySeries" + sensorData.SensorID].Points.RemoveAt(0);
                    }

                    chart4.Series["BatteryVoltageSeries" + sensorData.SensorID].Points.AddXY(sensorData.Date.ToShortTimeString(), sensorData.BatteryVoltage);
                    if (chart4.Series["BatteryVoltageSeries" + sensorData.SensorID].Points.Count > 11)
                    {
                        chart4.Series["BatteryVoltageSeries" + sensorData.SensorID].Points.RemoveAt(0);
                    }
                }
            }
        }

        private void ShowPopup(string message)
        {
            MessageBox.Show(message, "Figyelem!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            sensorDataList = databaseManager.TestQueryByDate(lastQueryTime.ToString("yyyy.MM.dd H:mm"));
            sensorDataListFull.AddRange(sensorDataList);
            UpdateChartsWithData(sensorDataList);
            UpdateMinMaxValues(sensorDataList);
            UpdateMinMaxLabels();
            UpdateMovingAverages(sensorDataListFull);
            UpdateMovingAverageLabels();
            lastQueryTime = lastQueryTime.AddMinutes(1);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            databaseManager = new DatabaseManager();
            lastQueryTime = DateTime.Parse("2024.03.29 3:59");
            minMaxValues = new Dictionary<string, (float min, float max)>()
            {
                { "ParticulateMatter", (min: 100, max: 0) },
                { "Temperature", (min: 100, max: 0) },
                { "Humidity", (min: 100, max: 0) },
                { "BatteryVoltage", (min: 100, max: 0) }
            };
            movingAverages = new Dictionary<string, (double particle, double temp, double humidity, double voltage)>()
            {
                { "Sensor0", (particle: 0, temp: 0, humidity: 0, voltage: 0) },
                { "Sensor1", (particle: 0, temp: 0, humidity: 0, voltage: 0) },
                { "Sensor2", (particle: 0, temp: 0, humidity: 0, voltage: 0) },
                { "Sensor3", (particle: 0, temp: 0, humidity: 0, voltage: 0) },
                { "Sensor4", (particle: 0, temp: 0, humidity: 0, voltage: 0) },
                { "Sensor5", (particle: 0, temp: 0, humidity: 0, voltage: 0) },
                { "Sensor6", (particle: 0, temp: 0, humidity: 0, voltage: 0) },
                { "Sensor7", (particle: 0, temp: 0, humidity: 0, voltage: 0) },
                { "Sensor8", (particle: 0, temp: 0, humidity: 0, voltage: 0) }
            };
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            label2.Text = "Min:";
            label3.Text = "Max:";
        }


        private void UpdateMinMaxValues(List<SensorData> sensorDataList)
        {
            foreach (SensorData sensorData in sensorDataList)
            {
                if (sensorData != null)
                {
                    var (minParticulateMatter, maxParticulateMatter) = minMaxValues["ParticulateMatter"];
                    if(sensorData.ParticulateMatter < minParticulateMatter)
                    {
                        minMaxValues["ParticulateMatter"] = (sensorData.ParticulateMatter, maxParticulateMatter);
                    }
                    if(sensorData.ParticulateMatter > maxParticulateMatter)
                    {
                        minMaxValues["ParticulateMatter"] = (minParticulateMatter, sensorData.ParticulateMatter);
                    }

                    var (minTemperature, maxTemperature) = minMaxValues["Temperature"];
                    if (sensorData.Temperature < minTemperature)
                    {
                        minMaxValues["Temperature"] = (sensorData.Temperature, maxTemperature);
                    }
                    if (sensorData.Temperature > maxTemperature)
                    {
                        minMaxValues["Temperature"] = (minTemperature, sensorData.Temperature);
                    }

                    var (minHumidity, maxHumidity) = minMaxValues["Humidity"];
                    if (sensorData.Humidity < minHumidity)
                    {
                        minMaxValues["Humidity"] = (sensorData.Humidity, maxHumidity);
                    }
                    if (sensorData.Humidity > maxHumidity)
                    {
                        minMaxValues["Humidity"] = (minHumidity, sensorData.Humidity);
                    }

                    var (minBatteryVoltage, maxBatteryVoltage) = minMaxValues["BatteryVoltage"];
                    if (sensorData.BatteryVoltage < minBatteryVoltage)
                    {
                        minMaxValues["BatteryVoltage"] = (sensorData.BatteryVoltage, maxBatteryVoltage);
                    }
                    if (sensorData.BatteryVoltage > maxBatteryVoltage)
                    {
                        minMaxValues["BatteryVoltage"] = (minBatteryVoltage, sensorData.BatteryVoltage);
                    }
                }
            }
        }

        private void UpdateMinMaxLabels()
        {
            label2.Text = "Min: " + minMaxValues["ParticulateMatter"].min;
            label3.Text = "Max: " + minMaxValues["ParticulateMatter"].max;
            label4.Text = "Min: " + minMaxValues["Temperature"].min;
            label5.Text = "Max: " + minMaxValues["Temperature"].max;
            label6.Text = "Min: " + minMaxValues["Humidity"].min;
            label7.Text = "Max: " + minMaxValues["Humidity"].max;
            label8.Text = "Min: " + minMaxValues["BatteryVoltage"].min;
            label9.Text = "Max: " + minMaxValues["BatteryVoltage"].max;
        }

        private void UpdateMovingAverages(List<SensorData> sensorDataList)
        {
            foreach (SensorData sensorData in sensorDataList)
            {
                if (sensorData != null)
                {
                    movingAverages["Sensor" + sensorData.SensorID] = (
                        particle: movingAverageCalculator.AddDataPoint(sensorData.ParticulateMatter),
                        temp: movingAverageCalculator.AddDataPoint(sensorData.Temperature),
                        humidity: movingAverageCalculator.AddDataPoint(sensorData.Humidity),
                        voltage: movingAverageCalculator.AddDataPoint(sensorData.BatteryVoltage)
                    );
                }
            }
        }

        private void UpdateMovingAverageLabels()
        {
            for(int i=0; i<9; i++)
            {
                chart1.Series["ParticulateMatterSeries" + i].LegendText = $"Szenzor{i} ({movingAverages["Sensor"+i].particle.ToString("0.00")})";
                chart2.Series["TemperatureSeries" + i].LegendText = $"Szenzor{i} ({movingAverages["Sensor" + i].temp.ToString("0.00")})";
                chart3.Series["HumiditySeries" + i].LegendText = $"Szenzor{i} ({movingAverages["Sensor" + i].humidity.ToString("0.00")})";
                chart4.Series["BatteryVoltageSeries" + i].LegendText = $"Szenzor{i} ({movingAverages["Sensor" + i].voltage.ToString("0.00")})";
            }
        }
    }
}
