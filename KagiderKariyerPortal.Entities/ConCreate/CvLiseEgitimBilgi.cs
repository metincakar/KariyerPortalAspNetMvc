using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KagiderKariyerPortal.Entities.ConCreate
{
    class CvLiseEgitimBilgi
    {
        [Key]
        public int EgitimLiseId { get; set; }
        public int UserId { get; set; }
        public virtual UserProfile User { get; set; }
        public int LiseId { get; set; }

        public string LiseAd { get; set; }

        public string Bolum { get; set; }

        public string MezuniyetYil { get; set; }

        public string MezuniyetDerece { get; set; }
    }
}
