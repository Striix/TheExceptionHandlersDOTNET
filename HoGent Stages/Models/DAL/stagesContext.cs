using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using HoGent_Stages.Models.DAL.Mapping;
using MySql.Data.Entity;

namespace HoGent_Stages.Models.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class stagesContext : DbContext
    {
        public stagesContext() : base("stages")
        {
            
        }
        public DbSet<Bedrijf> Bedrijf { get; set; }
        public DbSet<Stage> Stage { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new BedrijfMap());
            modelBuilder.Configurations.Add(new StageMap());
        }
    }
}