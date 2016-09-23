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
    public class Pie : ACharts
    {

        public Pie(string name, Dictionary<string, List<Objectif>> objByType) : base(name)
        {
            this.objByType = objByType;
        }
        public override void ComputeChart()
        {
            ChartOption option = new ChartOption();

            option.Title = new Title()
            {
                Text = "Points Overview"
            };

            option.Tooltip = new Tooltip()
            {
                PointFormat = "{series.name}: <b>{point.percentage:.1f}%</b>"
            };

            option.PlotOptions = new PlotOptions()
            {
                Pie = new PlotOptionsPie()
                {
                    AllowPointSelect = true,
                    Cursor = DotNet.Highcharts.Enums.Cursors.Pointer,
                    Depth = 35,
                    DataLabels = new PlotOptionsPieDataLabels
                    {
                        Enabled = true,
                        Format = "{point.name}"
                    }
                }
            };


            option.Series.Add(new Series()
            {
                Name = "Clement",
                Data = ComputeData()
               
            });


            InitChart(new Chart()
            {
                Type = DotNet.Highcharts.Enums.ChartTypes.Pie,
                Height = 350,
                BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#F5F5F5")),
                BorderColor = ColorTranslator.FromHtml("#F5F5F5"),
                //Options3d = new ChartOptions3d { Enabled = true, Alpha = 45, Beta = 0 },                
                BorderWidth = 1,                
                //BorderRadius =35

            }
                )
                .SetTitle(option.Title)
                .SetPlotOptions(option.PlotOptions)
                .SetCredits(option.Credit)
                .SetSeries(option.Series.ToArray());

        }

        public override Data ComputeData()
        {
            List<object> datas = new List<object>();

            foreach (var item in objByType.Keys)
            {
                var nbPoint = objByType[item].Select(a => a.NbPoint).Sum();
                var objectifs = Newtonsoft.Json.JsonConvert.SerializeObject(objByType[item]);
                datas.Add(new { name = item,Color=ColorManager.TaskColors[item], Y = nbPoint, events = new SeriesDataEvents() { Click = "function () {"
                      + "$.post('/DashBoard/DisplayTasks',"
                      + $"{{objectifs:{objectifs}}}"
                      + ", function(obj) { $('#myModal').modal();$('#objInfos').html(obj);"
                      + "})"
                      +"}"
                } });
            }
            return new Data(datas.ToArray());
        }
    }
}
           
        
    
