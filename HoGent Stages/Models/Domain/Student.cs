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
        public String Naam { get; set; }

        [Display(Name = "Naam van het bedrijf")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public String VoorNaam { get; set; }

        [Display(Name = "Straat")]
        public String Straat { get; set; }

        [Display(Name = "Huisnummer")]
        [RegularExpression("\\d+", ErrorMessage = "{0} moet een getal zijn")]
        public int Nummer { get; set; }

        [Display(Name = "Postcode")]
        [RegularExpression("\\d+", ErrorMessage = "{0} moet een getal zijn")]
        public String Postcode { get; set; }

        [Display(Name = "Plaatsnaam")]
        public String Plaats { get; set; }

        [Display(Name = "email")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public String Email { get; set; }

        [Display(Name = "wachtwoord")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public String Wachtwoord { get; set; }

        [Display(Name = "Gsm-nummer")]
        public int Gsm { get; set; }

        public ICollection<Stage> Stageopdrachten { get; set; }
        public byte[] Foto { get; set; }

        public Student()
        {
            Stageopdrachten = new List<Stage>();
        }
        public void WijzigGegevens(Student student)
        {
            Naam = student.Naam;
            VoorNaam = student.VoorNaam;
            Straat = student.Straat;
            Nummer = student.Nummer;
            Postcode = student.Postcode;
            Email = student.Email;
            Wachtwoord = student.Wachtwoord;
            Gsm = student.Gsm;
        }

        //public Stage ZoekStage(Stage stage)
        //{
        //    //     return IStageRepository.
        //}

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
                lijst = Stageopdrachten.OrderBy(b => b.Titel).Where(b => b.Titel.ToUpper().Contains(filter.ToUpper())
                                                                     ||
                                                                     b.Omschrijving.ToUpper().Contains(filter.ToUpper())
                                                                     || b.Semester.ToString() == filter);
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
                    stages = stages.OrderByDescending(s => s.Titel);
                    break;
                case "Date":
                    stages = stages.OrderBy(s => s.ToegevoegDateTime);
                    break;
                case "date_desc":
                    stages = stages.OrderByDescending(s => s.ToegevoegDateTime);
                    break;
                default:  // Name ascending 
                    stages = stages.OrderBy(s => s.Titel);
                    break;
            }
            return stages;
        }
    }

}
