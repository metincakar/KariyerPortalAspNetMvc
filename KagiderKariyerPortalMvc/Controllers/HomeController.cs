using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using KagiderKariyerPortal.Dal.ConCreate.EntityFramework;
using KagiderKariyerPortal.Entities.ConCreate;
using WebMatrix.WebData;

namespace KagiderKariyerPortalMvc.Controllers
{
    public class HomeController : Controller
    {
        readonly EfIlanlarDal _efIlanlarDal=new EfIlanlarDal();
        readonly int[] _isveren = new int[] { 1, 4 };
        public ActionResult Index()
        {
            var loginUser = Membertype();
            if (loginUser != null && loginUser.MemberTypeId==1)
            {
                ViewBag.logintype = 1;
            }
            else
            {
                ViewBag.logintype = 0;
            }
            ViewBag.Message = "Çok daha iyi bir gelecek için biz varız,ya siz?";
            var ilanlar=_efIlanlarDal.GetList();
            return View(model: ilanlar.AsEnumerable());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

      

        public static UserProfile Membertype()
        {
            if (WebSecurity.IsAuthenticated)
            {
                var userDal = new EfUserDal();
                var loginUser =
                    userDal.Get(new UserProfile() { UserId = WebSecurity.GetUserId(WebSecurity.CurrentUserName) });
                return loginUser ?? null;
            }
            else
            {
                return null;
            }

        }
        
    }


}
