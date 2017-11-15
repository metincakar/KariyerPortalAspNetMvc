using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;

namespace KagiderKariyerPortal.Entities.ConCreate
{
    public class IlanBasvuru
    {
        [Key]
        public int BasvuruId { get; set; }

        public int UserId { get; set; }

        //[Required]
        //[ForeignKey("UserId")]

        //public virtual User User { get; set; }

        public int IlanId { get; set; }
        public virtual Ilan Ilan { get; set; }

        public DateTime BasvuruTarih { get; set; }

        public string Onyazi { get; set; }

        public bool BasvuruDurum { get; set; }
     }
}
