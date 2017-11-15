using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KagiderKariyerPortal.Entities.ConCreate
{
    class CvSeminerler
    {
        public int UyeId { get; set; }

        public string EgitimAd { get; set; }
        public string EgitimKurumu { get; set; }
        public DateTime EgitimBaslangic { get; set; }
        public DateTime EgitimBitis { get; set; }

        public int EgitimSure { get; set; }

        public string Aciklama { get; set; }
    }
}
