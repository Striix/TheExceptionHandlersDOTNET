using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hogent_Stages.Models.Domain
{
    public class Begeleider
    {
        public string voornaam { get; set; }
    
        public string naam { get; set; }

        public string email { get; set; }

        public string straat { get; set; }

        public int huisnummer { get; set; }

        public int postcode { get; set; }

        public string plaats { get; set; }

        public int telefoon { get; set; }

        public int gsm { get; set; }

        public string wachtwoord { get; set; }

        public string foto { get; set; }

        public Stage stage { get; set; }

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



        public void GivePreference()
        {
            throw new System.NotImplementedException();
        }

        public void GiveOverview()
        {
            throw new System.NotImplementedException();
        }

        public Stage ChangeData()
        {
            throw new System.NotImplementedException();
        }

        public Begeleider ChangePersonalData()
        {
            throw new System.NotImplementedException();
        }
    }
}
