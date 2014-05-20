using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HoGent_Stages.Models.Domain
{
    public class ContactModels
    {
        [Required(ErrorMessage = "Naam is verplicht.")]
        public String naam { get; set; }
        public String voornaam { get; set; }
        [Required(ErrorMessage = "Email is verplicht.")]
        public String email { get; set; }
        [Required(ErrorMessage = "Bedrijf is verplicht.")]
        public String bedrijf { get; set; }
        public String omschrijvingBachelorProef { get; set; }
        public String omschrijvingProject { get; set; }
        [Required(ErrorMessage = "Vraag/opmerking is verplicht.")]
        public String vraagOpmerking { get; set; }
    }
}