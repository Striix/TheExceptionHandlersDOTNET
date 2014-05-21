using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HoGent_Stages.Models.DAL;
using HoGent_Stages.Models.Domain;

namespace HoGent_Stages.Controllers
{
    public class BegeleiderController : Controller
    {
        private IBegeleiderRepository begeleiderRepository;
        private IUserRepository userRepository;
        private IStageRepository stageRepository;
        private StagesContext db = new StagesContext();

        public BegeleiderController(IBegeleiderRepository begeleiderRepository, IUserRepository userRepository, IStageRepository stageRepository)
        {
            this.begeleiderRepository = begeleiderRepository;
            this.userRepository = userRepository;
            this.stageRepository = stageRepository;
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Overzicht()
        {
            var stageLijst = stageRepository.GetAll();
            return View(stageLijst);
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid) //check for any validation errors
            {
                Begeleider begeleider = new Begeleider();
                String[] namen = user.email.Split('@');
                String[] naam = namen[0].Split('.');
                begeleider.voorNaam = naam[0];
                begeleider.naam = naam[1];
                begeleider.wachtwoord = user.wachtwoord;
                begeleider.email = user.email;
                begeleiderRepository.Add(begeleider);
                begeleiderRepository.SaveChanges();
                user.rol = "Begeleider";
                userRepository.Add(user);
                userRepository.SaveChanges();
                FormsAuthentication.SetAuthCookie(user.email, false);
                return RedirectToAction("Home", "Begeleider");
            }
            else
            {
                ModelState.AddModelError("", "Het formulier is niet correct ingevuld!");
                return View(user);
            }   
        }

        [HttpGet]
        public ActionResult Gegevens()
        {

            var begeleider = db.Begeleider.FirstOrDefault(x => x.email == User.Identity.Name);
            return View(begeleider);
        }
       
        [HttpPost]
        public ActionResult Gegevens(Begeleider begeleider)
        {
            if (ModelState.IsValid)
            {
                var origineel = db.Begeleider.FirstOrDefault(x => x.email == User.Identity.Name);
                origineel.gsm = begeleider.gsm;
                origineel.nummer = begeleider.nummer;
                origineel.plaats = begeleider.plaats;
                origineel.postcode = begeleider.postcode;
                origineel.straat = begeleider.straat;
                origineel.telefoon = begeleider.telefoon;
                db.SaveChanges();
                return RedirectToAction("Home", "begeleider");
            }
            else
            {
                ModelState.AddModelError("", "Gegevens zijn niet aangepast, controleer of alles correct is ingevuld");
                return View(begeleider);
            }
        }
    }
}