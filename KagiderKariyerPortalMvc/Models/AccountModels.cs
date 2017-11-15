using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Http;
using System.Web.UI.WebControls;
using KagiderKariyerPortal.Dal.ConCreate.EntityFramework;
using KagiderKariyerPortal.Entities.ConCreate;

namespace KagiderKariyerPortalMvc.Models
{
 
    //public class UsersContext : DbContext
    //{
         
    //    public UsersContext()
    //        : base("KagiderContext")
    //    {
    //    }

    //}

    //[Table("UserProfile")]
    //public class UserProfiles
    //{
    //    [Key]
    //    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
    //    public int UserId { get; set; }
    //    public string UserName { get; set; }
    //}

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Varsayilan Şifre")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Bu alan gereklidir")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Bu alan gereklidir")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

       [Required(ErrorMessage = "Bu alan gereklidir")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla?")]
        public bool RememberMe { get; set; }

        [Required(ErrorMessage = "Bu alan gereklidir")]
        public int MemberTypeId { get; set; }
        public string MemberType { get; set; }

        public bool OnayDurumu { get; set; }

        
        //public UserProfile UserKontrol()
        //{
        //    var userDal=new EfUserDal();
            
        //    UserProfile loginUser =
        //        userDal.GetList().FirstOrDefault(u => u.UserName == this.UserName && u.Password == this.Password && u.MemberTypeId==this.MemberTypeId);
        //    if (loginUser != null)
        //    {
        //       /* 
        //        * FormsAuthentication.SetAuthCookie(this.UserName, false);
        //        FormsAuthentication.Authenticate(this.UserName, this.Password);
        //        */

        //        return loginUser;
        //    }
        //    else if (this.UserName == "adminpanel" && Password == "12345abcde" && this.MemberTypeId==99)
        //    {
        //        loginUser = new UserProfile()
        //        {
        //            UserName = "Admin",
        //            MemberTypeId = 99
  
        //        };
        //        return loginUser;
        //    }
        //    else
        //    {
        //        return null;
        //    }
            

        //}
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Bu alan gereklidir")]
        [Display(Name = "Ad")]
        public string AccountName { get; set; }

       [Required(ErrorMessage = "Bu alan gereklidir")]
        [Display(Name = "Soyad")]
        public string AccountSurName { get; set; }
        
        public int MemberTypeId { get; set; }

       [Required(ErrorMessage = "Bu alan gereklidir")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bu alan gereklidir")]
        [StringLength(100, ErrorMessage = "En az {2} karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Tekrar Şifre Giriniz")]
        [Compare("Password", ErrorMessage = "Şifreler Uyuşmuyor tekrar deneyiniz.")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Bu alan gereklidir")]
        [StringLength(10, ErrorMessage = "Telefon numaranızı başında 0 olmadan giriniz")]
        [Display(Name = "Mobil Telefon No")]
        public string MobilPhone { get; set; }

        [Display(Name = "Kagider'e Üyemisiniz?")]
        public bool KagiderUyesi { get; set; }

         [Display(Name = "Kagider Üye Referansınız Var mı?")]
        public bool Referansli { get; set; }

         [Display(Name = "Kagider Üye Referansı Mobil Tel")]
        public string ReferansPhone { get; set; }

        public string Kontrol()
        {
            var userDal = new EfUserDal();
            UserProfile userkontrol=userDal.GetList().FirstOrDefault(b => b.UserName == this.UserName);
            string message = string.Empty;
            if (userkontrol != null)
                message = "Bu kullanıcı sistemde kayıtlı lütfen farklı bir kullanıcı adı deneyin!";
            userkontrol = userDal.GetList().FirstOrDefault(b => b.Email == this.Email);
            if (userkontrol != null)
                message = "Email sistemde kullanılmaktdır lütfen yeni bir hesap açınız!";

            return message;
        }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }

   

   
}
