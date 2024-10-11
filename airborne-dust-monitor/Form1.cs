using System;
using System.Collections.Generic;
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
        private List<SensorData> sensorDataList;
        private Dictionary<string, (MovingAverageCalculator particle, MovingAverageCalculator temp, MovingAverageCalculator humidity, MovingAverageCalculator voltage)> movingAverages;
        private int movingAverageWindow;
        private float particulateMatterThreshold;
        private float temperatureThreshold;
        private int humidityThreshold;
        private float batteryVoltageThreshold;
        private bool alertOnCooldown;

        public Form1()
        {
            InitializeComponent();
            InitializeCharts();
            alertOnCooldown = false;
            databaseManager = new DatabaseManager();
            lastQueryTime = DateTime.Parse("2024.03.29 3:59");
            sensorDataList = new List<SensorData>();
            minMaxValues = new Dictionary<string, (float min, float max)>()
            {
                { "ParticulateMatter", (min: float.MaxValue, max: float.MinValue) },
                { "Temperature", (min: float.MaxValue, max: float.MinValue) },
                { "Humidity", (min : float.MaxValue , max : float.MinValue) },
                { "BatteryVoltage", (min: float.MaxValue, max: float.MinValue) }
            };
            movingAverageWindow = 6;
            movingAverages = new Dictionary<string, (MovingAverageCalculator particle, MovingAverageCalculator temp, MovingAverageCalculator humidity, MovingAverageCalculator voltage)>()
            {
                { "Sensor0", (particle: new MovingAverageCalculator(movingAverageWindow), temp: new MovingAverageCalculator(movingAverageWindow), humidity: new MovingAverageCalculator(movingAverageWindow), voltage: new MovingAverageCalculator(movingAverageWindow)) },
                { "Sensor1", (particle: new MovingAverageCalculator(movingAverageWindow), temp: new MovingAverageCalculator(movingAverageWindow), humidity: new MovingAverageCalculator(movingAverageWindow), voltage: new MovingAverageCalculator(movingAverageWindow)) },
                { "Sensor2", (particle: new MovingAverageCalculator(movingAverageWindow), temp: new MovingAverageCalculator(movingAverageWindow), humidity: new MovingAverageCalculator(movingAverageWindow), voltage: new MovingAverageCalculator(movingAverageWindow)) },
                { "Sensor3", (particle: new MovingAverageCalculator(movingAverageWindow), temp: new MovingAverageCalculator(movingAverageWindow), humidity: new MovingAverageCalculator(movingAverageWindow), voltage: new MovingAverageCalculator(movingAverageWindow)) },
                { "Sensor4", (particle: new MovingAverageCalculator(movingAverageWindow), temp: new MovingAverageCalculator(movingAverageWindow), humidity: new MovingAverageCalculator(movingAverageWindow), voltage: new MovingAverageCalculator(movingAverageWindow)) },
                { "Sensor5", (particle: new MovingAverageCalculator(movingAverageWindow), temp: new MovingAverageCalculator(movingAverageWindow), humidity: new MovingAverageCalculator(movingAverageWindow), voltage: new MovingAverageCalculator(movingAverageWindow)) },
                { "Sensor6", (particle: new MovingAverageCalculator(movingAverageWindow), temp: new MovingAverageCalculator(movingAverageWindow), humidity: new MovingAverageCalculator(movingAverageWindow), voltage: new MovingAverageCalculator(movingAverageWindow)) },
                { "Sensor7", (particle: new MovingAverageCalculator(movingAverageWindow), temp: new MovingAverageCalculator(movingAverageWindow), humidity: new MovingAverageCalculator(movingAverageWindow), voltage: new MovingAverageCalculator(movingAverageWindow)) },
                { "Sensor8", (particle: new MovingAverageCalculator(movingAverageWindow), temp: new MovingAverageCalculator(movingAverageWindow), humidity: new MovingAverageCalculator(movingAverageWindow), voltage: new MovingAverageCalculator(movingAverageWindow)) }
            };
        }

        private void InitializeCharts()
        {
            chart1.Titles.Add("Szálló por");
            chart1.Series.Clear();
            chart1.ChartAreas.Add(new ChartArea("ParticulateMatterChartArea"));
            chart1.ChartAreas[0].AxisX.Interval = 1;
            for (int i = 0; i < 9; i++)
            {
                chart1.Series.Add(new Series("ParticulateMatterSeries" + i));
                chart1.Series["ParticulateMatterSeries" + i].ChartType = SeriesChartType.Line;
                chart1.Series["ParticulateMatterSeries" + i].BorderWidth = 2;
                chart1.Series["ParticulateMatterSeries" + i].LegendText = "Szenzor" + i;
                chart1.Series["ParticulateMatterSeries" + i].Color = SetChartSeriesColorByID(i);

            }

            chart2.Titles.Add("Hőmérséklet (°C)");
            chart2.Series.Clear();
            chart2.ChartAreas.Add(new ChartArea("TemperatureChartArea"));
            chart2.ChartAreas[0].AxisX.Interval = 1;
            for (int i = 0; i < 9; i++)
            {
                chart2.Series.Add(new Series("TemperatureSeries" + i));
                chart2.Series["TemperatureSeries" + i].ChartType = SeriesChartType.Line;
                chart2.Series["TemperatureSeries" + i].BorderWidth = 2;
                chart2.Series["TemperatureSeries" + i].LegendText = "Szenzor" + i;
                chart2.Series["TemperatureSeries" + i].Color = SetChartSeriesColorByID(i);
            }

            chart3.Titles.Add("Páratartalom (%)");
            chart3.Series.Clear();
            chart3.ChartAreas.Add(new ChartArea("HumidityChartArea"));
            chart3.ChartAreas[0].AxisX.Interval = 1;
            for (int i = 0; i < 9; i++)
            {
                chart3.Series.Add(new Series("HumiditySeries" + i));
                chart3.Series["HumiditySeries" + i].ChartType = SeriesChartType.Line;
                chart3.Series["HumiditySeries" + i].BorderWidth = 2;
                chart3.Series["HumiditySeries" + i].LegendText = "Szenzor" + i;
                chart3.Series["HumiditySeries" + i].Color = SetChartSeriesColorByID(i);
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
                chart4.Series["BatteryVoltageSeries" + i].Color = SetChartSeriesColorByID(i);
            }
        }

        private System.Drawing.Color SetChartSeriesColorByID(int i)
        {
            switch (i)
            {
                case 0: return System.Drawing.Color.Blue;
                case 1: return System.Drawing.Color.Yellow;
                case 2: return System.Drawing.Color.Green;
                case 3: return System.Drawing.Color.Purple;
                case 4: return System.Drawing.Color.Orange;
                default: return System.Drawing.Color.Black;
            }

        }

        private void UpdateChartsWithData(List<SensorData> sensorDataList)
        {
            foreach (SensorData sensorData in sensorDataList)
            {
                if (sensorData != null)
                {
                    chart1.Series["ParticulateMatterSeries" + sensorData.SensorID].Points.AddXY(sensorData.Date.ToShortTimeString(), sensorData.ParticulateMatter);
                    if (chart1.Series["ParticulateMatterSeries" + sensorData.SensorID].Points.Count > 110)
                    {
                        chart1.Series["ParticulateMatterSeries" + sensorData.SensorID].Points.RemoveAt(0);
                    }

                    chart2.Series["TemperatureSeries" + sensorData.SensorID].Points.AddXY(sensorData.Date.ToShortTimeString(), sensorData.Temperature);
                    if (chart2.Series["TemperatureSeries" + sensorData.SensorID].Points.Count > 110)
                    {
                        chart2.Series["TemperatureSeries" + sensorData.SensorID].Points.RemoveAt(0);
                    }

                    chart3.Series["HumiditySeries" + sensorData.SensorID].Points.AddXY(sensorData.Date.ToShortTimeString(), sensorData.Humidity);
                    if (chart3.Series["HumiditySeries" + sensorData.SensorID].Points.Count > 110)
                    {
                        chart3.Series["HumiditySeries" + sensorData.SensorID].Points.RemoveAt(0);
                    }

                    chart4.Series["BatteryVoltageSeries" + sensorData.SensorID].Points.AddXY(sensorData.Date.ToShortTimeString(), sensorData.BatteryVoltage);
                    if (chart4.Series["BatteryVoltageSeries" + sensorData.SensorID].Points.Count > 110)
                    {
                        chart4.Series["BatteryVoltageSeries" + sensorData.SensorID].Points.RemoveAt(0);
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sensorDataList = databaseManager.TestQueryByDate(lastQueryTime.ToString("yyyy.MM.dd H:mm"));
            UpdateChartsWithData(sensorDataList);
            UpdateMinMaxValues(sensorDataList);
            UpdateMinMaxLabels();
            UpdateMovingAverages(sensorDataList);
            UpdateMovingAverageLabels();
            foreach (SensorData sensorData in sensorDataList)
            {
                CheckThresholdValues(sensorData);
            }
            lastQueryTime = lastQueryTime.AddMinutes(1);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            humidityThreshold = (int)numericUpDown1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetMinMaxValueByName("ParticulateMatter");
        }


        private void UpdateMinMaxValues(List<SensorData> sensorDataList)
        {
            foreach (SensorData sensorData in sensorDataList)
            {
                if (sensorData != null)
                {
                    var (minParticulateMatter, maxParticulateMatter) = minMaxValues["ParticulateMatter"];
                    if (sensorData.ParticulateMatter < minParticulateMatter)
                    {
                        minMaxValues["ParticulateMatter"] = (sensorData.ParticulateMatter, maxParticulateMatter);
                    }
                    if (sensorData.ParticulateMatter > maxParticulateMatter)
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

        private void ResetMinMaxValueByName(string name)
        {
            minMaxValues[name] = (float.MaxValue, float.MinValue);
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
                    movingAverages["Sensor" + sensorData.SensorID].particle.AddDataPoint(sensorData.ParticulateMatter);
                    movingAverages["Sensor" + sensorData.SensorID].temp.AddDataPoint(sensorData.Temperature);
                    movingAverages["Sensor" + sensorData.SensorID].humidity.AddDataPoint(sensorData.Humidity);
                    movingAverages["Sensor" + sensorData.SensorID].voltage.AddDataPoint(sensorData.BatteryVoltage);
                }
            }
        }

        private void UpdateMovingAverageLabels()
        {
            for (int i = 0; i < 9; i++)
            {
                chart1.Series["ParticulateMatterSeries" + i].LegendText = $"Szenzor{i} ({movingAverages["Sensor" + i].particle})";
                chart2.Series["TemperatureSeries" + i].LegendText = $"Szenzor{i} ({movingAverages["Sensor" + i].temp})";
                chart3.Series["HumiditySeries" + i].LegendText = $"Szenzor{i} ({movingAverages["Sensor" + i].humidity})";
                chart4.Series["BatteryVoltageSeries" + i].LegendText = $"Szenzor{i} ({movingAverages["Sensor" + i].voltage})";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResetMinMaxValueByName("Temperature");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResetMinMaxValueByName("Humidity");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResetMinMaxValueByName("BatteryVoltage");
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            particulateMatterThreshold = (float)numericUpDown2.Value;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            temperatureThreshold = (float)numericUpDown3.Value;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            batteryVoltageThreshold = (float)numericUpDown4.Value;
        }

        private void CheckThresholdValues(SensorData sensorData)
        {
            if (sensorData != null && !alertOnCooldown)
            {
                if (particulateMatterThreshold != 0 && sensorData.ParticulateMatter > particulateMatterThreshold)
                {
                    AlertCooldown();
                    MessageBox.Show($"Szenzor {sensorData.SensorID} szálló por értéke meghaladta a küszöbértéket! Érték: {sensorData.ParticulateMatter}");
                }
                if (temperatureThreshold != 0 && sensorData.Temperature > temperatureThreshold)
                {
                    AlertCooldown();
                    MessageBox.Show($"Szenzor {sensorData.SensorID} hőmérséklet értéke meghaladta a küszöbértéket! Érték: {sensorData.Temperature}");
                }
                if (humidityThreshold != 0 && sensorData.Humidity > humidityThreshold)
                {
                    AlertCooldown();
                    MessageBox.Show($"Szenzor {sensorData.SensorID} páratartalom értéke meghaladta a küszöbértéket! Érték: {sensorData.Humidity}");
                }
                if (batteryVoltageThreshold != 0 && sensorData.BatteryVoltage > batteryVoltageThreshold)
                {
                    AlertCooldown();
                    MessageBox.Show($"Szenzor {sensorData.SensorID} akkumulátor feszültség értéke meghaladta a küszöbértéket! Érték: {sensorData.BatteryVoltage}");
                }
            }
        }

        private void AlertCooldown()
        {
            alertOnCooldown = true;
            Task.Delay(10000).ContinueWith(t => alertOnCooldown = false);
        }

        private void grafikonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = true;
        }

        private void terkepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible=false;
            panel2.Visible = true;
        }
    }
}
