using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogent_Stages.Repository.Stages.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "E-mailadres")]
        [Required(ErrorMessage = "{0} is verplicht")]
        [EmailAddress(ErrorMessage = "{0} moet een geldig zijn.")]
        public string email { get; set; }

        [Display(Name = "Wachtwoord")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public string wachtwoord { get; set; }

        [Display(Name = "Bevestig wachtwoord")]
        [Required(ErrorMessage = "{0} is verplicht")]
        [NotMapped]
        [Compare("wachtwoord", ErrorMessage = "Wachtwoorden komen niet overeen.")]
        public string bevestigWachtwoord { get; set; }

        public string rol { get; set; }



    }
}
