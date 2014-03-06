using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace HoGent_Stages
{
    public class Stage
    {
        public Stage()
        {
            throw new System.NotImplementedException();
        }

        public string functie { get; set; }
        public string beschrijving { get; set; }
        public int semester { get; set; }
        public string mentorNaam { get; set; }
        public int gsmNummer { get; set; }
    }
}
