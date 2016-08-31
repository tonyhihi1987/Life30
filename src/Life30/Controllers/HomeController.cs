using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace Life30.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()

        {
            ViewBag.Title = "Accueil";
            return View();                                                
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
