using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HoGent_Stages.Models.Domain;

namespace HoGent_Stages.Models.DAL
{
    public class BegeleiderRepository : IBegeleiderRepository
    {

        private stagesContext context;
        private DbSet<Begeleider> begeleiders;

        public BegeleiderRepository(stagesContext context)
            {
                this.context = context;
                begeleiders = context.Begeleider;
            }

            public IEnumerable<Begeleider> GetAll()
            {
                return begeleiders.AsEnumerable();
            }

            public void Add(Begeleider begeleider)
            {
                begeleiders.Add(begeleider);
            }

            public void Delete(Begeleider begeleider)
            {
                begeleiders.Remove(begeleider);
            }

            public Begeleider FindBy(int begeleiderId)
            {
                return begeleiders.Find(begeleiderId);
            }

            public IQueryable<Begeleider> FindAll()
            {
                return begeleiders.Include(b => b.email).OrderBy(b => b.email);
            }

            public void SaveChanges()
            {
                context.SaveChanges();
            }

    }
}