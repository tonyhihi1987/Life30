using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Life30.ViewModels
{
    public class NotificationViewModel
    {
        public List<string> Notifs { get; set; } = new List<string>();
    }
}
