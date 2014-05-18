using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.ComponentModel;

namespace HoGent_Stages.Models.Domain
{
    public class Stage
    {
        [Key] 
        public int Id { get; set; }

        [Display(Name="Titel")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public string titel { get; set; }

        [Display(Name = "Omschrijving")]
        [DataType(DataType.MultilineText)]
        public string omschrijving { get; set; }

        [Display(Name = "Specialisatie")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public string specialisatie { get; set; }

        [Display(Name = "Semester")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public int semester { get; set; }

        [Display(Name = "Aantal studenten")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public int aantalStudenten { get; set; }

        [Display(Name = "Naam mentor")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public string mentorNaam { get; set; }

        [Display(Name="Datum toegevoegd")]
        [Required]
        public DateTime ToegevoegDateTime { get; set; }

        //public virtual Bedrijf Bedrijf { get; set; }

    }
}
