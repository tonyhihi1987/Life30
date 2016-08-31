using DotNet.Highcharts.Helpers;
using System.Collections.Generic;
using System.Drawing;
namespace Life30.Models.Charts
{
    public abstract class ACharts:DotNet.Highcharts.Highcharts
    {
        public Dictionary<string, List<Objectif>> objByType;
        public ACharts(string name) : base(name) { }

        protected List<System.Drawing.Color> colors = new List<System.Drawing.Color>
        {
            System.Drawing.Color.LightGreen,
            System.Drawing.Color.DarkOrange,
            System.Drawing.Color.DeepSkyBlue,
            System.Drawing.Color.Magenta
        };
        public abstract void ComputeChart();
        public abstract Data ComputeData();
    }
}