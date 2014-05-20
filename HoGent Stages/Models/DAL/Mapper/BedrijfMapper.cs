using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using HoGent_Stages.Models.Domain;

namespace HoGent_Stages.Models.DAL.Mapper
{
    public class BedrijfMapper : EntityTypeConfiguration<Bedrijf>
    {
        public BedrijfMapper()
        {
            Property(b => b.bedrijfsNaam).IsRequired().HasMaxLength(100);
            ToTable("Bedrijf");

            HasMany(b => b.stages).WithRequired().Map(s => s.MapKey("BedrijfId")).WillCascadeOnDelete(false );
            HasOptional(m => m.MentorBedrijf).WithRequired().Map(s => s.MapKey("BedrijfId")).WillCascadeOnDelete(false);
            
        }


    }
}