using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hogent_Stages.Repository.Stages;

namespace HoGent_Stages.Models.Domain
{
    public interface IBedrijfRepository
    {
        Bedrijf FindBy(int bedrijfsId);
        IQueryable<Bedrijf> FindAll();
        void Add(Bedrijf bedrijf);
        void Delete(Bedrijf bedrijf);
        void SaveChanges();
        IEnumerable<Bedrijf> GetAll();
    }
}
