﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hogent_Stages.Repository.Stages.Model
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Naam van de student")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public String naam { get; set; }

        [Display(Name = "Achternaam van de student")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public String voorNaam { get; set; }

        [Display(Name = "Straat")]
        public String straat { get; set; }

        [Display(Name = "Huisnummer")]
        [RegularExpression("\\d+", ErrorMessage = "{0} moet een getal zijn")]
        public int nummer { get; set; }

        [Display(Name = "Postcode")]
        [RegularExpression("\\d+", ErrorMessage = "{0} moet een getal zijn")]
        public String postcode { get; set; }

        [Display(Name = "Plaatsnaam")]
        public String plaats { get; set; }

        [Display(Name = "email")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public String email { get; set; }

        [Display(Name = "wachtwoord")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public String wachtwoord { get; set; }

        [Display(Name = "Gsm-nummer")]
        public int gsm { get; set; }

        public string Foto { get; set; }
      
        public Hogent_Stages.Repository.Stages.Stage Stage { get; set; }

        public Stage FindStage()
        {
            throw new System.NotImplementedException();
        }

        public Student EditData()
        {
            throw new System.NotImplementedException();
        }

        public Stage ShowAllStages()
        {
            return FindAll();
        }

        public Stage ChooseStage()
        {
            throw new System.NotImplementedException();
        }

        public Stage ChangeChoice()
        {
            throw new System.NotImplementedException();
        }

    }
}