using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HoGent_Stages.Models.DAL;
using Hogent_Stages.Repository.Stages;
using Hogent_Stages.Repository.Stages.DBContext;
using Hogent_Stages.Repository.Stages.Model;
using PagedList;

namespace HoGent_Stages.Controllers
{
    public class StudentController : Controller
    {
        private stagesContext db = new stagesContext();

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Gegevens()
        {
            return View();
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

        public ViewResult Overzicht(string sortOrder, string currentFilter, string searchString, int? page)
        {
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

            var stages = (from s in db.Stage
                          join b in db.Bedrijf on s.bedrijfId equals b.Id
                          select new OverzichtViewModel()
                           {
                               BedrijfNaam = b.bedrijfsNaam,
                               Titel = s.titel,
                               Omschrijving = s.omschrijving,
                               AantalStudenten = s.aantalStudenten,
                               Semester = s.semester,
                               Datum = s.ToegevoegDateTime
                           });



            if (!String.IsNullOrEmpty(searchString))
            {
                stages = stages.Where(s => s.Titel.ToUpper().Contains(searchString.ToUpper())
                                       || s.Omschrijving.ToUpper().Contains(searchString.ToUpper())
                                       || s.Semester.ToString() == searchString
                                       || s.BedrijfNaam.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    stages = stages.OrderByDescending(s => s.Titel);
                    break;
                case "Date":
                    stages = stages.OrderBy(s => s.Datum);
                    break;
                case "date_desc":
                    stages = stages.OrderByDescending(s => s.Datum);
                    break;
                default:  // Name ascending 
                    stages = stages.OrderBy(s => s.Titel);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(stages.ToPagedList(pageNumber, pageSize));
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


    }
}