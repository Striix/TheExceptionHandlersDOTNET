using System;
using System.ComponentModel.DataAnnotations;

namespace Hogent_Stages.Repository.Stages.Model
{
    public class Bedrijf
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Naam van het bedrijf")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public String bedrijfsNaam { get; set; }

        [Display(Name = "Straat")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public String straat { get; set; }

        [Display(Name = "Huisnummer")]
        [Required(ErrorMessage = "{0} is verplicht")]
        [RegularExpression("\\d+", ErrorMessage = "{0} moet een getal zijn")]
        public int nummer { get; set; }

        [Display(Name = "Postcode")]
        [Required(ErrorMessage = "{0} is verplicht")]
        [RegularExpression("\\d+", ErrorMessage = "{0} moet een getal zijn")]
        public String postcode { get; set; }

        [Display(Name = "Plaatsnaam")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public String plaats { get; set; }

        [Display(Name = "URL")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public string url { get; set; }

        [Display(Name = "E-mailadress")]
        [Required(ErrorMessage = "{0} is verplicht")]
        [EmailAddress(ErrorMessage = "{0} moet een geldig zijn.")]
        public string email { get; set; }

        [Display(Name = "Telefoonnummer")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public string telefoon { get; set; }

        public string bereikbaarheid { get; set; }

        [Display(Name = "Wachtwoord")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public string wachtwoord { get; set; }

        [Display(Name = "Bevestig wachtwoord")]
        [Required(ErrorMessage = "{0} is verplicht")]
        [Compare("wachtwoord", ErrorMessage = "Wachtwoorden komen niet overeen.")]
        public string bevestigWachtwoord { get; set; }

        [Display(Name = "Contactpersoon")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public string contactPersoon { get; set; }

    }
}