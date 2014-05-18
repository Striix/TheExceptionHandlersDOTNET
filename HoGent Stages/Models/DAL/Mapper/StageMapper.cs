using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using HoGent_Stages.Models.Domain;

namespace HoGent_Stages.Models.DAL.Mapper
{
    public class StageMapper : EntityTypeConfiguration<Stage>
    {
        public StageMapper()
        {
            Property(t => t.titel).IsRequired().HasMaxLength(100);

            //Table
            ToTable("Stage");

        }
    }
}