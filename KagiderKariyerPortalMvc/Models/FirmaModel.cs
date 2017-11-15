using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KagiderKariyerPortal.Entities.ConCreate;

namespace KagiderKariyerPortalMvc.Models
{
    public class FirmaModel
    {
        public FirmaModel()
        {
                CalisanSayisiList = new List<CalisanSayisi>
                { 
                    new CalisanSayisi { Id = "1", Sayi = "Yok" },
                    new CalisanSayisi { Id = "2", Sayi = "1-5" },
                    new CalisanSayisi { Id = "3", Sayi = "6-10" },
                    new CalisanSayisi { Id = "4", Sayi = "11-15" },
                    new CalisanSayisi { Id = "5", Sayi = "16-20" },
                    new CalisanSayisi { Id = "6", Sayi = "21-25" },
                    new CalisanSayisi { Id = "7", Sayi = "16-50" },
                    new CalisanSayisi { Id = "8", Sayi = "51-100" },
                    new CalisanSayisi { Id = "9", Sayi = "100 den fazla" }
                };
        }
        public List<CalisanSayisi> CalisanSayisiList { get; set; }
        public Firma Firma { get; set; }

        public string[] Sektorler { get; set; }
        public CitiesViewModel CitiesViewModel { get; set; }
    }

    public class CalisanSayisi
    {
        public string Id { get; set; }
        public string Sayi { get; set; }
    }
}