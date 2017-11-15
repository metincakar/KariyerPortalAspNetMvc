using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KagiderKariyerPortalMvc.Models
{
    public class KategoriModel
    {
        public int KategoriId { get; set; }

        public string KategoriAd { get; set; }

        public string AltKategoriDegeri { get; set; }
        public int AltKategoriId { get; set; }
    }
    public enum EnumCategories
    {  
        Pozisyon=1,
        Sektör=2,
         [Display(Name = "Sınav Bilgileri")]
        SınavBilgileri=3,
         [Display(Name = "Yabancı Dil")]
        YabancıDil=4,
        Üniversite=5,
        Departmanlar = 6,
    }
}