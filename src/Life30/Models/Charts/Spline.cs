using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Life30.Models.Charts
{
    public class Spline : ACharts
    {
        private List<string> allDate;
        private List<string> allObjectifDates;
        private List<Objectif> currentObj;



        public Spline(string name, Dictionary<string, List<Objectif>> objByType ,List<string> allObjectifDates):base(name)
        {
            this.objByType = objByType;
            this.allObjectifDates = allObjectifDates;
        }
        public override void ComputeChart()
        {
           
            var categories = new List<string>();
            
            ChartOption option = new ChartOption();

            option.Title = new Title()
            {
                Text = "Points Overview"
            };

            option.Tooltip = new Tooltip()
            {
                PointFormat = "{series.name} produced <b>{point.y:,.0f}</b><br/>warheads in {point.x}"
            };

            option.PlotOptions = new PlotOptions()
            {
                Spline = new PlotOptionsSpline()
                {
                    Marker = new PlotOptionsSplineMarker()
                    {
                        Enabled = false,
                        Symbol = "circle",
                        Radius = 2,
                        States = new PlotOptionsSplineMarkerStates()
                        {
                            Hover = new PlotOptionsSplineMarkerStatesHover()
                            {
                                Enabled = true
                            }
                        }
                    },
                    ConnectNulls=true
                   
                }
            };
            allDate = new List<string>() { new DateTime(2016, 06, 15).ToString() };
            allDate.AddRange(allObjectifDates);
            int i = 0;
            foreach (var item in objByType.Keys)
            {
                currentObj = objByType[item];
                option.Series.Add(new Series()
                {
                    Name = item,
                    Data = ComputeData(),
                    Color = colors[i]
                });
                i++;
            }


            option.XAxis = new XAxis()
            {

                Labels = new XAxisLabels()
                {
                    Formatter = "function () {return this.value} "
                },
                Categories = allDate.ToArray()
            };

            option.YAxis = new YAxis()
            {
                Title = new YAxisTitle()
                {
                    Text = "Points"
                },

                Labels = new YAxisLabels()
                {
                    Formatter = "function () {return this.value} "
                }
            };

            InitChart(new Chart()
            {
              
                 
                Type = DotNet.Highcharts.Enums.ChartTypes.Spline,
                BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#F5F5F5")),
                BorderColor = ColorTranslator.FromHtml("#F5F5F5"),                
                BorderWidth = 1,                
                Height = 350                
            }
                )
                .SetTitle(option.Title)
                .SetXAxis(option.XAxis)
                .SetYAxis(option.YAxis)
                .SetCredits(option.Credit)
                .SetPlotOptions(option.PlotOptions)
                .SetSeries(option.Series.ToArray());
        }

        public override Data ComputeData()
        {
            List<object> nbPoints = new List<object> { 0 };

            foreach (var item in allDate)
            {                 
                if (currentObj.Select(a=>a.Date.ToString()).ToList().Contains(item))
                {
                    nbPoints.Add(currentObj.Where(a => a.Date.ToString().Equals(item)).FirstOrDefault()?.NbPoint);
                }
                else
                {
                    nbPoints.Add(null);
                }

            }

            return new Data(nbPoints.ToArray());
        }
    }


    
}
