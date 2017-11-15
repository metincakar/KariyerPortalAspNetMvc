using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace KagiderKariyerPortal.Entities.ConCreate
{
 public class UyeBilgi
    {
        [Key]
     public int UyeBilgiId { get; set; }

        public int UyelikTipi { get; set; } //kagider uyesi veya duyupta gelen

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string UyeAdi { get; set; }


        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string UyeSoyadi { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UyeMail { get; set; }

     [Required]
     [DataType(DataType.PhoneNumber)]
        public string UyeMobilTel { get; set; }


      [DataType(DataType.Password)]
        public string UyeSifre { get; set; }

        public DateTime UyelikTarihi { get; set; }
    }
}
