using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoGent_Stages.Models.Domain
{
    public interface IBegeleiderRepository
    {
        Begeleider FindBy(int begeleiderId);
        IQueryable<Begeleider> FindAll();
        void Add(Begeleider begeleider);
        void Delete(Begeleider begeleider);
        void SaveChanges();
        IEnumerable<Begeleider> GetAll();
    }
}