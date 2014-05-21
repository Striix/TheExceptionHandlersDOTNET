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
            public virtual Mentor MentorBedrijf { get; set; } 

            public Bedrijf()
            {
                stages = new List<Stage>();
            }

            public void Mail()
            {
                var myMailMessage = new System.Net.Mail.MailMessage();
                myMailMessage.From = new System.Net.Mail.MailAddress("gauthier.meert.gm@gmail.com");
                myMailMessage.To.Add("gauthier_meert@hotmail.com");// Mail would be sent to this address
                myMailMessage.Subject = "Feedback registratie";
                myMailMessage.Body = "Uw registratie is geslaagd!";

                var smtpServer = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                smtpServer.Port = 587;
                smtpServer.Credentials = new System.Net.NetworkCredential("gauthier.meert.gm@gmail.com", "philips69");
                smtpServer.EnableSsl = true;
                smtpServer.Send(myMailMessage);
            }

            public Stage VoegStageToe(Stage stage)
            {
                if (stages.FirstOrDefault(s => s.Titel == stage.Titel) != null)
                    throw new ArgumentException("Er bestaat al een stage met dezelfde titel");
                stage.ToegevoegDateTime = DateTime.Now;
                stages.Add(stage);
                return stage;
            }

            public void VerwijderStage(Stage stage)
            {
                var stageVerwijderen = stages.FirstOrDefault(s => s.Id == stage.Id);
                if (!stages.Contains(stageVerwijderen))
                    throw new ArgumentException(string.Format("{0} is geen stage van {1}", stage.Titel, this.bedrijfsNaam));
                stages.Remove(stageVerwijderen);
            }

            public void WijzigStage(Stage stage)
            {
                var wijzig = stages.FirstOrDefault(s => s.Id == stage.Id);
                wijzig.Titel = stage.Titel;
                wijzig.Omschrijving = stage.Omschrijving;
                wijzig.Specialisatie = stage.Specialisatie;
                wijzig.Semester = stage.Semester;
                wijzig.AantalStudenten = stage.AantalStudenten;
                wijzig.ToegevoegDateTime = DateTime.Now;
                wijzig.MentorNaam = stage.MentorNaam;
            }

            public void VoegMentorToe(Mentor mentor)
            {
               stagesContext db = new stagesContext();
                MentorBedrijf = mentor;
                db.Mentor.Add(MentorBedrijf);
            }

        }
    }
