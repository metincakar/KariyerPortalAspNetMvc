using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;

namespace KagiderKariyerPortal.Entities.ConCreate
{
    public class CvKisiselBilgi
    {
        [Key]
        public int KisiselBilgiId { get; set; }
    
        public int UserId { get; set; }
        public bool Cinsiyet { get; set; }

        
        [Display(Name = "Medeni Durum")]
        public bool MedeniDurum { get; set; }
      
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Doğum Tarihi")]
        [Required(ErrorMessage = "Doğum tarihi alanı zorunludur")]
        public DateTime DogumTarihi { get; set; }

        [Display(Name = "Doğum Yeri")]
        [Required(ErrorMessage = "Doğum yeri alanı zorunludur")]
        public int CityId { get; set; }

        public virtual SetupCity SetupCity { get; set; }

        [Display(Name = "Sürücü Belge Var mı?")]
        public bool SurucuBelge { get; set; }

        [Display(Name = "Sürücü Belge Sınıfı")]
        public string SurucuBelgeSinifi { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Sürücü Belge Veriliş Tarihi")]
        public DateTime SurucuBelgeVerilisTarih { get; set; }

       
        [Display(Name = "Uyruk")]
        [Required(ErrorMessage = "Uyruk alanı zorunludur")]

        public int CountryId { get; set; }
        public virtual SetupCountry SetupCountry { get; set; }

        //public virtual Askerlik Askerlik { get; set; }
     
        [Display(Name = "Askerlik Durumu")]
        public string AskerlikDurum { get; set; }

        [Display(Name = "Tc Kimlik No")]
        public string TcNo { get; set; }


    }
}
