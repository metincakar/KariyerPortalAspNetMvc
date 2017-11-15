using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KagiderKariyerPortalMvc.Models
{
    public class PozisyonTipiModel
    {
        public PozisyonTipiModel()
        {
            PozisyonTipiList = new List<PozisyonTipi>()
            {
                new PozisyonTipi(){Id=0,Tip="Sürekli"},
                new PozisyonTipi(){Id=1,Tip="Tam Zamanlı"},
                new PozisyonTipi(){Id=2,Tip="Yarı Zamanlı"},
            };
        }

        public List<PozisyonTipi> PozisyonTipiList { get; set; }
    }

    public class PozisyonTipi
    {
        public int Id { get; set; }
        public string Tip { get; set; }
    }
}