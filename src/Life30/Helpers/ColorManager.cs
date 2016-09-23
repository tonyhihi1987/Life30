using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Life30.Helpers
{
    public static class ColorManager
    {
        public static Dictionary<string, System.Drawing.Color> TaskColors { get; set; } = new Dictionary<string, System.Drawing.Color>
        {
            { "Alimentation", System.Drawing.Color.LightGreen },
            { "Sport",System.Drawing.Color.DarkOrange },
            { "Menage",System.Drawing.Color.DeepSkyBlue },
            { "Finances",System.Drawing.Color.Magenta }

        };

        public static Dictionary<string, System.Drawing.Color> CompleTedColor { get; set; } = new Dictionary<string, System.Drawing.Color>
        {
            { "Completed", System.Drawing.Color.LightGreen },
            { "NotCompleted",System.Drawing.Color.Red },

        };


    }
}
