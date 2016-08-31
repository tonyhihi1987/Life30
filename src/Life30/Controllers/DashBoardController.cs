﻿using System;
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
            var dianeId = userCtx.GetUserIdWhithName("Diane");
            var clementId = userCtx.GetUserIdWhithName("Clem");

            var objByTypeDiane = objCtx.GetObjectifs().GroupBy(a => a.ObjTypeId).ToDictionary(a => objCtx.GetObjectifTypeById(a.Key).Name, z => z.Where(a => a.UserId.Equals(dianeId)).ToList());
            var objByTypeClement = objCtx.GetObjectifs().GroupBy(a => a.ObjTypeId).ToDictionary(a => objCtx.GetObjectifTypeById(a.Key).Name, z => z.Where(a => a.UserId.Equals(clementId)).ToList());

            var allDate = objCtx.GetObjectifs().Select(a => a.Date.ToString()).ToList();
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
            return View(new ChartsViewModel { Charts = higCharts });
        }

        [HttpPost]
        public IActionResult DisplayTasks(List<Objectif> objectifs)
        {
            var column = new Column("Column", new Dictionary<string, List<Objectif>>() { {string.Empty, objectifs } });
            column.ComputeChart();
            return PartialView("ObjectifInfo", new ChartsViewModel { PointCharts = column });
        }
    }
}
