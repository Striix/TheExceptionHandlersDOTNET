﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using HoGent_Stages.Models.Domain;
using Hogent_Stages.Models.Domain;
using MySql.Data.Entity;

namespace HoGent_Stages.Models.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class stagesContext : DbContext
    {
        public stagesContext()
        {

            Database.SetInitializer<stagesContext>(new DropCreateDatabaseIfModelChanges<stagesContext>()); 
            Database.Initialize(false);
        }

        public DbSet<Bedrijf> Bedrijf { get; set; }
        public DbSet<Stage> Stage { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Student> Student { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Configurations.Add(new BedrijfMap());
            //modelBuilder.Configurations.Add(new StageMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}