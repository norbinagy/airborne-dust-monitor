using AirborneDustMonitor.Core;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirborneDustMonitor.UI.ViewModels.Charts
{
    public class PMChartViewModel
    {
        public PlotModel PlotModel { get; }
        private readonly Dictionary<int, LineSeries> _sensorSeries;

        public PMChartViewModel()
        {
            PlotModel = new PlotModel { Title = "Szálló por ()" };
            _sensorSeries = new Dictionary<int, LineSeries>();

            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                MinimumPadding = 0.5,
                MaximumPadding = 0.5,
                Title = ""
            };

            PlotModel.Axes.Add(yAxis);
            PlotModel.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, StringFormat = "HH:mm" });
        }

        public void UpdateChart(IEnumerable<SensorData> data)
        {
            if (data == null || data.Count() == 0)
            {
                return;
            }

            foreach (var series in _sensorSeries.Values)
            {
                series.Points.Clear();
            }

            foreach (var d in data.OrderBy(d => d.Date))
            {
                if (!_sensorSeries.TryGetValue(d.SensorID, out var series))
                {
                    series = new LineSeries
                    {
                        Title = $"Sensor {d.SensorID}",
                        StrokeThickness = 2,
                        Color = OxyColor.FromHsv((_sensorSeries.Count * 0.15) % 1, 1, 0.8)
                    };
                    _sensorSeries[d.SensorID] = series;
                    PlotModel.Series.Add(series);
                }

                series.Points.Add(new DataPoint(
                    DateTimeAxis.ToDouble(d.Date),
                    (double)d.ParticulateMatter));
            }

            PlotModel.InvalidatePlot(true);
        }
    }
}
