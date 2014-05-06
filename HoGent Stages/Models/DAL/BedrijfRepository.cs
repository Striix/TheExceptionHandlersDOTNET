﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HoGent_Stages.Models.Domain;
using Hogent_Stages.Repository.Stages;
using Hogent_Stages.Repository.Stages.DBContext;

namespace HoGent_Stages.Models.DAL
{
    public class BedrijfRepository : IBedrijfRepository
    {
        private stagesContext context;
        private DbSet<Bedrijf> bedrijven;

        public BedrijfRepository(stagesContext context)
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