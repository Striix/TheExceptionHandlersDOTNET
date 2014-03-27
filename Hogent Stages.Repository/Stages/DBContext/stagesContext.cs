using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Hogent_Stages.Repository.Stages.Model;
using MySql.Data.Entity;

namespace Hogent_Stages.Repository.Stages.DBContext
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class StagesContext : DbContext
    {
        public StagesContext()
        {

            Database.SetInitializer<StagesContext>(new DropCreateDatabaseIfModelChanges<StagesContext>()); 
            Database.Initialize(false);
        }
        public DbSet<Bedrijf> Bedrijf { get; set; }
       // public DbSet<Stage> Stage { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Configurations.Add(new BedrijfMap());
            //modelBuilder.Configurations.Add(new StageMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}