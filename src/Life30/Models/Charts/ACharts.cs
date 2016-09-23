using DotNet.Highcharts.Helpers;
using System.Collections.Generic;
using System.Drawing;
namespace Life30.Models.Charts
{
    public abstract class ACharts : DotNet.Highcharts.Highcharts
    {
        public Dictionary<string, List<Objectif>> objByType;
        public ACharts(string name) : base(name) { }
        public string Task { get; set; } = string.Empty;
        public int NbData { get; set; }
        public System.Drawing.Color Color {get;set;}

        public abstract void ComputeChart();
        public abstract Data ComputeData();
    }
}