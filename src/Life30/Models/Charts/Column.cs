using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Drawing;
using Life30.ViewModels;
using Life30.Helpers;

namespace Life30.Models.Charts
{
    public class Column : ACharts
    {

        public Column(string task,string name, Dictionary<string, List<Objectif>> objByType) : base(name)
        {
            this.Task = task;
            this.objByType = objByType;
        }
        public override void ComputeChart()
        {
            ChartOption option = new ChartOption();

            option.Title = new Title()
            {
                Text = string.Empty
            };

            option.Tooltip = new Tooltip()
            {
                PointFormat = "{series.name}: <b>{point}%</b>"
            };

            option.PlotOptions = new PlotOptions()
            {                
                Series = new PlotOptionsSeries()
                {                    
                    DataLabels = new PlotOptionsSeriesDataLabels()
                    {
                        Enabled = true,
                        Format = "{point.y}"            
                    }
                }
            };
            option.Tooltip = new Tooltip()
            {
                HeaderFormat = "<span style='font - size:11px'>{series.name}</span><br>",
                PointFormat = "<span style='color:{point.color}'>{point.name}</span>: <b>{point.y</b><br/>"
            };

            option.XAxis = new XAxis()
            {
                Type = DotNet.Highcharts.Enums.AxisTypes.Category
            };
            option.YAxis = new YAxis
            {
                Title = new YAxisTitle()
                {
                    Text = "points by categories"
                }
            };
            option.Legend = new Legend()
            {
                Enabled = false
            };


            option.Series.Add(new Series()
            {               
                Name = "points :",
                Data = ComputeData(),
            });


            InitChart(new Chart()
            {
                Type = DotNet.Highcharts.Enums.ChartTypes.Column,
                BorderWidth = 0,                
            }
                )                
                .SetLegend(option.Legend)
                .SetTitle(option.Title)
                .SetPlotOptions(option.PlotOptions)
                .SetXAxis(option.XAxis)
                .SetYAxis(option.YAxis)
                .SetCredits(option.Credit)
                .SetOptions(new GlobalOptions() { Colors = new Color[] { ColorManager.TaskColors[Task] } })
                .SetSeries(option.Series.ToArray());

        }

        public override Data ComputeData()
        {
            List<object> datas = new List<object>();

            var objectifs = objByType.First().Value;
            if (objectifs != null)
            {
                foreach (var obj in objectifs)
                {
                    var nbPoint = obj.NbPoint;
                    datas.Add(new { name = obj.Tache, Y = nbPoint, drilldown = obj.Tache });
                }            
                                
            }
            this.NbData = datas.ToArray().Count();
            return new Data(datas.ToArray());
        }
    }
}
           
        
    
