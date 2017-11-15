using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KagiderKariyerPortal.Entities.ConCreate
{
    public class Ilanlar
    {
        [Key]
       
            public int IlanId { get; set; }
            public int UserId { get; set; }

            public virtual User IlanUser { get; set; }

            [Display(Name = "İş Tanımı")]
            public string IlanTitle { get; set; }

            [Display(Name = "Genel Nitelikler")]
            public string Detail { get; set; }

            public string AcikPozisyon { get; set; }

            public int CityId { get; set; }

            public virtual SetupCity SetupCity { get; set; }
            public int CoutyId { get; set; }

            public virtual SetupCounty SetupCounty { get; set; }

            public int CoutryId { get; set; }

            public virtual SetupCountry SetupCountry { get; set; }

            public DateTime IlanStartDate { get; set; }

            public DateTime IlanFinishDate { get; set; }

            public int ArananPersonelSayisi { get; set; }

            public string PozisyonTipi { get; set; }

            public string PozisyonSeviyesi { get; set; }

            public string Tecrube { get; set; }

            public string EgitimSeviyesi { get; set; }
            public List<User> Users { get; set; }

        
    }
}
