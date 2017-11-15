using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KagiderKariyerPortal.Entities.ConCreate
{
   public class Firma
    {

       [Key]
       public int FirmaId { get; set; }

       public int UserId { get; set; }
       public User User { get; set; }

       [Display(Name = "Firma Adı")]
       [Required(ErrorMessage = "Firma Adı alanı zorunludur.")]
       public string FirmaAd { get; set; }


       [Display(Name = "Sorumlu Ad")]
       public string SorumluAd { get; set; }

        [Display(Name = "Sorumlu Soyad")]
       public string SorumluSoyAd { get; set; }

      [Display(Name = "Sorumlu Pozisyon")]
       public string SorumluPozisyon { get; set; }

        [Display(Name = "Email Adres")]
       public string Email { get; set; }

          [Display(Name = "Telefon")]
       public string Tel { get; set; }

       public string Adres { get; set; }

           [Display(Name = "Firma Hakkında Kısa Bilgi")]
       public string FirmaHakkinda { get; set; }

        [Display(Name = "Faliyet Gözterdiği İller")]
       public string FaliyetGosterdigiIller { get; set; }

           [Display(Name = "Firma Çalışma Alanları")]
       public string FirmaCalismaAlanlari { get; set; }

          [Display(Name = "Çalışan sayısı")]
       public int CalisanSayisi { get; set; }

       [Display(Name = "Firma Logo")]
        public string FirmaLogo { get; set; }


    }
}
