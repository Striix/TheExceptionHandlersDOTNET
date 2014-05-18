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

            [Display(Name = "E-mailadres")]
            [Required(ErrorMessage = "{0} is verplicht")]
            [EmailAddress(ErrorMessage = "{0} moet een geldig zijn.")]
            public string email { get; set; }

            [Display(Name = "Telefoonnummer")]
            [Required(ErrorMessage = "{0} is verplicht")]
            public string telefoon { get; set; }

            [Display(Name = "Bereikbaarheid")]
            [Required(ErrorMessage = "{0} is verplicht")]
            public string bereikbaarheid { get; set; }

            [Display(Name = "Wachtwoord")]
            [Required(ErrorMessage = "{0} is verplicht")]
            public string wachtwoord { get; set; }

            [Display(Name = "Bevestig wachtwoord")]
            [Required(ErrorMessage = "{0} is verplicht")]
            [NotMapped]
            [Compare("wachtwoord", ErrorMessage = "Wachtwoorden komen niet overeen.")]
            public string bevestigWachtwoord { get; set; }

            [Display(Name = "Contactpersoon")]
            [Required(ErrorMessage = "{0} is verplicht")]
            public string contactPersoon { get; set; }

            public virtual ICollection<Stage> stages { get; set; }
            public virtual Mentor mentor { get; set; } 

            public Bedrijf()
            {
                stages = new List<Stage>();
            }

            public Stage VoegStageToe(Stage stage)
            {
                if (stages.FirstOrDefault(s => s.titel == stage.titel) != null)
                    throw new ArgumentException("Er bestaat al een stage met dezelfde titel");
                stage.ToegevoegDateTime = DateTime.Now;
                stages.Add(stage);
                return stage;
            }

            public void VerwijderStage(Stage stage)
            {
                var stageVerwijderen = stages.FirstOrDefault(s => s.Id == stage.Id);
                if (!stages.Contains(stageVerwijderen))
                    throw new ArgumentException(string.Format("{0} is geen stage van {1}", stage.titel, this.bedrijfsNaam));
                stages.Remove(stageVerwijderen);
            }

            public void WijzigStage(Stage stage)
            {
                var wijzig = stages.FirstOrDefault(s => s.Id == stage.Id);
                wijzig.titel = stage.titel;
                wijzig.omschrijving = stage.omschrijving;
                wijzig.specialisatie = stage.specialisatie;
                wijzig.semester = stage.semester;
                wijzig.aantalStudenten = stage.aantalStudenten;
                wijzig.ToegevoegDateTime = DateTime.Now;
                wijzig.mentorNaam = stage.mentorNaam;
            }

            public void VoegMentorToe(Mentor mentor)
            {
                mentors.Add(mentor);   
            }

        }
    }
