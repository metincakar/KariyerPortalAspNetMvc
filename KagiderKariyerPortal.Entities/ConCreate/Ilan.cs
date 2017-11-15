using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace KagiderKariyerPortal.Entities.ConCreate
{
    public class Ilan
    {
      [Key]
        public int IlanId { get; set; }
      
        public int UserId { get; set; }

      //  public virtual User UserIlan { get; set; }

        public int FirmaId { get; set; }
        public virtual Firma Firma { get; set; }

        [Display(Name = "İş Tanımı")]
        public string IlanTitle { get; set; }

        [Display(Name = "Genel Nitelikler")]
        public string Detail { get; set; }

         [Display(Name = "Aranan Pozisyonlar")]
        public string ArananPozisyonlar { get; set; }

         [Display(Name = "Açık Pozisyon")]
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

        [DataType(DataType.Date)]
        [Display(Name = "İlan Başlangıç Tarihi")]
        [Required(ErrorMessage = "Başlangıç tarihi giriniz")]
        public DateTime IlanStartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "İlan Bitiş Tarihi")]
        [Required(ErrorMessage = "Bitiş tarihi giriniz")]
        public DateTime IlanFinishDate { get; set; }


        [Display(Name = "Aranan Personel Sayısı")]
        [DisplayFormat(ApplyFormatInEditMode = true,
                                 HtmlEncode = false,
                                 NullDisplayText = "1",
                                 DataFormatString = "{0:n}")]

        [Required(ErrorMessage = "Personel sayisi giriniz")]
        public int ArananPersonelSayisi { get; set; }

        [Display(Name = "Aranan Pozisyon Tipi")]
        public int PozisyonTipi { get; set; }

        [Display(Name = "Aranan Pozisyon Seviyesi")]
        public int PozisyonSeviyesi { get; set; }

        [Display(Name = "Tecrübe")]
        public string Tecrube { get; set; }

        [Display(Name = "Eğitim Seviyesi")]
        [Required(ErrorMessage = "Eğitim Seviyesi seçiniz")]
        public int EgitimSeviyesi { get; set; }

        public bool IlanDurum { get; set; }
    }
}
