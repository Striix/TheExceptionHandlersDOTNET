using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoGent_Stages.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Hogeschool Gent - Faculteit Bedrijf en Organisatie - Stages";

            return View();
        }

        public ActionResult Over()
        {
            ViewBag.Message = "Alle informatie over deze applicatie";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contactpagina";

            return View();
        }

        

        
    }
}
