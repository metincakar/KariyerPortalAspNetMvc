using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KagiderKariyerPortalMvc.Models
{
    public class CvBankModel
    {
        public int UserId { get; set; }
        public string MemberName { get; set; }
        public string MemberSurName { get; set; }
        public string MobilTel { get; set; }
        public string Email { get; set; }
        public string Resim { get; set; }
        public int SehirId { get; set; }
        public int IlceId { get; set; }
    }
}