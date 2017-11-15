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

        [Required]
        public int UserId { get; set; }

        public int UniversiteId { get; set; }

        [Required]
        public string UniversiteAd { get; set; }

         [Required]
       [Display(Name = "Bölüm")]
        public string Bolum { get; set; }

        [Display(Name = "Mezuniyet Yılı")]
        public string MezuniyetYil { get; set; }

        [Display(Name = "Mezuniyet Derece")]
        public string MezuniyetDerece { get; set; }
       public int EgitimTipi { get; set; }

    }
}
