using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Security;
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

        public virtual ICollection<Student> studenten { get; set; }
        public Student ()
        {
            studenten = new List<Student>();
        }

        public void WijzigGegevens(Student student)
        {
            var wijzig = studenten.FirstOrDefault(s => s.Id == student.Id);
            wijzig.naam = student.naam;
            wijzig.voorNaam = student.voorNaam;
            wijzig.straat = student.straat;
            wijzig.nummer = student.nummer;
            wijzig.postcode = student.postcode;
            wijzig.email = student.email;
            wijzig.wachtwoord = student.wachtwoord;
            wijzig.gsm = student.gsm;
        }
        public void ZoekStage(Stage stage)
        {

        }

        public void ZoekAlleStages(Stage stage)
        {
            
        }

        public void KiesStage(Stage stage)
        {
            
        }

        public void VeranderKeuze(Stage stage)
        {
            
        }
    }
   
}
