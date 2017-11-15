using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KagiderKariyerPortal.Entities.ConCreate;

namespace KagiderKariyerPortalMvc.Models
{
    public class IlanBasvuruModel
    {
        public int BasvuruId { get; set; }
        public DateTime BasvuruTarihi { get; set; }
        public DateTime IlanTarihi { get; set; }
        public DateTime IlanBitisTarihi { get; set; }
        public string BasvuruYapanAd { get; set; }
        public string BasvuruYapanSoyad { get; set; }
        public int BasvuruYapanId { get; set; }
        public int IlanId { get; set; }
        public string IlanTitle { get; set; }
        public string AcikPozisyon { get; set; }
        public Firma IlanFirma { get; set; }
        public int IlanFirmaId { get; set; }
    }
}