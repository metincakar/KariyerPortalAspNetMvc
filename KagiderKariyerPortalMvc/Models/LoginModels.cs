using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.Security;

namespace KagiderKariyerPortal.Models
{
    public class LoginIsArayanDiger
    {

        [Required]
        [Display(Name = "Email Adresi")]
        [DataType(DataType.EmailAddress)]
        public string UyeMail { get; set; }

        [Required]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string UyeSifre { get; set; }
        

        public bool LoginKontrol()
        {
            if (this.UyeMail == "text34@gmail.com" && this.UyeSifre == "1")
            {
                FormsAuthentication.SetAuthCookie("Metin Çakar", false);
                FormsAuthentication.Authenticate(this.UyeMail, this.UyeSifre);

               
                
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class LoginIsArayanReferans
    {
        [Required]
        [Display(Name = "Referans Cep Tel")]
        [DataType(DataType.PhoneNumber)]
        public string ReferansCepTel { get; set; }

    }

    public class LoginIsArayanKagiderUye
    {
        [Required]
        [Display(Name = "Cep Tel")]
        [DataType(DataType.PhoneNumber)]
        public string UyeCepTel { get; set; }

        public string UyeAd { get; set; }

        public string UyeSoyAd { get; set; }
    }
}