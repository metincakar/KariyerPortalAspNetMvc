using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace KagiderKariyerPortalMvc.Models
{
    public class IlanSearchModel
    {
    
        public string Keyword { get; set; }
        public string FirmaAd { get; set; }
        public int Sehir { get; set; }
        public int Ilce { get; set; }
        public int[] Sektorler { get; set; }
        public int[] ArananPozisyon { get; set; }
        public string[] PSeviye { get; set; }
        public string[] PTipi { get; set; }
        

    }
}