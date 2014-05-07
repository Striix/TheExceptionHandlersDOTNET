using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hogent_Stages.Models.Domain;

namespace HoGent_Stages.Models.Domain
{
    public interface IStageRepository
    {
        Stage FindBy(int stagesId);
        IQueryable<Stage> FindAll();
        void Add(Stage stage);
        void Delete(Stage stage);
        void SaveChanges();
        IEnumerable<Stage> GetAll();
    }
}