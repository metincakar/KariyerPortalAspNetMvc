using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace KagiderKariyerPortal.Entities.ConCreate
{
    public class Ilan
    {
        public Ilan()
        {
            Users=new List<User>();
        }
        public int IlanId { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int FirmaId { get; set; }
        public virtual Firma Firma { get; set; }

        [Display(Name = "İş Tanımı")]
        public string IlanTitle { get; set; }

        [Display(Name = "Genel Nitelikler")]
        public string Detail { get; set; }

        public string AcikPozisyon { get; set; }

        [Display(Name = "Şehir")]
        public int CityId { get; set; }

        public virtual SetupCity SetupCity { get; set; }

        [Display(Name = "İlçe")]
        public int CountyId { get; set; }

        public virtual SetupCounty SetupCounty { get; set; }

        [Display(Name = "Ülke")]
        public int CountryId { get; set; }

        public virtual SetupCountry SetupCountry { get; set; }

        [Display(Name = "İlan Başlangıç Tarihi")]
        public DateTime IlanStartDate { get; set; }

        [Display(Name = "İlan Bitiş Tarihi")]
        public DateTime IlanFinishDate { get; set; }


        [Display(Name = "Aranan Personel Sayısı")]
        public int ArananPersonelSayisi { get; set; }

        [Display(Name = "Aranan Pozisyon Tipi")]
        public string PozisyonTipi { get; set; }

        [Display(Name = "Aranan Pozisyon Seviyesi")]
        public string PozisyonSeviyesi { get; set; }

        [Display(Name = "Tecrübe")]
        public string Tecrube { get; set; }

        [Display(Name = "Eğitim Seviyesi")]
        public string EgitimSeviyesi { get; set; }
        public List<User> Users { get; set; }

        
    }
}
