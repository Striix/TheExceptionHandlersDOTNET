using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Hogent_Stages.Models.Domain;
using HoGent_Stages.Models.Domain;

namespace HoGent_Stages.Models.DAL
{
    public class StageRepository : IStageRepository
    {
        private stagesContext context;
        private DbSet<Stage> stages;

        public StageRepository(stagesContext context)
        {
            this.context = context;
            stages = context.Stage;
        }

        public IEnumerable<Stage> GetAll()
        {
            return stages.AsEnumerable();
        }

        public void Add(Stage stage)
        {
            stages.Add(stage);
        }

        public void Delete(Stage stage)
        {
            stages.Remove(stage);
        }

        public Stage FindBy(int stagesId)
        {
            return stages.Find(stagesId);
        }

        public IQueryable<Stage> FindAll()
        {
            return stages.Include(b => b.titel).OrderBy(b => b.titel);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Update(Stage stage)
        {
            stages.Attach(stage);
        }

    }
}