using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
namespace PlcPortal.Client.Models.ChartsOptions
{
    public  class ChartOptions
    {
        private Title title;
        public  Title Title { get; set; }
        public Credits Credit { get { return new Credits { Enabled = false }; } }
        public  Subtitle Subtitle { get; set; }
        public  Chart Chart { get; set; }
        public  XAxis XAxis { get; set; }
        public  YAxis YAxis { get; set; }
        public  YAxis[] YAxisArray
        {
            get; set;
        }

        public  PlotOptions PlotOptions { get; }

        public  Exporting Exporting { get; }
        public  Tooltip Tooltip { get; }

        public  Loading Loading { get; }
        public  String Size { get; }

        public  Legend Legend { get; }


    }

}
