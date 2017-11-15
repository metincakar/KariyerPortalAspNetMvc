using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using Microsoft.SqlServer.Server;

namespace KagiderKariyerPortal.Entities.ConCreate
{
  public  class CvOther
    {

      [Key]
      public int OtherId { get; set; }
        public int UserId { get; set; }

        public string BilgisayarBilgi { get; set; }

         [Display(Name = "Cv Özel Bilgi")]
        public string OzetBilgi { get; set; }

      [Display(Name = "Sertifika bilgileriniz")]
      public string SertifikaBilgi { get; set; }

      [Display(Name = "Burslar ve projeler")]
      public string BurslarProjeler { get; set; }

      public string Hobiler { get; set; }

        [Display(Name = "Sosyal Sorumluluk Çalışmaları")]
      public string SosyalSorumlulukCalismalari { get; set; }

      [Display(Name = "Üye Olunan Kuruluşlar-Sivil Toplum Kuruluşları")]
      public string Uyelikler { get; set; }

       [Display(Name = "Calişma Durumu Tercihiniz")]
      public string CalismaDurumu { get; set; }

       [Display(Name = "Çalışmak İstediği Pozisyon")]
      public string IstenilenPozisyon { get; set; }

       [Display(Name = "Çalışmak İstediği Alan")]
      public string CalismakIstedigiAlan { get; set; }

       [Display(Name = "Seminer ve Kurslar")]
      public string SeminerVeKurslar { get; set; }

       [Display(Name = "Referans 1 Ad Soyad")]
      public string Ref1AdSoyad { get; set; }

        [Display(Name = "Pozisyon")]
      public string Ref1Pozisyon { get; set; }

      [Display(Name = "Telefon")]
      [DataType(DataType.PhoneNumber)]
      public string Ref1Tel { get; set; }

      [Display(Name = "Eposta")]
      [DataType(DataType.EmailAddress)]
      public string Ref1Mail { get; set; }

      [Display(Name = "Referans 2 Ad Soyad")]
      public string Ref2AdSoyad { get; set; }

           [Display(Name = "Pozisyon")]
      public string Ref2Pozisyon { get; set; }

       [Display(Name = "Telefon")]
       [DataType(DataType.PhoneNumber)]
      public string Ref2Tel { get; set; }

       [Display(Name = "Eposta")]
       [DataType(DataType.EmailAddress)]
      public string Ref2Mail { get; set; }







    }
}
