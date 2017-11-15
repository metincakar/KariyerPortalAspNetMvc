using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KagiderKariyerPortal.Entities.ConCreate
{
    class CvSertifikaBilgi
    {
        public int UyeId { get; set; }

        public string SertifikaAd { get; set; }

        public string SertifikaTarih { get; set; }
        public string AlindigiKurum { get; set; }
        public string Aciklama { get; set; }
    }
}
