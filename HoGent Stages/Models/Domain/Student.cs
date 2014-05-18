using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Mvc;
using System.Web.Security;
using HoGent_Stages.Controllers;
using HoGent_Stages.Models.DAL;
using Ninject.Activation;

namespace HoGent_Stages.Models.Domain
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Naam van het bedrijf")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public String naam { get; set; }

        [Display(Name = "Naam van het bedrijf")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public String voorNaam { get; set; }

        [Display(Name = "Straat")]
        public String straat { get; set; }

        [Display(Name = "Huisnummer")]
        [RegularExpression("\\d+", ErrorMessage = "{0} moet een getal zijn")]
        public int nummer { get; set; }

        [Display(Name = "Postcode")]
        [RegularExpression("\\d+", ErrorMessage = "{0} moet een getal zijn")]
        public String postcode { get; set; }

        [Display(Name = "Plaatsnaam")]
        public String plaats { get; set; }

        [Display(Name = "email")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public String email { get; set; }

        [Display(Name = "wachtwoord")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public String wachtwoord { get; set; }

        [Display(Name = "Gsm-nummer")]
        public int gsm { get; set; }

        public byte[] Foto { get; set; }

        public void WijzigGegevens(Student student)
        {
            naam = student.naam;
            voorNaam = student.voorNaam;
            straat = student.straat;
            nummer = student.nummer;
            postcode = student.postcode;
            email = student.email;
            wachtwoord = student.wachtwoord;
            gsm = student.gsm;
        }
     
        public Stage ZoekStage(Stage stage)
        {
       //     return IStageRepository.
        }

        public void KiesStage(Stage stage)
        {
            
        }
        public List<Stage> ToonAlleStages()
        {
            List<Stage> lijst = Stageopdrachten.ToList();
            return lijst;
        }
        public void VeranderKeuze(Stage stage)
        {
            
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

    }
   
}
