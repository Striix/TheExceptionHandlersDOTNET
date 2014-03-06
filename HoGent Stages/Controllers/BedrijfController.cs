using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HoGent_Stages.Models.DAL;

namespace HoGent_Stages.Controllers
{
    public class BedrijfController : Controller
    {
       private stagesContext db = new stagesContext();

        public ActionResult Create()
        {
            ViewBag.Message = "Bedrijven kunnen zich hier inschrijven";

            return View();
        }

        public ActionResult Details()
        {
            ViewBag.Message = "Your details page.";

            return View();
        }

    }
}
