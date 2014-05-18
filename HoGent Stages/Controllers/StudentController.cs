using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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

         [AllowAnonymous]
         public ActionResult Profiel()
         {
             Student student = studentRep.FindBy(User.Identity.Name);
             Student model = new Student();
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

             return View(Student);
         }
         
        [HttpPost]
        [AllowAnonymous]

        public ActionResult Profiel(Student model, HttpPostedFileBase image)
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
            student.setUpdates(student);
            studentRep.SaveChanges();
            target.Close();
            return View(Student);
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Create()
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
            var user = db.Student.FirstOrDefault(u => u.email == ticket.Name);
            return View(user);
        }

        [HttpPost]
        public ActionResult Gegevens(Student student) //Oude stage verwijderen en nieuwe toevoegen --> automatisch bovenaan lijst
        {
            if (ModelState.IsValid)
            {
                StudentRepository studentRepository = new StudentRepository(db);
                var origineel = studentRepository.FindBy(student.Id);
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

    }
}