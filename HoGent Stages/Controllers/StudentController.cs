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

        //[AllowAnonymous]
        //public ActionResult Profiel()
        //{
        //    StageBegeleider begeleider = rep.FindBy(User.Identity.Name);
        //    try
        //    {

        //        var foto = (Byte[])begeleider.Foto;
        //        if (foto != null)
        //        {
        //            begeleider.FotoString = Convert.ToBase64String(begeleider.Foto);
        //        }
        //    }
        //    catch
        //    {
        //        return RedirectToAction("Index", "Begeleider");
        //    }

        //    return View(begeleider);
        //}
        //<<<<<<<<<<<Dit is nodig vo als ge geen model gebruiken. string fotostring toevoegen aan student.>>>>>>>>>>>
        [AllowAnonymous]
        public ActionResult Profiel()
        {
            Student student = studentRep.FindBy(User.Identity.Name);
            StudentModel model = new StudentModel();
            try
            {
                var foto = (Byte[])student.Foto;
                if (foto != null)
                    model.Foto = Convert.ToBase64String(foto);
                else
                {
                    foto = new byte[20];
                    model.Foto = Convert.ToBase64String(foto);
                }
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]

        public ActionResult Profiel(StudentModel model, HttpPostedFileBase image)
        {
            Student student = studentRep.FindBy(User.Identity.Name);
            MemoryStream target = new MemoryStream();
            byte[] arr = target.ToArray();
            if (image != null)
            {

                image.InputStream.CopyTo(target);

                arr = target.ToArray();
                student.Foto = arr;
                model.Foto = Convert.ToBase64String(arr);
            }

            var data = (Byte[])student.Foto;
            student.setUpdates(model);//moet student.wijzigGegevens worde denkek?
            studentRep.SaveChanges();
            target.Close();
            return View(model);
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
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
        //                  select new StudentController.OverzichtViewModel()
        //                   {
        //                       BedrijfNaam = b.bedrijfsNaam,
        //                       Titel = s.titel,
        //                       Omschrijving = s.omschrijving,
        //                       AantalStudenten = s.aantalStudenten,
        //                       Semester = s.semester,
        //                       Datum = s.ToegevoegDateTime
        //                   });



        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        stages = stages.Where(s => s.Titel.ToUpper().Contains(searchString.ToUpper())
        //                               || s.Omschrijving.ToUpper().Contains(searchString.ToUpper())
        //                               || s.Semester.ToString() == searchString
        //                               || s.BedrijfNaam.ToUpper().Contains(searchString.ToUpper()));
        //    }
        //    switch (sortOrder)
        //    {
        //        case "name_desc":
        //            stages = stages.OrderByDescending(s => s.Titel);
        //            break;
        //        case "Date":
        //            stages = stages.OrderBy(s => s.Datum);
        //            break;
        //        case "date_desc":
        //            stages = stages.OrderByDescending(s => s.Datum);
        //            break;
        //        default:  // Name ascending 
        //            stages = stages.OrderBy(s => s.Titel);
        //            break;
        //    }

        //    int pageSize = 3;
        //    int pageNumber = (page ?? 1);
        //    return View(stages.ToPagedList(pageNumber, pageSize));
        //}


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
                    student.VoorNaam = naam[0];
                    student.Naam = naam[1];
                    student.Wachtwoord = user.wachtwoord;
                    student.Email = user.email;
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
            var student = db.Student.FirstOrDefault(u => u.Email == ticket.Name);
            return View(student);
        }

        [HttpPost]
        public ActionResult Gegevens(Student student) //Oude stage verwijderen en nieuwe toevoegen --> automatisch bovenaan lijst
        {
            if (ModelState.IsValid)
            {
                StudentRepository studentRepository = new StudentRepository(db);
                var origineel = studentRepository.FindById(student.Id);
                origineel.Gsm = student.Gsm;
                origineel.Nummer = student.Nummer;
                origineel.Plaats = student.Plaats;
                origineel.Postcode = student.Postcode;
                origineel.Straat = student.Straat;
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