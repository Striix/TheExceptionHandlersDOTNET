using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using HoGent_Stages.Models.DAL;
using HoGent_Stages.Models.Domain;
using Microsoft.Ajax.Utilities;

namespace HoGent_Stages.Controllers
{

    public class BedrijfController : Controller
    {
        private IBedrijfRepository bedrijfsRepository;
        private IUserRepository userRepository;
        private IStageRepository _stageRepository;
 

        public BedrijfController(IBedrijfRepository bedrijfsRepository, IUserRepository userRepository, IStageRepository stageRepository)
        {
            this.bedrijfsRepository = bedrijfsRepository;
            this.userRepository = userRepository;
            this._stageRepository = stageRepository;
        }

        static StagesContext db = new StagesContext();

        public ActionResult Create()
        {
           ViewBag.Message = "Bedrijven kunnen zich hier inschrijven";

            return View();
        }

        [Authorize]
        public ActionResult Stages()
        {
            ViewBag.Message = "Maak hier een stage aan";

            return View();
        }

        [Authorize]
        public ActionResult Deleted()
        {
            ViewBag.Message = "Verwijder Stage";

            return View();
        }

        [Authorize]
        public ActionResult Overzicht()
        {
            ViewBag.Message = "Overzicht stages";
            var bedrijf = db.Bedrijf.FirstOrDefault(u => u.email == User.Identity.Name);
            var stagelijst = bedrijf.stages;
            return View(stagelijst);
        }

        [Authorize]
        public ActionResult Edit()
        {
            ViewBag.Message = "Stage aanpassen";

            return View();
        }

        [Authorize]
        public ActionResult Home()
        {
            ViewBag.Message = "Home";

            return View();   
        }

        [Authorize]
        public ActionResult CreateMentor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Bedrijf bedrijf) //save entered data
        {
            if (ModelState.IsValid) //check for any validation errors
            {

                if (bedrijfsRepository.ControleBedrijf(bedrijf))
                {
                    var user = new User();
                    user.email = bedrijf.email;
                    user.wachtwoord = bedrijf.wachtwoord;
                    user.rol = "bedrijf";
                    user.bevestigWachtwoord = bedrijf.wachtwoord;
                    userRepository.Add(user);
                    userRepository.SaveChanges();
                    bedrijfsRepository.Add(bedrijf);
                    bedrijfsRepository.SaveChanges();
                    FormsAuthentication.SetAuthCookie(user.email, false);
                    return RedirectToAction("CreateMentor", "Bedrijf");
                 }
                 else
                 {
                     ModelState.AddModelError("", "Er bestaat al een bedrijf met hetzelfde email-adres");
                     return View(bedrijf);
                 }
             }
             else
             {
                 ModelState.AddModelError("", "Het formulier is niet correct ingevuld!");
                 return View(bedrijf);
             }
        }

        [HttpPost]
        public ActionResult CreateMentor(Mentor mentor)
        {
            if (ModelState.IsValid)
            {
                var bedrijf = db.Bedrijf.FirstOrDefault(u => u.email == User.Identity.Name);
                bedrijf.VoegMentorToe(mentor);
                db.SaveChanges();
                return RedirectToAction("CreateMentor", "Bedrijf");
            }
            else
            {
                ModelState.AddModelError("","Er zijn gegeven niet ingevuld of incorrect");
                return View(mentor);
            }


        }

        [HttpPost]
        public ActionResult Stages(Stage stage) //save entered data
        {
            if (ModelState.IsValid) //check for any validation errors
            {
                var bedrijf = db.Bedrijf.FirstOrDefault(u => u.email == User.Identity.Name);
                bedrijf.VoegStageToe(stage);
                db.SaveChanges();
                //Mail();
                return RedirectToAction("Overzicht", "Bedrijf");
            }
            else
            {
                ModelState.AddModelError("", "Er zijn gegevens niet ingevuld of incorrect");
                return View(stage);
            }
        }



        [HttpGet]
        public ActionResult Deleted(int id)
        {
            Stage stage = db.Stage.Find(id);
            if (stage == null)
            {
                return HttpNotFound();
            }
            return View(stage);
        }

        [HttpPost]
        public ActionResult Deleted(Stage stage)
        {
        //    //try
        //    //{
            var bedrijf = db.Bedrijf.FirstOrDefault(u => u.email == User.Identity.Name);
            bedrijf.VerwijderStage(stage);
            db.SaveChanges();
            return RedirectToAction("Overzicht", "Bedrijf");
        //    //}
        ////    catch (DataException /* dex */)
        ////    {
        ////        ModelState.AddModelError("", "De stage is niet verwijdert. Probeer opnieuw.");
        ////        return View(stage);C:\Users\Gauthier\Documents\School\Project\HoGent Stages\HoGent Stages\Models\Domain\domain.cd
        ////    }
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            Stage stage = db.Stage.Find(id);
            if (stage == null)
            {
                return HttpNotFound();
            }
            return View(stage);
        }

        [HttpPost]
        public ActionResult Edit(Stage stage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var bedrijf = db.Bedrijf.FirstOrDefault(u => u.email == User.Identity.Name);
                    bedrijf.WijzigStage(stage);
                    db.SaveChanges();
                    return RedirectToAction("Overzicht", "Bedrijf");
                }
            }
            catch (DataException /* dex */)
            {
                ModelState.AddModelError("", "Wijzigingen kunnen niet worden opgeslagen. Probeer opnieuw.");
            }
            return View(stage);
        }

        [HttpGet]
        public ActionResult OverzichtMentor()
        {
            var bedrijf = db.Bedrijf.FirstOrDefault(u => u.email == User.Identity.Name);
            Mentor mentor = bedrijf.MentorBedrijf;
            if (mentor == null)
            {
                ModelState.AddModelError("", "U heeft nog geen mentor toegevoegd");
                return View();
            }
            return View(mentor);
        }




    }
}
