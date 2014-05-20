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
using Hogent_Stages.Models;
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

        public ICollection<Stage> Stageopdrachten { get; set; }
        public byte[] Foto { get; set; }

        public Student()
        {
            Stageopdrachten = new List<Stage>();
        }
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
        public IEnumerable<Stage> Filter(ICollection<Bedrijf> bedrijven, string filter)
        {
            IEnumerable<Stage> lijst = new List<Stage>();
            if (!String.IsNullOrEmpty(filter))
            {
                lijst = Stageopdrachten.OrderBy(b => b.titel).Where(b => b.titel.ToUpper().Contains(filter.ToUpper())
                                                                     ||
                                                                     b.omschrijving.ToUpper().Contains(filter.ToUpper())
                                                                     || b.semester.ToString() == filter);
                for (int i = 0; i < bedrijven.Count; i++)
                {
                    if (bedrijven.ElementAt(i).stages.Any())
                        if (bedrijven.ElementAt(i).bedrijfsNaam.Contains(filter))
                            for (int j = 0; j < bedrijven.ElementAt(i).stages.Count; j++)
                                lijst.ToList().Add(bedrijven.ElementAt(i).stages.ElementAt(j));
                }
            }
            return lijst;
        }

        public IEnumerable<Stage> Sort(string sortOrder)
        {
            IEnumerable<Stage> stages = new List<Stage>();
            switch (sortOrder)
            {
                case "name_desc":
                    stages = stages.OrderByDescending(s => s.titel);
                    break;
                case "Date":
                    stages = stages.OrderBy(s => s.ToegevoegDateTime);
                    break;
                case "date_desc":
                    stages = stages.OrderByDescending(s => s.ToegevoegDateTime);
                    break;
                default:  // Name ascending 
                    stages = stages.OrderBy(s => s.titel);
                    break;
            }
            return stages;
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
