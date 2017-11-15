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

        public DbSet<UserProfile> Users { get; set; }
        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
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

        public DbSet<IlanBasvuru> IlanBasvurus { get; set; }

        public DbSet<CvOther> CvOthers { get; set; }

        public DbSet<CvSertifikaBilgi> CvSertifikaBilgis { get; set; }

        public DbSet<CvSeminerler> CvSeminerlers { get; set; }

        public DbSet<CvSinavBilgi> CvSinavBilgis { get; set; }
    }
}
