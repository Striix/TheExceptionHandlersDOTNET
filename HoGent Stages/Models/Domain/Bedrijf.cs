    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Globalization;
    using System.Linq;
    using System.Web;
    using System.ComponentModel;
    using HoGent_Stages.Models.DAL;
    using Hogent_Stages.Models.Domain;

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
            public virtual ICollection<Mentor> mentors { get; set; }

            public Hogent_Stages.Models.Domain.Stage Stage
            {
                get
                {
                    throw new System.NotImplementedException();
                }
                set
                {
                }
            }



            public Bedrijf()
            {
                stages = new List<Stage>();
            }

            public void AddStage(Stage stage)
            {
                if (stages.FirstOrDefault(s => s.titel == stage.titel) == null)
                {
                    stages.Add(stage);
                }  
            }

            public void RemoveStage(int stageId)
            {
                stages.Remove(stages.FirstOrDefault(s => s.Id == stageId));
            }

            public void WijzigStage(Stage stage, int stageId)
            {
                var wijzig = stages.FirstOrDefault(s => s.Id == stageId);
                stages.Remove(wijzig);
                stages.Add(new Stage());
            }

            public void AddMentor(Mentor mentor)
            {
                mentors.Add(mentor);   
            }


        }
    }
