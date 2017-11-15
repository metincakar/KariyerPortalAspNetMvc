using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using KagiderKariyerPortal.Dal.ConCreate.EntityFramework;
using KagiderKariyerPortal.Entities.ConCreate;
using KagiderKariyerPortalMvc.Models;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;

namespace KagiderKariyerPortalMvc.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        readonly int[] _isveren = new int[] { 1, 4 };
        readonly int[] _isarayan = new int[] { 0, 2, 3 };

        [AllowAnonymous]
        public ActionResult Login(int id=0, string membertype = null, int viewtype = 0, string returnUrl = "/Home/Index")
        {

            if(WebSecurity.IsAuthenticated)WebSecurity.Logout();
           
            switch (id)
            {
                case 0:
                    membertype = MemberTypeModel.IsarayanSifreli.ToString();
                    ViewBag.Message = "İnsan kaynakları platformuna kayıtlı iş arayan üyeler kullanıcı adı ve şifreleriyle login olabilirler.";
                    break;
                case 1:
                    membertype = MemberTypeModel.IsverenSifreli.ToString();
                    ViewBag.Message = "İnsan kaynakları platformuna kayıtlı işveren üyeler kullanıcı adı ve şifreleriyle login olabilirler.";
                    break;
                case 2:
                    membertype = MemberTypeModel.IsarayanGeleceginKadinLideri.ToString();
                    ViewBag.Message = "Geleğin Kadın Lideri üyeleri mobil telefon numaraları ile sisteme kayıt olmaksızın giriş yapabilirler.";
                    break;
                case 3:
                    membertype = MemberTypeModel.IsarayanKagiderUyeReferansli.ToString();
                    break;
                case 4:
                    membertype = MemberTypeModel.IsverenKagiderUye.ToString();
                    ViewBag.Message = "Kagider üyeleri mobil telefon numaraları ile sisteme kayıt olmaksızın giriş yapabilirler.";
                    break;
            }

            ViewBag.returnUrl = returnUrl;
            ViewBag.Viewtype = viewtype;
            if(viewtype==1)
            return PartialView(new LoginModel(){MemberTypeId=id,MemberType=membertype});
            else
            {
                return View(new LoginModel() { MemberTypeId = id, MemberType = membertype });
            }
        }
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl, int viewtype = 0)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName.Replace('i', 'İ'), model.Password, persistCookie: model.RememberMe))
            {
                var loginUser = Membertype(model.UserName.Replace('i', 'İ'));
                if (model.MemberTypeId != loginUser.MemberTypeId)
                {
                    WebSecurity.Logout();
                    ModelState.AddModelError("", "Hatalı kullanıcı adı veya şifre.");
                    if (viewtype == 1)
                        return PartialView(model);
                    else
                    {
                        return View(model);
                    }
                }
                if (model.MemberTypeId == 1 && loginUser.OnayDurumu == false)
                {
                    WebSecurity.Logout();
                    ModelState.AddModelError("", "Üyelik talebiniz henüz ik. yönetimi tarafından onaylanmamıştır..");
                    if (viewtype == 1)
                        return PartialView(model);
                    else
                    {
                        return View(model);
                    }
                }
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Hatalı kullanıcı adı veya şifre.");
            if (viewtype == 1)
                return PartialView(model);
            else
            {
                return View(model);
            }
          
        }
        //public ActionResult Login(LoginModel model, string returnUrl, int viewtype = 0)
        //{
        //    ViewBag.Viewtype = viewtype;
        //    ViewBag.returnUrl = returnUrl;


        //    //if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
        //    //{
        //    User loginUser = model.UserKontrol();
        //    if (ModelState.IsValid && loginUser!=null)
        //    {
        //        if (loginUser.MemberTypeId != 99 && loginUser.OnayDurumu==true)
        //        {
        //            Session["uid"] = loginUser.UserId;
        //            Session["uname"] = loginUser.UserName;
        //            Session["typeid"] = loginUser.MemberTypeId;
                  
        //        }
        //        else if(loginUser.MemberTypeId==0 && loginUser.OnayDurumu==false)
        //        {
        //            ViewBag.Message = "Üyelik talebiniz henüz ik. yönetimi tarafından onaylanmamıştır.";
        //            return PartialView("ExternalLoginFailure");
        //        }
        //        else if(loginUser.MemberTypeId==99)
        //        {
        //            Session["adminpanel"] = "adminpanel";
        //            return RedirectToLocal(returnUrl);
        //        }
        //        //return RedirectToLocal(returnUrl);
        //        return RedirectToAction("Index","Home");

               
        //    }

        //    // If we got this far, something failed, redisplay form
        //    ModelState.AddModelError("", "Hatalı kullanıcı adı veya şifre.");
        //    if (viewtype == 1)
        //        return PartialView(model);
        //    else
        //    {
        //        return View(model);
        //    }
        //    //return View(model);
        //}
       
    
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public JsonResult LoginAjax(LoginModel model, string returnUrl)
        {


            if (ModelState.IsValid && WebSecurity.Login(model.UserName.Replace('i', 'İ'), model.Password, persistCookie: model.RememberMe))
            {
                var loginUser = Membertype(model.UserName.Replace('i', 'İ'));
                if (model.MemberTypeId != loginUser.MemberTypeId)
                {
                    WebSecurity.Logout();
                    ModelState.AddModelError("", "Hatalı kullanıcı adı veya şifre.");
                    return Json(JsonResponseFactory.ErrorResponse("Hatalı kullanıcı adı veya şifre."), JsonRequestBehavior.DenyGet);
                  
                }
                if (loginUser.MemberTypeId == 1 && loginUser.OnayDurumu == false)
                {
                    WebSecurity.Logout();
                  
                    ModelState.AddModelError("", "Üyelik talebiniz henüz ik. yönetimi tarafından onaylanmamıştır.");
                    return Json(JsonResponseFactory.ErrorResponse("Üyelik talebiniz henüz ik. yönetimi tarafından onaylanmamıştır."), JsonRequestBehavior.DenyGet);

                }
               
                return Json(JsonResponseFactory.SuccessResponse(model, returnUrl.ToString()), JsonRequestBehavior.DenyGet);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Hatalı kullanıcı adı veya şifre.");
            return Json(JsonResponseFactory.ErrorResponse("Hatalı kullanıcı adı veya şifre."), JsonRequestBehavior.DenyGet);
           
         
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            if(WebSecurity.IsAuthenticated)
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
      
        public ActionResult Register(int id=0)
        {

            return View(new RegisterModel(){MemberTypeId = id});
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
              
                   
                    try
                    {
                       // var userDal = new EfUserDal();

                        var addUser = new 
                        {
                            UserName = model.UserName,
                            //Password = model.Password,
                            MemberName = model.AccountName,
                            MemberSurName = model.AccountSurName,
                            Email = model.Email,
                            MobilPhone = model.MobilPhone,
                            MemberTypeId = model.MemberTypeId,
                            ReferansPhone = model.ReferansPhone,
                            Referansli = model.Referansli,
                            KagiderUyesi = model.KagiderUyesi,
                            OnayDurumu = model.MemberTypeId == 0,
                            KayitTarihi = DateTime.Now.Date
                        };
                       // userDal.Add(addUser);


                    WebSecurity.CreateUserAndAccount(model.UserName.Replace('i','İ'), model.Password,addUser);
                    var succes = WebSecurity.Login(model.UserName.Replace('i', 'İ'), model.Password);

                    if (succes)
                    {
                        var cvOther = new EfCvOther();
                        cvOther.Add(new CvOther() {UserId = WebSecurity.GetUserId(model.UserName)});
                        /*cvIletisimBilgi.UyeMail = model.Email;
                            cvIletisimBilgi.UyeAd = model.UserName;
                            cvIletisimBilgi.UserId = addUser.UserId;
                            cvIletisimBilgiDal.Add(cvIletisimBilgi);*/
                        if (model.MemberTypeId == 0)
                        {
                            //Session["uid"] = addUser.UserId;
                            //Session["uname"] = model.UserName;
                            //Session["typeid"] = addUser.MemberTypeId;
                        }
                        else
                        {
                            WebSecurity.Logout();
                            return RedirectToAction("Error",
                                routeValues:
                                    new
                                    {
                                        error =
                                            "Kaydınız Gerçekleşti ve Onaya sunuldu,onaylandıktan sonra ilan bırakabilirsiniz."
                                    });
                        }
                    }

                    return RedirectToAction("Index", "Home");

                    
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SifreDegistirmeBasarili });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Şifreniz hatalı veya yeni şifreniz geçersiz.");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", String.Format("Unable to create local account. An account with the name \"{0}\" may already exist.", User.Identity.Name));
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult Manage(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Attempt to register the user
        //        try
        //        {
        //            /* WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
        //             WebSecurity.Login(model.UserName, model.Password);*/

        //            var userDal = new EfUserDal();
        //            string message = model.Kontrol();
        //            if (message.Length == 0)
        //            {
        //                var addUser = new UserProfile();
        //                addUser.UserName = model.UserName;
        //                addUser.Password = model.Password;
        //                addUser.MemberName = model.AccountName;
        //                addUser.MemberSurName = model.AccountSurName;
        //                addUser.Email = model.Email;
        //                addUser.MobilPhone = model.MobilPhone;
        //                addUser.MemberTypeId = model.MemberTypeId; //0 ise isarayan sifreli login 1 ise isveren sifreli login
        //                addUser.ReferansPhone = model.ReferansPhone;
        //                addUser.Referansli = model.Referansli;
        //                addUser.KagiderUyesi = model.KagiderUyesi;

        //                //addUser.OnayDurumu = model.MemberTypeId == 0; //isveren ilk kayitta onaylanmiyor
                        

        //                userDal.Update(addUser);

        //                /*cvIletisimBilgi.UyeMail = model.Email;
        //                    cvIletisimBilgi.UyeAd = model.UserName;
        //                    cvIletisimBilgi.UserId = addUser.UserId;
        //                    cvIletisimBilgiDal.Add(cvIletisimBilgi);*/
        //                if (model.MemberTypeId == 0)
        //                {
        //                    Session["uid"] = addUser.UserId;
        //                    Session["uname"] = model.UserName;
        //                    Session["typeid"] = addUser.MemberTypeId;
        //                }
                        
        //                // FormsAuthentication.SetAuthCookie(model.UserName,false);

        //                return RedirectToAction("Index", "Home");

        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", message);
        //                return View(model);
        //            }
        //        }
        //        catch (MembershipCreateUserException e)
        //        {
        //            ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }




        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.SifreDegistirmeBasarili ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }
         //public ActionResult Manage()
         //{
         //    if (WebSecurity.IsAuthenticated)
         //    {
         //        var user =Membertype(WebSecurity.CurrentUserName);
         //        var userDal = new EfUserDal();
         //       // var user = userDal.GetList().FirstOrDefault(u => u.UserId == loginUser.UserId);
         //        var reg=new RegisterModel();
         //        if (user != null)
         //        {
         //            reg.KagiderUyesi = user.KagiderUyesi?? false;
         //            reg.MemberTypeId = user.MemberTypeId?? 0;
         //            reg.MobilPhone = user.MobilPhone;
         //            reg.Password = user.Password;
         //            reg.ReferansPhone = user.ReferansPhone;
         //            reg.Referansli = user.Referansli??false;
         //            reg.UserName = user.UserName;
         //            reg.Email = user.Email;
         //            reg.AccountName = user.MemberName;
         //            reg.AccountSurName = user.MemberSurName;
         //        }

         //        return View(reg);
         //    }
         //    else
         //    {
         //        return RedirectToAction("Login");

         //    }

             
         //}
         public ActionResult Error(string error)
         {
             ViewBag.Message = error;
             return PartialView("ExternalLoginFailure");
         }

         public static UserProfile Membertype(string uname)
         {
            
                 var userDal = new EfUserDal();
                 var loginUser =
                     userDal.Get(new UserProfile() { UserId = WebSecurity.GetUserId(uname) });

             
                 return loginUser ?? null;
           

         }
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        public enum ManageMessageId
        {
            SifreDegistirmeBasarili,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }
       
    }
}
