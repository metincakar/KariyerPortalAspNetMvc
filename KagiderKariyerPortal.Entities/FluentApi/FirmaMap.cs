using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using KagiderKariyerPortal.Entities.ConCreate;

namespace KagiderKariyerPortal.Entities.FluentApi
{
   public class FirmaMap:EntityTypeConfiguration<Firma>
   {
       public FirmaMap()
       {
           this.HasKey(f => f.FirmaId);

           this.Property(f => f.FirmaId).IsRequired();

           this.Property(f => f.FirmaAd).IsRequired().HasMaxLength(250);

           this.Property(f => f.Adres).HasMaxLength(750);

           this.Property(f => f.FirmaCalismaAlanlari).HasMaxLength(750);
           this.Property(f => f.Email).HasMaxLength(50);
       }
    }
}
