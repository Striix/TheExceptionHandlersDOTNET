using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using HoGent_Stages.Models.Domain;

namespace HoGent_Stages.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Hogeschool Gent - Faculteit Bedrijf en Organisatie - Stages";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Verdere informatie";

            return View();
        }

        public ActionResult Details()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModels c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msg = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    MailAddress from = new MailAddress(c.email.ToString());
                    StringBuilder sb = new StringBuilder();
                    msg.To.Add("astriid_dl@hotmail.com"); // moet naar mvr Van Audenrode & mnr Van Vreckem
                    msg.Subject = "Contact";
                    msg.IsBodyHtml = false;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 25;
                    sb.Append("First name: " + c.naam);
                    sb.Append(Environment.NewLine);
                    sb.Append("Last name: " + c.voornaam);
                    sb.Append(Environment.NewLine);
                    sb.Append("Last name: " + c.email);
                    sb.Append(Environment.NewLine);
                    sb.Append("Email: " + c.bedrijf);
                    sb.Append(Environment.NewLine);
                    sb.Append("Comments: " + c.omschrijvingProject);
                    sb.Append(Environment.NewLine);
                    sb.Append("Comments: " + c.omschrijvingBachelorProef);
                    sb.Append(Environment.NewLine);
                    sb.Append("Comments: " + c.vraagOpmerking);
                    smtp.Send(msg);
                    msg.Dispose();
                    return View("Success");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View();
        }

    }

        
    }

