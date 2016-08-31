using Life30.Models;
using Life30.Models.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Life30.ViewModels
{
    public class ChartsViewModel
    {
        public List<ACharts> Charts {get;set;}
        public ACharts PointCharts { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}
