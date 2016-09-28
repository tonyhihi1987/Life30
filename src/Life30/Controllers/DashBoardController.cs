using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Life30.ViewModels;
using Life30.Models;
using Newtonsoft.Json;
using System.Security.Claims;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using Life30.Models.Charts;
using Life30.Helpers;

namespace Life30.Controllers
{
    public class DashBoardController : ControllerBase
    {
        private IObjectifDataContext objCtx;
        private IUserDataContext userCtx;
        public DashBoardController([FromServices] IUserDataContext userCtx, [FromServices] IObjectifDataContext objCtx)
        {
            this.userCtx = userCtx;
            this.objCtx = objCtx;
        }

        public IActionResult DashBoard()
        {
            if (User.IsSignedIn())
            {

                var startDate = new DateTime();
                var endDate = new DateTime();

                if (MemoryCacher.Contain(CacheKey.START_DATE.GetDescription()))
                {
                    startDate = Convert.ToDateTime(MemoryCacher.GetValue(CacheKey.START_DATE.GetDescription()));
                }
                else
                {
                    startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, DateTime.Now.Day);
                }

                if (MemoryCacher.Contain(CacheKey.END_DATE.GetDescription()))
                {
                    endDate = Convert.ToDateTime(MemoryCacher.GetValue(CacheKey.END_DATE.GetDescription()));
                }
                else
                {
                    endDate = DateTime.Now;
                }

                var highCharts = getCharts(startDate, endDate);
                return View(new ChartsViewModel { Charts = highCharts, StartDate = startDate, EndDate = endDate });
            }
            else
            {
                return View("~/views/account/login");
            }
        }

        [HttpPost]
        public IActionResult RefreshDashBoard(ChartsViewModel chartsVm)
        {            

            var highCharts = getCharts(chartsVm.StartDate, chartsVm.EndDate);
            MemoryCacher.Add(CacheKey.START_DATE.GetDescription(), chartsVm.StartDate,DateTime.Now.AddDays(1));
            MemoryCacher.Add(CacheKey.END_DATE.GetDescription(), chartsVm.EndDate,DateTime.Now.AddDays(1));
            return View("DashBoard",new ChartsViewModel { Charts = highCharts, StartDate = chartsVm.StartDate, EndDate = chartsVm.EndDate });

        }
        private List<ACharts>  getCharts(DateTime startDate,DateTime endDate)
        {
            var dianeId = userCtx.GetUserIdWhithName("Diane");
            var clementId = userCtx.GetUserIdWhithName("Clem");

            var objByTypeDiane = objCtx.GetObjectifs()
                                .GroupBy(a => a.ObjTypeId)
                                .ToDictionary(a => objCtx.GetObjectifTypeById(a.Key).Name, z => z.
                                Where(a => a.UserId.Equals(dianeId) && a.Date >=startDate && a.Date <=endDate).OrderBy(a=>a.Date).ToList());
            var objByTypeClement = objCtx.GetObjectifs()
                                  .GroupBy(a => a.ObjTypeId).ToDictionary(a => objCtx.GetObjectifTypeById(a.Key).Name, z => z.
                                  Where(a => a.UserId.Equals(clementId) && a.Date >= startDate && a.Date <= endDate).OrderBy(a => a.Date).ToList());

            var allDate = objCtx.GetObjectifs().Select(a => a.Date).OrderBy(a=>a.Date)
                                .Where(a => a >= startDate && a <= endDate)
                                .Select(a => a.ToString()).ToList();
            var Area = new Spline("area", objByTypeDiane, allDate);
            var Pie = new Pie("Pie", objByTypeDiane);
            var aa = new Spline("aa", objByTypeClement, allDate);
            var ee = new Pie("ee", objByTypeClement);

            List<ACharts> higCharts = new List<ACharts> { Area, Pie, aa, ee };

            if (User.IsSignedIn())
            {
                foreach (var chart in higCharts)
                {
                    chart.ComputeChart();
                }
            }
            return higCharts;
        }

        [HttpPost]
        public IActionResult DisplayTasks(string task, List<Objectif> objectifs)
        {
            var type = string.Empty;
            if (objectifs.Any())
            {
                type = objCtx.GetObjectifTypeById(objectifs.First().ObjTypeId).Name;
            }
                var column = new Column(type, "Column", new Dictionary<string, List<Objectif>>() { { string.Empty, objectifs } });
                column.ComputeChart();

            return PartialView("ObjectifInfo", new ChartsViewModel { PointCharts = column });
        }
    }
}
