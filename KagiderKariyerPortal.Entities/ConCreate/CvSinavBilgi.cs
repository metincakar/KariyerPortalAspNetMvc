using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KagiderKariyerPortal.Entities.ConCreate
{
    public  class CvSinavBilgi
    {
        public int UserId { get; set; }

        [Key]
        public int SinavId { get; set; }

        public int SinavTuru { get; set; }

        [Display(Name = "Sınav Adı")]
        public string SinavAdi { get; set; }


        [Display(Name = "Sınav Yapan Kuruluş")]
        public string SinavYapanKurulus { get; set; }

        [Display(Name = "Sınav Tarihi")]
        public string SinavTarih { get; set; }

          [Display(Name = "Sınav Skor")]
        public string SinavSkor { get; set; }

          [Display(Name = "Sınav Açıklama")]
        public string Aciklama { get; set; }
    }
}
