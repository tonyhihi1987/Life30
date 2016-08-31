using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Life30.Models.Charts
{
    public class ChartOption
    {
        public Title Title { get; set; }
        public Credits Credit { get { return new Credits { Enabled = false }; } }
        public Subtitle Subtitle { get; set; }
        public Chart Chart { get; set; }
        public XAxis XAxis { get; set; }
        public YAxis YAxis { get; set; }
        public YAxis[] YAxisArray
        {
            get; set;
        }

        public PlotOptions PlotOptions { get; set; }

        public List<Series> Series { get; set; } = new List<DotNet.Highcharts.Options.Series>();
        public Exporting Exporting { get; set; }
        public Tooltip Tooltip { get; set; }

        public Loading Loading { get; set; }
        public String Size { get; set; }

        public Legend Legend { get; set; }
    }
}
