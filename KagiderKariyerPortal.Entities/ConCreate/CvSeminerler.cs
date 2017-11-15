using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KagiderKariyerPortal.Entities.ConCreate
{
   public class CvSeminerler
    {
        public int UserId { get; set; }

        [Key]
        public int SeminerId { get; set; }

        [Display(Name = "Egitim Adı")]
        [Required(ErrorMessage = "Bu alan gereklidir")]
        public string EgitimAd { get; set; }
   
        [Display(Name = "Egitim Kurumu")]
        [Required(ErrorMessage = "Bu alan gereklidir")]
        public string EgitimKurumu { get; set; }

        [Display(Name = "Başlangıç Tarihi")]
        public string EgitimBaslangic { get; set; }

         [Display(Name = "Bitiş Tarihi")]
        public string EgitimBitis { get; set; }

         [Display(Name = "Süre(Saat)")]
        public int EgitimSure { get; set; }

         [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }
    }
}
