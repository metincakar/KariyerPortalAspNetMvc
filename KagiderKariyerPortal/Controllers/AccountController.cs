using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KagiderKariyerPortal.Entities.ConCreate;
using KagiderKariyerPortal.Models;

namespace KagiderKariyerPortal.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult LoginIsArayanDiger()
        {
            return View(new LoginIsArayanDiger());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LoginIsArayanDiger(LoginIsArayanDiger loginUyeBilgi)
        {
            if (ModelState.IsValid)
            {
                if (loginUyeBilgi.LoginKontrol())
                    return RedirectToAction("Index","Home");
                else
                {
                    ModelState.AddModelError("", "Lütfen kullanıcı bilgilerinizi kontrol ediniz.");
                    return View();
                }
            }
            else
            {
               
                return View();
            }
        }


        public ActionResult EkleIsArayan()
        {
            return View(new LoginIsArayanDiger());
        }
    }
}
