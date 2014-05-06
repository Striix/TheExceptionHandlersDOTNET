using System;
using System.Collections.Generic;
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
using Hogent_Stages.Repository.Stages;
using Hogent_Stages.Repository.Stages.DBContext;
using Hogent_Stages.Repository.Stages.Model;

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


        public ActionResult Stages()
        {
            ViewBag.Message = "Maak hier een stage aan";

            return View();
        }

        public ActionResult Deleted()
        {
            ViewBag.Message = "Verwijder Stage";

            return View();
        }


        public ActionResult Overzicht()
        {
            ViewBag.Message = "Overzicht stages";

            return View();
        }

        public ActionResult Edit()
        {
            ViewBag.Message = "Stage aanpassen";

            return View();
        }

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
                BedrijfRepository bedrijfsRepository = new BedrijfRepository(db);   //Controleren of persoon zich al heeft ingeschreven
                UserRepository userRepository = new UserRepository(db);

                var existedUsers = (from c in bedrijfsRepository.GetAll()
                                    where c.email == bedrijf.email
                                    select c.bedrijfsNaam).ToList();

                    if (existedUsers.Count.Equals(0))
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
                        return RedirectToAction("Index", "Home");       //Hier nog wijzigen
                    }
                
            }
            else
            {
                //when validation failed return viewmodel back to UI (View) 
                return View(bedrijf);
            }
        }

        [HttpPost]
        public ActionResult Stages(Stage stage) //save entered data
        {
            if (ModelState.IsValid) //check for any validation errors
            {
                StageRepository stageRepository = new StageRepository(db);
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                var user = db.Bedrijf.FirstOrDefault(u => u.email == ticket.Name);
                stage.bedrijfId = user.Id;                          // Hier zit nog een fout, neemt id niet over, id wordt wel ingeladen
                stage.ToegevoegDateTime = DateTime.Now;
                stageRepository.Add(stage);
                stageRepository.SaveChanges();
                Mail();
                return RedirectToAction("Overzicht", "Bedrijf");
            }
            else
            {
                //when validation failed return viewmodel back to UI (View) 
                return View(stage);
            }
        }

        [HttpGet]
        public ActionResult Deleted(int id)
        {
            StageRepository stageRepository = new StageRepository(db);
            var stage = stageRepository.FindBy(id);
            ViewData.Add("1", id);
            return View(stage);
        }

        public ActionResult Verwijder(int id)
        {
            StageRepository stageRepository = new StageRepository(db);
            var stage2 = stageRepository.FindBy(id);
            stageRepository.Delete(stage2);
            stageRepository.SaveChanges();
            return RedirectToAction("Overzicht", "Bedrijf");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            StageRepository stageRepository = new StageRepository(db);
            var stage = stageRepository.FindBy(id);
            ViewData.Add("1", id);
            return View(stage);
        }

        [HttpPost]
        public ActionResult Edit(Stage stage) //Oude stage verwijderen en nieuwe toevoegen --> automatisch bovenaan lijst
        {
            StageRepository stageRepository = new StageRepository(db);
            var origineel = stageRepository.FindBy(stage.Id);
            stageRepository.Delete(origineel);
            stageRepository.Add(stage);
            stageRepository.SaveChanges();
            return RedirectToAction("Overzicht", "Bedrijf");
            
        }




    }
}
