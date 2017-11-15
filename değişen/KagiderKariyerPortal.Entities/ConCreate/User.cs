using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KagiderKariyerPortal.Entities.ConCreate
{
    public class User
    {
        [Key]
        public int UserId { get; set; }


        public string MemberName { get; set; }

        public string MemberSurName { get; set; }

        public string MemberType { get; set; }

        public int MemberTypeId { get; set; } //0 şifreli iş arayan girişi 1 kagider üye ceptel iş arayan girişi 2 kagider referans üye ceptel girişi
        //3 kagider üye ceptel işveren girişi 4 şifreli iş veren girişi
        public string MobilPhone { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }
        public string ResimYol { get; set; }
    }
}
