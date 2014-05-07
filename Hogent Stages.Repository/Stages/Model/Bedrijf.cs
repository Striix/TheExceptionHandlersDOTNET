    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Globalization;
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

            public string logo
            {
                get
                {
                    throw new System.NotImplementedException();
                }
                set
                {
                }
            }

            public Stage Stage
            {
                get
                {
                    throw new System.NotImplementedException();
                }
                set
                {
                }
            }

            public stage AddStage()
            {
                throw new System.NotImplementedException();
            }

            public stage RemoveStage()
            {
                throw new System.NotImplementedException();
            }

            public stage EditStage()
            {
                throw new System.NotImplementedException();
            }

            public bedrijf EditData()
            {
                throw new System.NotImplementedException();
            }

            public Mentor AddMentor()
            {
                throw new System.NotImplementedException();
            }

            public Mentor EditMentor()
            {
                throw new System.NotImplementedException();
            }

            public Mentor RemoveMentor()
            {
                throw new System.NotImplementedException();
            }

            public stage FindStage()
            {
                throw new System.NotImplementedException();
            }

            public stage ShowHistory()
            {
                throw new System.NotImplementedException();
            }

            public stage ShowCurrentStages()
            {
                throw new System.NotImplementedException();
            }



        }
    }
