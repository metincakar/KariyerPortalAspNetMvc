using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KagiderKariyerPortal.Entities.ConCreate
{
   public class CvEgitimUniversiteBilgi
    {
        [Key]
        public int EgitimId { get; set; }

       [Required(ErrorMessage = "Bu alan gereklidir")]
        public int UserId { get; set; }

        public virtual UserProfile User { get; set; }

        [Required(ErrorMessage = "Okul adı alanı zorunludur")]
        public string OkulAd { get; set; }

      
       [Display(Name = "Bölüm")]
        public string Bolum { get; set; }

        [Display(Name = "Mezuniyet Yılı")]
        public string MezuniyetYil { get; set; }

        [Display(Name = "Mezuniyet Derece")]
        public string MezuniyetDerece { get; set; }

        [Required(ErrorMessage = "Eğitim seviyesi alanı zorunludur")]
        [Display(Name = "Eğitim Seviyesi")]
        public int EgitimTipi { get; set; }

    }
}
