using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.ComponentModel;

namespace HoGent_Stages.Models.Domain
{
    public class Mentor
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Naam")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public string naam { get; set; }

        [Display(Name = "Voornaam")]
        public string voornaam { get; set; }

        [Display(Name = "email")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public string email { get; set; }

        [Display(Name = "Gsm")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public int gsm { get; set; }

        [Display(Name = "Funtie")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public String functie { get; set; }

        [Display(Name = "Aanspreektitel")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public string aanspreektitel { get; set; }



    }
}
