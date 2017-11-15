using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KagiderKariyerPortal.Entities.ConCreate
{
    public class CvIletisimBilgi
    {
        [Key]
        public int IletisimBilgiId { get; set; }
        public int UserId { get; set; }
    
        [Display(Name = "Adım")]
        [Required(ErrorMessage = "Ad giriniz!")]
        
        public string UyeAd { get; set; }

        [Display(Name = "Soyadım")]
        [Required(ErrorMessage = "Soyad giriniz!")]
        public string UyeSoyad { get; set; }

         [Display(Name = "Email")]
         [Required(ErrorMessage = "Mail giriniz!")]
        public string UyeMail { get; set; }

         [Display(Name = "Mobil Tel")]
         [Required(ErrorMessage = "Mobil tel giriniz!")]
        [DataType(DataType.PhoneNumber)]
        public string MobilTel { get; set; }

         [Display(Name = "Ev Tel")]
         [DataType(DataType.PhoneNumber)]
        public string EvTel { get; set; }
        public string Adres { get; set; }

        [Display(Name = "Şehir")]
        public int CityId { get; set; }

        public virtual SetupCity SetupCity { get; set; }

        [Display(Name = "İlçe")]
        public int CountyId { get; set; }

        public virtual SetupCounty SetupCounty { get; set; }

        public string Semt { get; set; }

         [Display(Name = "Ülke")]
        public int CountryId { get; set; }

        public virtual  SetupCountry SetupCountry { get; set; }

      

        public string OzgecmisOzet { get; set; }

        public string EgitimDurumu { get; set; }
        
        [Display(Name = "Kişisel Web Sayfası")]
        public string WebLink { get; set; }
    }
}
