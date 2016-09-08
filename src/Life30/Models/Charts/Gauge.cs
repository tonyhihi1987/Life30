using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Drawing;
using Life30.ViewModels;
using DotNet.Highcharts.Enums;
using Life30.Helpers;

namespace Life30.Models.Charts
{
    public class Gauge : ACharts
    {
        private string name;
        public Gauge(string name, Dictionary<string, List<Objectif>> objByType) : base(name)
        {
            this.name = name;
            this.objByType = objByType;
        }
        public override void ComputeChart()
        {
            InitChart(new Chart() { PlotBorderWidth = 0, PlotShadow = false,Height=200,Width=320 })                
                .SetTitle(new Title() { Text = string.Empty })
                .SetTooltip(new Tooltip { PointFormat = "{series.name}: <b>{point.percentage:.1f}%</b>" })
                .SetPlotOptions(new PlotOptions
                {
                    Pie = new PlotOptionsPie
                    {
                        StartAngle = -90,
                        EndAngle = 90,
                        Center = new[] { new PercentageOrPixel(50, true), new PercentageOrPixel(75, true) },
                        DataLabels = new PlotOptionsPieDataLabels
                        {
                            Enabled = true,
                            Distance = 0,
                            Style = "color: 'grey'"
                        }
                    }
                })
                .SetCredits(new Credits { Enabled = false })
                .SetOptions(new GlobalOptions() { Colors = colors.ToArray() })
                .SetSeries(new Series
                {
                    Type = ChartTypes.Pie,
                    Name = "Points",
                    PlotOptionsPie = new PlotOptionsPie { InnerSize = new PercentageOrPixel(50, true) },
                    Data = ComputeData()
                });
        }
          

        public override Data ComputeData()
        {
            List<object> datas = new List<object>();

            var objectifs = objByType.First().Value;
            if (objectifs != null)
            {               
                datas.Add(new object[] {"completed",objectifs.Select(a => a.NbPoint).Sum() });
                var toBeCompleted = 30 - objectifs.Select(a => a.NbPoint).Sum();

                if (toBeCompleted < 0) toBeCompleted = 0;

                datas.Add(new object[] { "to complete", toBeCompleted });
            }
            return new Data(datas.ToArray());
        }
    }
}
           
        
    
