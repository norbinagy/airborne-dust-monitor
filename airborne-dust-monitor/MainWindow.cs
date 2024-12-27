using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace airborne_dust_monitor
{
    public partial class MainWindow : Form
    {
        DatabaseManager databaseManager;
        DateTime lastQueryTime;
        int movingAverageWindow = 6;
        Dictionary<string, (float min, float max)> minMaxValues;
        List<SensorData> sensorDataList;
        Dictionary<string, (MovingAverageCalculator particle, MovingAverageCalculator temp, MovingAverageCalculator humidity, MovingAverageCalculator voltage)> movingAverages;
        Dictionary<string, (float sum, int count)> recentSensorParticulateMatterSum;
        bool emailSent;

        public MainWindow()
        {
            InitializeComponent();
            InitializeCharts();
            emailSent = false;
            databaseManager = new DatabaseManager();
            lastQueryTime = DateTime.Parse("2024.03.28 12:39");
            sensorDataList = new List<SensorData>();
            recentSensorParticulateMatterSum = new Dictionary<string, (float sum, int count)>()
            {
                { "Sensor0", (sum: 0, count: 0) },
                { "Sensor1", (sum: 0, count: 0) },
                { "Sensor2", (sum: 0, count: 0) },
                { "Sensor3", (sum: 0, count: 0) },
                { "Sensor4", (sum: 0, count: 0) },
                { "Sensor5", (sum: 0, count: 0) },
                { "Sensor6", (sum: 0, count: 0) },
                { "Sensor7", (sum: 0, count: 0) },
                { "Sensor8", (sum: 0, count: 0) }
            };
            minMaxValues = new Dictionary<string, (float min, float max)>()
            {
                { "ParticulateMatter", (min: float.MaxValue, max: float.MinValue) },
                { "Temperature", (min: float.MaxValue, max: float.MinValue) },
                { "Humidity", (min : float.MaxValue , max : float.MinValue) },
                { "BatteryVoltage", (min: float.MaxValue, max: float.MinValue) }
            };
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
            particulateMatterChart.Titles.Add("Szálló por");
            particulateMatterChart.Series.Clear();
            particulateMatterChart.ChartAreas.Add(new ChartArea("ParticulateMatterChartArea"));
            particulateMatterChart.ChartAreas[0].AxisX.Interval = 1;
            for (int i = 0; i < 9; i++)
            {
                particulateMatterChart.Series.Add(new Series("ParticulateMatterSeries" + i));
                particulateMatterChart.Series["ParticulateMatterSeries" + i].ChartType = SeriesChartType.Line;
                particulateMatterChart.Series["ParticulateMatterSeries" + i].BorderWidth = 2;
                particulateMatterChart.Series["ParticulateMatterSeries" + i].LegendText = "Szenzor" + i;
                particulateMatterChart.Series["ParticulateMatterSeries" + i].Color = SetChartSeriesColorByID(i);

            }

            temperatureChart.Titles.Add("Hőmérséklet (°C)");
            temperatureChart.Series.Clear();
            temperatureChart.ChartAreas.Add(new ChartArea("TemperatureChartArea"));
            temperatureChart.ChartAreas[0].AxisX.Interval = 1;
            for (int i = 0; i < 9; i++)
            {
                temperatureChart.Series.Add(new Series("TemperatureSeries" + i));
                temperatureChart.Series["TemperatureSeries" + i].ChartType = SeriesChartType.Line;
                temperatureChart.Series["TemperatureSeries" + i].BorderWidth = 2;
                temperatureChart.Series["TemperatureSeries" + i].LegendText = "Szenzor" + i;
                temperatureChart.Series["TemperatureSeries" + i].Color = SetChartSeriesColorByID(i);
            }

            humidityChart.Titles.Add("Páratartalom (%)");
            humidityChart.Series.Clear();
            humidityChart.ChartAreas.Add(new ChartArea("HumidityChartArea"));
            humidityChart.ChartAreas[0].AxisX.Interval = 1;
            for (int i = 0; i < 9; i++)
            {
                humidityChart.Series.Add(new Series("HumiditySeries" + i));
                humidityChart.Series["HumiditySeries" + i].ChartType = SeriesChartType.Line;
                humidityChart.Series["HumiditySeries" + i].BorderWidth = 2;
                humidityChart.Series["HumiditySeries" + i].LegendText = "Szenzor" + i;
                humidityChart.Series["HumiditySeries" + i].Color = SetChartSeriesColorByID(i);
            }

            batteryVoltageChart.Titles.Add("Akkumulátor feszültség (V)");
            batteryVoltageChart.Series.Clear();
            batteryVoltageChart.ChartAreas.Add(new ChartArea("BatteryVoltageChartArea"));
            batteryVoltageChart.ChartAreas[0].AxisX.Interval = 1;
            batteryVoltageChart.ChartAreas[0].AxisY.Minimum = 3.5;
            batteryVoltageChart.ChartAreas[0].AxisY.Maximum = 4.5;
            for (int i = 0; i < 9; i++)
            {
                batteryVoltageChart.Series.Add(new Series("BatteryVoltageSeries" + i));
                batteryVoltageChart.Series["BatteryVoltageSeries" + i].ChartType = SeriesChartType.Line;
                batteryVoltageChart.Series["BatteryVoltageSeries" + i].BorderWidth = 2;
                batteryVoltageChart.Series["BatteryVoltageSeries" + i].LegendText = "Szenzor" + i;
                batteryVoltageChart.Series["BatteryVoltageSeries" + i].Color = SetChartSeriesColorByID(i);
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
                    recentSensorParticulateMatterSum["Sensor" + sensorData.SensorID] = (sum: recentSensorParticulateMatterSum["Sensor" + sensorData.SensorID].sum+sensorData.ParticulateMatter, count: recentSensorParticulateMatterSum["Sensor" + sensorData.SensorID].count+1);

                    if (recentSensorParticulateMatterSum["Sensor" + sensorData.SensorID].count==5)
                    {
                        if (recentSensorParticulateMatterSum["Sensor" + sensorData.SensorID].sum == 0) Console.WriteLine("Sensor" + sensorData.SensorID + " error!");
                        recentSensorParticulateMatterSum["Sensor" + sensorData.SensorID] = (sum: 0, count: 0);
                    }

                    particulateMatterChart.Series["ParticulateMatterSeries" + sensorData.SensorID].Points.AddXY(sensorData.Date.ToShortTimeString(), sensorData.ParticulateMatter);
                    if (particulateMatterChart.Series["ParticulateMatterSeries" + sensorData.SensorID].Points.Count > 110)
                    {
                        particulateMatterChart.Series["ParticulateMatterSeries" + sensorData.SensorID].Points.RemoveAt(0);
                    }

                    temperatureChart.Series["TemperatureSeries" + sensorData.SensorID].Points.AddXY(sensorData.Date.ToShortTimeString(), sensorData.Temperature);
                    if (temperatureChart.Series["TemperatureSeries" + sensorData.SensorID].Points.Count > 110)
                    {
                        temperatureChart.Series["TemperatureSeries" + sensorData.SensorID].Points.RemoveAt(0);
                    }

                    humidityChart.Series["HumiditySeries" + sensorData.SensorID].Points.AddXY(sensorData.Date.ToShortTimeString(), sensorData.Humidity);
                    if (humidityChart.Series["HumiditySeries" + sensorData.SensorID].Points.Count > 110)
                    {
                        humidityChart.Series["HumiditySeries" + sensorData.SensorID].Points.RemoveAt(0);
                    }

                    batteryVoltageChart.Series["BatteryVoltageSeries" + sensorData.SensorID].Points.AddXY(sensorData.Date.ToShortTimeString(), sensorData.BatteryVoltage);
                    if (batteryVoltageChart.Series["BatteryVoltageSeries" + sensorData.SensorID].Points.Count > 110)
                    {
                        batteryVoltageChart.Series["BatteryVoltageSeries" + sensorData.SensorID].Points.RemoveAt(0);
                    }
                }
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            sensorDataList = databaseManager.TestQueryByDate(lastQueryTime.ToString("yyyy.MM.dd H:mm"));
            UpdateChartsWithData(sensorDataList);
            UpdateMinMaxValues(sensorDataList);
            UpdateMinMaxLabels();
            UpdateMovingAverages(sensorDataList);
            UpdateMovingAverageLabels();
            foreach (SensorData sensorData in sensorDataList)
            {
                ThresholdManager.CheckThresholdValues(sensorData, particulateMatterChart, temperatureChart, humidityChart, batteryVoltageChart);
            }
            lastQueryTime = lastQueryTime.AddMinutes(1);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void HumThresholdNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            ThresholdManager.HumidityThreshold = (int)HumThresholdNumericUpDown.Value;
        }

        private void PMMinMaxResetButton_Click(object sender, EventArgs e)
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
                particulateMatterChart.Series["ParticulateMatterSeries" + i].LegendText = $"Szenzor{i} ({movingAverages["Sensor" + i].particle})";
                temperatureChart.Series["TemperatureSeries" + i].LegendText = $"Szenzor{i} ({movingAverages["Sensor" + i].temp})";
                humidityChart.Series["HumiditySeries" + i].LegendText = $"Szenzor{i} ({movingAverages["Sensor" + i].humidity})";
                batteryVoltageChart.Series["BatteryVoltageSeries" + i].LegendText = $"Szenzor{i} ({movingAverages["Sensor" + i].voltage})";
            }
        }

        private void TempMinMaxResetButton_Click(object sender, EventArgs e)
        {
            ResetMinMaxValueByName("Temperature");
        }

        private void HumMinMaxResetButton_Click(object sender, EventArgs e)
        {
            ResetMinMaxValueByName("Humidity");
        }

        private void BVMinMaxResetButton_Click(object sender, EventArgs e)
        {
            ResetMinMaxValueByName("BatteryVoltage");
        }

        private void PMThresholdNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            ThresholdManager.ParticulateMatterThreshold = (float)PMThresholdNumericUpDown.Value;

        }

        private void TempThresholdNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            ThresholdManager.TemperatureThreshold = (float)TempThresholdNumericUpDown.Value;
        }

        private void BVThresholdNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            ThresholdManager.BatteryVoltageThreshold = (float)BVThresholdNumericUpDown.Value;
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

        private void ResetParticulateMatterSeriesColor()
        {
            for (int i = 0; i < 9; i++)
            {
                particulateMatterChart.Series["ParticulateMatterSeries" + i].Color = SetChartSeriesColorByID(i);
            }
        }

        private void ResetTemperatureSeriesColor()
        {
            for (int i = 0; i < 9; i++)
            {
                temperatureChart.Series["TemperatureSeries" + i].Color = SetChartSeriesColorByID(i);
            }
        }

        private void ResetHumiditySeriesColor()
        {
            for (int i = 0; i < 9; i++)
            {
                humidityChart.Series["HumiditySeries" + i].Color = SetChartSeriesColorByID(i);
            }
        }

        private void ResetBatteryVoltageSeriesColor()
        {
            for (int i = 0; i < 9; i++)
            {
                batteryVoltageChart.Series["BatteryVoltageSeries" + i].Color = SetChartSeriesColorByID(i);
            }
        }

        private void PMThresholdResetButton_Click(object sender, EventArgs e)
        {
            ResetParticulateMatterSeriesColor();
            PMThresholdNumericUpDown.Value = 0;
            emailSent = false;
        }

        private void TempThresholdResetButton_Click(object sender, EventArgs e)
        {
            ResetTemperatureSeriesColor();
            TempThresholdNumericUpDown.Value = 0;
            emailSent = false;
        }

        private void HumThresholdResetButton_Click(object sender, EventArgs e)
        {
            ResetHumiditySeriesColor();
            HumThresholdNumericUpDown.Value = 0;
            emailSent = false;
        }

        private void BVThresholdResetButton_Click(object sender, EventArgs e)
        {
            ResetBatteryVoltageSeriesColor();
            BVThresholdNumericUpDown.Value = 0;
            emailSent = false;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsWindow().Show();
        }
    }
}
