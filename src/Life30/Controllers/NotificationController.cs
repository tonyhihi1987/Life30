using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Life30.Models;
using Life30.ViewModels;

namespace Life30.Controllers
{
    public class NotificationController : Controller
    {
        private IObjectifDataContext objCtx;
        private IUserDataContext userCtx;
        public NotificationController([FromServices] IObjectifDataContext objCtx, [FromServices] IUserDataContext userCtx)
        {
            this.objCtx = objCtx;
            this.userCtx = userCtx;
        }
        [HttpGet]
        public IActionResult GetObjectifs()
        {
            List<Objectif>objs = objCtx.GetObjectifs();
            List<string> notifs = new List<string>();
            foreach (var obj in objs)
            {                
                if (obj.Date >= DateTime.Now.AddDays(-7))
                {
                    var type = objCtx.GetObjectifTypeById(obj.ObjTypeId).Name;
                    var user = userCtx.GetUserNameWhithId(obj.UserId);
                    notifs.Add($"{obj.Date} : {user} a ajouté {obj.NbPoint} point(s) dans la catégorie {type}");
                }
            }
            return PartialView("Notification", new NotificationViewModel { Notifs = notifs });
        }
    }
}
