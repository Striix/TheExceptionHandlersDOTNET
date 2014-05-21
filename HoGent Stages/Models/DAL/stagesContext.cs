using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using HoGent_Stages.Models.DAL.Mapper;
using HoGent_Stages.Models.Domain;
using MySql.Data.Entity;

namespace HoGent_Stages.Models.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class StagesContext : DbContext
    {
        public StagesContext()
        {

            Database.SetInitializer<StagesContext>(new DropCreateDatabaseAlways<StagesContext>()); 
           // Database.Initialize(false);
        }


        public DbSet<Bedrijf> Bedrijf { get; set; }
        public DbSet<Stage> Stage { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Mentor> Mentor { get; set; }
        public DbSet<Begeleider> Begeleider { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new BedrijfMapper());
            modelBuilder.Configurations.Add(new StageMapper());
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
        }
    }
}