using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Life30.Models;
using Life30.ViewModels;
using System.Security.Claims;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace Life30.Controllers
{
    public class FinanceController : Controller
    {
        private IUserDataContext userCtx;
        private IDepenseDataContext depenseCtx;
        private IChargeDataContext chargeCtx;
        public FinanceController([FromServices] IUserDataContext userCtx, [FromServices] IDepenseDataContext depenceCtx, [FromServices] IChargeDataContext chargeCtx)
        {
            this.userCtx = userCtx;
            this.depenseCtx = depenceCtx;
            this.chargeCtx = chargeCtx;
        }
        public ActionResult Finance()
        {
            return View(new FinanceViewModel());
        }
        [HttpPost]
        public ActionResult AddDepense(FinanceViewModel fnVm)
        {
            if (ModelState.IsValid)
            {
                depenseCtx.AddDepenses(fnVm.CurrentDepense);

            }            
            return View("Finance", fnVm);
        }

        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            var userId = userCtx.GetUserIdWhithName(User.Identity.Name);
            var result = depenseCtx.GetDepenses(userId);
            return Json(result?.ToDataSourceResult(request));
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, Depense dep)
        {
            depenseCtx.UpdateDepense(dep);

            return Json(new[] { dep }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request, Depense dep)
        {
            if (dep != null)
            {
                depenseCtx.DeleteDepense(dep);
            }

            return Json(new[] { dep }.ToDataSourceResult(request, ModelState));
        }

    }
}
