using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KagiderKariyerPortal.Entities.ConCreate
{
    public class CvSertifikaBilgi
    {
        public int UserId { get; set; }

        [Key]
        public int SertifikaId { get; set; }

        [Display(Name = "Sertifika Adı")]
        [Required(ErrorMessage = "Bu alan gereklidir")]
        public string SertifikaAd { get; set; }

        [Display(Name="Sertifika Tarihi")]
        public string SertifikaAy { get; set; }

        [Display(Name = "Sertifika Tarihi")]
        public int SertifikaSene { get; set; }

        [Display(Name="Alındığı Kurum")]
        public string AlindigiKurum { get; set; }
        public string Aciklama { get; set; }
    }
}
