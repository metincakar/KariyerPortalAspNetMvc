using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KagiderKariyerPortalMvc.Models
{
    public class EgitimSeviyesiModel
    {
        public EgitimSeviyesiModel()
        {
            EgitimSeviyesiList = new List<EgitimSeviyesi>
            {
                new EgitimSeviyesi(){Id=1,Seviye="Yüksek Lisans"},
                new EgitimSeviyesi(){Id=2,Seviye="Üniversite"},
                new EgitimSeviyesi(){Id=3,Seviye="Lise"},
                new EgitimSeviyesi(){Id=4,Seviye="Ortaokul"},
                new EgitimSeviyesi(){Id=5,Seviye="İlkokul"}
            };
        }

        public List<EgitimSeviyesi> EgitimSeviyesiList{ get; set; }
    }

    public class EgitimSeviyesi
    {
        public int Id { get; set; }
        public string Seviye { get; set; }
    }
}