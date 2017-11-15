using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KagiderKariyerPortal.Entities.ConCreate
{
   public class CvDilBilgi
    {
       [Key]
        public int DilBilgId { get; set; }
         [Required]
        public int UserId { get; set; }


       [Required]
       [Display(Name = "Yabancı Dil Seçimi *")]
        public string Dil { get; set; }

        public string Okuma { get; set; }
        public string Yazma { get; set; }
        public string Konusma { get; set; }
    }
}
