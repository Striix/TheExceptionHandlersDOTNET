using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HoGent_Stages.Models.Domain;

namespace HoGent_Stages.Models.DAL
{
    public class BedrijfRepository : IBedrijfRepository
    {
        private StagesContext context;
        private DbSet<Bedrijf> bedrijven;

        public BedrijfRepository(StagesContext context)
        {
            this.context = context;
            bedrijven = context.Bedrijf;
        }

        public IEnumerable<Bedrijf> GetAll()
        {
            return bedrijven.AsEnumerable();
        }


        public void Add(Bedrijf bedrijf)
        {
            bedrijven.Add(bedrijf);
        }
        
        public void Delete(Bedrijf bedrijf)
        {
            bedrijven.Remove(bedrijf);
        }

        public bool ControleBedrijf(Bedrijf bedrijf)
        {
            var controle = bedrijven.FirstOrDefault(b => b.email == bedrijf.email);
            if (controle == null)
            {
                return true;
            }
            else
            {
                return false;
            }       
        }

        public Bedrijf FindBy(int bedrijfsId)
        {
            return bedrijven.Find(bedrijfsId);
        }

        public IQueryable<Bedrijf> FindAll()
        {
            return bedrijven.Include(b => b.bedrijfsNaam).OrderBy(b => b.bedrijfsNaam);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

    }
}
