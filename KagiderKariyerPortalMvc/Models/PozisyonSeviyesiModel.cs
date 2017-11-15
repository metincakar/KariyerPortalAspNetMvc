using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KagiderKariyerPortalMvc.Models
{
    public class PozisyonSeviyesiModel
    {
        public PozisyonSeviyesiModel()
        {
            PozisyonSeviyesiList = new List<PozisyonSeviyesi>()
            {
                new PozisyonSeviyesi() {Id = 0, Seviye = "Uzman"},
                new PozisyonSeviyesi() {Id = 1, Seviye = "Uzman Yardımcısı"},
                new PozisyonSeviyesi() {Id = 2, Seviye = "Stajyer"},
                new PozisyonSeviyesi() {Id = 3, Seviye = "Diğer"}
            };
        }

        public class PozisyonSeviyesi
        {
            public int Id { get; set; }
            public string Seviye { get; set; }
        }

        public List<PozisyonSeviyesi> PozisyonSeviyesiList { get; set; }
    }
}