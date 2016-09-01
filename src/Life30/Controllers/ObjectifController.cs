using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Life30.ViewModels;
using Life30.Models;
using Newtonsoft.Json;
using System.Security.Claims;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Life30.Models.Charts;

namespace Life30.Controllers
{
    public class ObjectifController : ControllerBase
    {
        private IUserDataContext userCtx;
        private IObjectifDataContext objCtx;
        private ObjectifViewModel objVm;
        public ObjectifController([FromServices] IObjectifDataContext objCtx, [FromServices] IUserDataContext userCtx)
        {
            this.objCtx = objCtx;
            this.userCtx = userCtx;
            objVm = new ObjectifViewModel();
            for (int i = 1; i < 31; i++)
            {
                objVm.nbPoints[i - 1] = i;
            }

        }
        private IActionResult UpdateVm(string name)
        {
            @ViewBag.Title = name;
            @ViewBag.Tab = name;
            objVm.Date = DateTime.Now;

            initViewModel(objVm, @ViewBag.Tab);
            return View("Objectif", objVm);
        }

        public IActionResult Alimentation()
        {
            if (User.IsSignedIn())
            {
                return UpdateVm("Alimentation");

            }
            return View("~/views/account/login");
        }

        public IActionResult Sport()
        {
            if (User.IsSignedIn())
            {
                return UpdateVm("Sport");
            }
            return View("~/views/account/login");
        }

        public IActionResult Finances()
        {
            if (User.IsSignedIn())
            {
                return UpdateVm("Finances");
            }
            return View("~/views/account/login");
        }

        public IActionResult Tache()
        {
            if (User.IsSignedIn())
            {
                return UpdateVm("Menage");
            }
            return View("~/views/account/login");
        }

        public IActionResult DashBoard()
        {
            if (User.IsSignedIn())
            {
                @ViewBag.Title = "Tableau de Bord";
                @ViewBag.Tab = "DashBoard";
                return View("DashBoard");
            }
            return View("~/views/account/login");
        }

        [HttpPost]
        public JsonResult AddTask(ObjectifViewModel objVm)
        {
            JsonResult result;


            if (ModelState.IsValid)
            {
                string type = objVm.Type;

                ObjectifType objectifType = objCtx.GetObjectifTypeByName(type);
                objectifType.Items = new List<Item>();


                if (!objVm.Items.Contains(objVm.Tache))
                {
                    objectifType.Items.Add(new Item { Name = objVm.Tache });
                }

                var objectif = new Objectif
                {
                    Date = objVm.Date,
                    NbPoint = objVm.NbPoint,
                    ObjTypeId = objectifType.Id,
                    Commentaire = objVm.Commentaire,
                    UserId = userCtx.GetUserIdWhithName(User.Identity.Name),
                    Tache = objVm.Tache
                };

                objCtx.AddObjectif(objectif);
                objVm = new ObjectifViewModel();
                initViewModel(objVm, type);
                objVm.Objectifs = objCtx.GetObjectifsByType(type);
                result = Json(new { success = true, view = RenderPartialViewToString("Objectif", objVm) });

            }
            else
            {
                initViewModel(objVm, objVm.Type);
                objVm.Objectifs = objCtx.GetObjectifsByType(objVm.Type);
                result = Json(new { success = false, view = RenderPartialViewToString("Objectif", objVm), date = objVm.Date.ToString() });
            }
            return result;

        }

        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request, string task)
        {
            var userId = userCtx.GetUserIdWhithName(User.Identity.Name);
            var result = objCtx.GetObjectifsByType(task)?.Where(a => a.UserId.Equals(userId)).ToList();
            return Json(result?.ToDataSourceResult(request));
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, Objectif obj)
        {
            objCtx.UpdateObjectifs(obj);

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request, Objectif obj)
        {
            if (obj != null)
            {
                objCtx.DeleteObjectif(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        private void SetItems(string task, ObjectifViewModel objVm)
        {
            ObjectifType type = objCtx.GetObjectifTypeByName(task);
            objVm.Items = objCtx.GetItems(type.Id).Select(a => a.Name).Distinct().ToList();
            objVm.Type = task;
        }

        public IActionResult Error()
        {
            return View();
        }

        private void initViewModel(ObjectifViewModel obj, string task)
        {

            InitNbPoints(obj);
            SetItems(task, obj);
            var lastMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, DateTime.Now.Day);
            var lastLastMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 2, DateTime.Now.Day); ;
            SetCharts(task,"ThisMonth", lastMonth,DateTime.Now);
            SetCharts(task,"LastMonth", lastLastMonth,lastMonth);
        }

        private void InitNbPoints(ObjectifViewModel objVm)
        {
            for (int i = 1; i < 31; i++)
            {
                objVm.nbPoints[i - 1] = i;
            }
        }
        private void SetCharts(string task,string name ,DateTime startDate, DateTime endDate)
        {
            var objectifs = objCtx.GetObjectifsByType(task);
            ACharts chart = new Gauge(name, new Dictionary<string, List<Objectif>> { { name, objectifs.Where(a=>a.Date>=startDate && a.Date<=endDate).ToList() } });
            chart.ComputeChart();
            objVm.Charts.Add(chart);
        }
    }
}
