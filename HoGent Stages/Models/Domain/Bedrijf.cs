using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace HoGent_Stages
{
    public class Bedrijf
    {
        public Bedrijf()
        {
            throw new System.NotImplementedException();
        }

        public String bedrijfsNaam { get; set; }

        public String straat { get; set; }

        public int nummer { get; set; }

        public int postcode { get; set; }

        public String plaats { get; set; }

        public string url { get; set; }

        public string email { get; set; }

        public string telefoon { get; set; }

        public string bereikbaarheid { get; set; }

        public string wachtwoord { get; set; }
    }
}
