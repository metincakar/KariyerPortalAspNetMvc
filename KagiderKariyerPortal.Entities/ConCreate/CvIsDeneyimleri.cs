using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KagiderKariyerPortal.Entities.ConCreate
{
   public class CvIsDeneyimleri
    {
        [Key]
        public int IsDeneyimleriId { get; set; }
       
        [Required]
        public int UserId { get; set; }
        public virtual UserProfile User { get; set; }
      [Required(ErrorMessage = "Bu alan gereklidir")]
        [Display(Name = "Firma Adı")]
        public string FirmaAdi { get; set; }

        [Display(Name = "Başlangıç Tarihi")]
        [Required(ErrorMessage = "Bu alan gereklidir")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BaslangicTarihi { get; set; }


      [Required(ErrorMessage = "Bu alan gereklidir")]
        [Display(Name = "Bitiş Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BitisTarihi { get; set; }


       [Required(ErrorMessage = "Bu alan gereklidir")]
        [Display(Name = "Firmadaki Pozisyon")]
        public string FirmadakiPozisyon { get; set; }


        [Display(Name = "İş Tanımı")]
        public string Istanimi { get; set; }

          [Display(Name = "Firma Sektörü")]
        public string FirmaSektoru { get; set; }

         [Display(Name = "İş Alanı")]
        public string IsAlani { get; set; }

    }
}
