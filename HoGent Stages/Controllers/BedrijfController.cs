using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HoGent_Stages.Models.DAL;
using Hogent_Stages.Repository.Stages;
using Hogent_Stages.Repository.Stages.DBContext;

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

        [HttpPost]
        public ActionResult Create(Bedrijf bedrijf) //save entered data
        {
            if (ModelState.IsValid) //check for any validation errors
            {
                BedrijfRepository bedrijfsRepository = new BedrijfRepository(db);
                bedrijfsRepository.Add(bedrijf);
                bedrijfsRepository.SaveChanges();
                return View(bedrijf);
            }
            else
            {
                //when validation failed return viewmodel back to UI (View) 
                return View(bedrijf);
            }
        }

    }
}
