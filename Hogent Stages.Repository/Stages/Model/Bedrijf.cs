using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

using System.ComponentModel;

namespace Hogent_Stages.Repository.Stages
{
    public class Bedrijf
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Naam van het bedrijf")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public String bedrijfsNaam { get; set; }

        [Display(Name = "De straatnaam")]
        public String straat { get; set; }

        [Display(Name = "Het nummer")]
        public int nummer { get; set; }

        [Display(Name = "De postcode")]
        public int postcode { get; set; }

        [Display(Name = "De plaatsnaam")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public String plaats { get; set; }

        public string url { get; set; }

        [Display(Name = "Het e-mailadres")]
        [Required(ErrorMessage = "{0} is verplicht")]
        [EmailAddress(ErrorMessage = "{0} moet een geldig emailadres zijn.")]
        public string email { get; set; }

        [Display(Name = "Telefoonnummer van het bedrijf")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public string telefoon { get; set; }

        public string bereikbaarheid { get; set; }

        [Display(Name = "Een zelfgekozen wachtwoord")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public string wachtwoord { get; set; }

        [Display(Name = "Bevestig wachtwoord")]
        [Required(ErrorMessage = "{0} is verplicht")]
        [Compare("wachtwoord")]
        public string bevestigWachtwoord { get; set; }

        [Display(Name = "Naam van de contactpersoon")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public string contactPersoon { get; set; }
    }
}
