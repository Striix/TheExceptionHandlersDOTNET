using System;
using System.Web;
using System.ComponentModel;

namespace Hogent_Stages.Repository.Stages
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
