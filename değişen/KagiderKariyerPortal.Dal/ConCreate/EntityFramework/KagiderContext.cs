using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using KagiderKariyerPortal.Entities.ConCreate;

namespace KagiderKariyerPortal.Dal.ConCreate.EntityFramework
{
    public class KagiderContext:DbContext
    {
       // public DbSet<UyeBilgi> UyeBilgis { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<CvIletisimBilgi> CvIletisimBilgis { get; set; }

        public DbSet<CvKisiselBilgi> CvKisiselBilgis { get; set; }

        public DbSet<CvIsDeneyimleri> CvIsDeneyimleris { get; set; }

        public DbSet<CvEgitimUniversiteBilgi> CvEgitimUniversiteBilgis { get; set; }

        public DbSet<CvDilBilgi> CvDilBilgis { get; set; }

        public DbSet<SetupCity> SetupCities { get; set; }

        public DbSet<SetupCountry> SetupCountries { get; set; }

        public DbSet<SetupCounty>  SetupCounties  { get; set; }

        public DbSet<Ilan> Ilanlar { get; set; }

        public DbSet<Firma> Firmas { get; set; }
    }
}
