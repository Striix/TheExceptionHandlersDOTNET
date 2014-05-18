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
        private IStageRepository stageRepository;

        public BedrijfController(IBedrijfRepository bedrijfsRepository, IUserRepository userRepository, IStageRepository stageRepository)
        {
            this.bedrijfsRepository = bedrijfsRepository;
            this.userRepository = userRepository;
            this.stageRepository = stageRepository;
        }

        static stagesContext db = new stagesContext();

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
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            var bedrijf = db.Bedrijf.FirstOrDefault(u => u.email == ticket.Name);
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

        public void Mail()
        {
            var myMailMessage = new System.Net.Mail.MailMessage();
            myMailMessage.From = new System.Net.Mail.MailAddress("gauthier.meert.gm@gmail.com");
            myMailMessage.To.Add("gauthier_meert@hotmail.com");// Mail would be sent to this address
            myMailMessage.Subject = "Feedback registratie";
            myMailMessage.Body = "Uw registratie is geslaagd!";

            var smtpServer = new System.Net.Mail.SmtpClient("smtp.gmail.com");
            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential("gauthier.meert.gm@gmail.com", "philips69");
            smtpServer.EnableSsl = true;
            smtpServer.Send(myMailMessage);
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
                    return RedirectToAction("Home", "Bedrijf");
                 }
                 else
                 {
                    return RedirectToAction("Index", "Home"); //Hier nog wijzigen
                 }
             }
             else
             {
                 return RedirectToAction("Index", "Home");       //Hier nog wijzigen
             }
        }

        [HttpPost]
        public ActionResult Stages(Stage stage) //save entered data
        {
            if (ModelState.IsValid) //check for any validation errors
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                var bedrijf = db.Bedrijf.FirstOrDefault(u => u.email == ticket.Name);
                bedrijf.VoegStageToe(stage);
                db.SaveChanges();
                Mail();
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
            //try
            //{
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                var bedrijf = db.Bedrijf.FirstOrDefault(u => u.email == ticket.Name);
                bedrijf.VerwijderStage(stage);
                db.SaveChanges();
                return RedirectToAction("Overzicht", "Bedrijf");
        //    }
        //    catch (DataException /* dex */)
        //    {
        //        ModelState.AddModelError("", "De stage is niet verwijdert. Probeer opnieuw.");
        //        return View(stage);
        //    }
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
                    HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    var bedrijf = db.Bedrijf.FirstOrDefault(u => u.email == ticket.Name);
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




    }
}
