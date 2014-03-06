using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HoGent_Stages.Models.DAL
{
    public class stagesInitializer : DropCreateDatabaseAlways<stagesContext>
    {
        protected override void Seed(stagesContext context)
        {
            var bedrijven = new List<Bedrijf>
            {
                new Bedrijf
                {
                    bedrijfsNaam = "VPK Packaging",
                    bereikbaarheid = "Openbaar vervoer",
                    email = "astriid_dl@hotmail.com",
                    nummer = 10,
                    plaats = "Woubrechtegem",
                    postcode = 9550,
                    straat = "sleistraat",
                    telefoon = "053631679",
                    url = "www.google.be",
                    wachtwoord = "blabla"
                }
            };
            bedrijven.ForEach(s => context.Bedrijf.Add(s));
            context.SaveChanges();
            var stages = new List<Stage>
            {
                new Stage
                {
                    beschrijving = "Telefoons openemen, programmeren",
                    functie = "Programmeur",
                    gsmNummer = 0472094005,
                    mentorNaam = "Astrid De Landsheer",
                    semester = 2
                }
            };
            stages.ForEach(s => context.Stage.Add(s));
            context.SaveChanges();
        }
    }
}