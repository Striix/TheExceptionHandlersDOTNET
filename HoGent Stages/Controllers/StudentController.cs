using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Hogent_Stages.Models;
using HoGent_Stages.Models.DAL;
using HoGent_Stages.Models.Domain;
using PagedList;


namespace HoGent_Stages.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        static stagesContext db = new stagesContext();
         StudentRepository studentRep = new StudentRepository(db);
        BedrijfRepository bedrijfRep = new BedrijfRepository(db);
        StageRepository stageRep = new StageRepository(db);

         /*[AllowAnonymous]
         public ActionResult Profiel()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
             }
             catch
             {
                 return RedirectToAction("Index", "Begeleider");
             }

        public class OverzichtViewModel
        {
            public string BedrijfNaam { get; set; }
            public string Titel { get; set; }
            public string Omschrijving { get; set; }
            public int AantalStudenten { get; set; }
            public int Semester { get; set; }
            public DateTime Datum { get; set; }
        }
             }
             catch
             {
                 return RedirectToAction("Index", "Home");
             }

        //public ViewResult Overzicht(string sortOrder, string currentFilter, string searchString, int? page)
        //{
        //    ViewBag.CurrentSort = sortOrder;
        //    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

        //    if (searchString != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }

        //    ViewBag.CurrentFilter = searchString;

        //    var stages = (from s in db.Stage
        //                  join b in db.Bedrijf on s.bedrijfId equals b.Id
        //                  select new OverzichtViewModel()
        //                   {
        //                       BedrijfNaam = b.bedrijfsNaam,
        //                       Titel = s.titel,
        //                       Omschrijving = s.omschrijving,
        //                       AantalStudenten = s.aantalStudenten,
        //                       Semester = s.semester,
        //                       Datum = s.ToegevoegDateTime
        //                   });

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Create(User user)
        {
            UserRepository userRepository = new UserRepository(db);
            StudentRepository studentRepository = new StudentRepository(db);

            var existedUsers = (from c in userRepository.GetAll()
                                where c.email == user.email
                                select c.email).ToList();

            if (existedUsers.Count.Equals(0))
            {
                if (user.email.Contains("student.hogent"))
                {
                    Student student = new Student();
                    String[] namen = user.email.Split('@');
                    String[] naam = namen[0].Split('.');
                    student.voorNaam = naam[0];
                    student.naam = naam[1];
                    student.wachtwoord = user.wachtwoord;
                    student.email = user.email;
                    studentRepository.Add(student);
                    studentRepository.SaveChanges();
                    user.rol = "student";
                    userRepository.Add(user);
                    userRepository.SaveChanges();
                    FormsAuthentication.SetAuthCookie(user.email, false);
                    return RedirectToAction("Home", "Student");
                }
                else
                {
                    ModelState.AddModelError("", "Het gebruikte e-mailadres is geen HoGent account");
                    return View(user);
                }
            }
            else
            {
                ModelState.AddModelError("", "Het gebruikte e-mailadres beschikt al over een account");
                return View(user);
            }
        }

        [HttpGet]
        public ActionResult Gegevens()
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            var student = db.Student.FirstOrDefault(u => u.email == ticket.Name);
            return View(student);
        }

        [HttpPost]
        public ActionResult Gegevens(Student student) //Oude stage verwijderen en nieuwe toevoegen --> automatisch bovenaan lijst
        {
            if (ModelState.IsValid)
            {
                StudentRepository studentRepository = new StudentRepository(db);
                var origineel = studentRepository.FindById(student.Id);
                origineel.gsm = student.gsm;
                origineel.nummer = student.nummer;
                origineel.plaats = student.plaats;
                origineel.postcode = student.postcode;
                origineel.straat = student.straat;
                studentRepository.SaveChanges();
                return RedirectToAction("Home", "Student"); 
            }
            else
            {
                return RedirectToAction("about", "home");
            }
            

        }

       
        public ViewResult Overzicht(string sortOrder, string currentFilter, string searchString, int? page)
        {
            Student student = studentRep.FindBy(User.Identity.Name);
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            
            ViewBag.CurrentFilter = searchString;

            var stages = stageRep.GetAll();
            stages = student.Filter(bedrijfRep.FindAll().ToList(), searchString);
            stages = student.Sort(sortOrder);

            int pageSize = 3; 
            int pageNumber = (page ?? 1);
            return View(stages.ToPagedList(pageNumber, pageSize));
        }

    }
}