using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KagiderKariyerPortal.Entities.ConCreate;

namespace KagiderKariyerPortalMvc.Models
{
    public class CvDetail
    {
        public CvDetail()
        {
            this.IletisimBilgisi=new CvIletisimBilgi();
            this.KisiselBilgisi=new CvKisiselBilgi();
            this.Universiteler=new List<CvEgitimUniversiteBilgi>();
            this.IsDeneyimleri=new List<CvIsDeneyimleri>();
        }

        public UserProfile User { get; set; }
        public CvIletisimBilgi IletisimBilgisi { get; set; }

        public CvKisiselBilgi KisiselBilgisi { get; set; }

        public List<CvIsDeneyimleri> IsDeneyimleri { get; set; }

        public List<CvEgitimUniversiteBilgi> Universiteler { get; set; }

        public List<CvDilBilgi> Diller { get; set; }
        public CvOther Other { get; set; }
        public List<CvSertifikaBilgi> SertifikaBilgileri { get; set; }
        public List<CvSeminerler> SeminerBilgileri { get; set; }
        public List<CvSinavBilgi> SinavBilgileri { get; set; }
    }

   
}