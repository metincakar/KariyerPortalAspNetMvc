using System;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using KagiderKariyerPortal.Dal.ConCreate.EntityFramework;
using KagiderKariyerPortal.Entities.ConCreate;
using WebMatrix.WebData;

namespace KagiderKariyerPortalMvc.Controllers
{
    public class EmployeeController : Controller
    {
        readonly EfIlanBasvuruDal _efIlanBasvuruDal = new EfIlanBasvuruDal();
        readonly EfUserDal _efUserDal = new EfUserDal();
        readonly EfIlanlarDal _efIlanDal = new EfIlanlarDal();
        readonly EfCvIletisimBilgiDal _efCvIletisimBilgiDal = new EfCvIletisimBilgiDal();
        readonly KagiderContext _dbcontext = new KagiderContext();
        readonly int[] _isveren = new int[] { 1, 4 };
        readonly int[] _isarayan = new int[] { 0, 2, 3 };
        //[Authorize]
        //public ActionResult Index()
        //{
        //    //Session["uid"] = 2;
        //    //Session["typeid"] = 2;
        //    //Session["uname"] = "test is arayan";
        //    var isveren = new int[] { 1, 4 };
        //    if (Session["typeid"] != null && isveren.Contains((int)Session["typeid"]))
        //        Session.Clear();
        //    ViewBag.Message = "Sisteme giriş yaparak kayıtlı iş fırsatlarını görebilirsiniz.";

        //    return View();
        //}
        [Authorize]
        public ActionResult AddApp(int id)
        {
            var loginUser = Membertype();
            if (loginUser != null && loginUser.MemberTypeId == 0) //isarayan ise
            {
                if (_efCvIletisimBilgiDal.GetList().Where(cv => cv.UserId == loginUser.UserId).ToList().Count != 0)
                {
                    var user = _efUserDal.Get(new UserProfile() { UserId = loginUser.UserId });
                    var ilan = _efIlanDal.Get(new Ilan() { IlanId = id });
                    var basvuruYapilmismi =
                        _efIlanBasvuruDal.GetList().FirstOrDefault(b => b.IlanId == id && b.UserId == user.UserId);

                    ViewBag.BasvuruVarmi = basvuruYapilmismi !=null;

                    return View(new IlanBasvuru() { Ilan = ilan, IlanId = id, UserId = user.UserId, BasvuruDurum = true });

                }
                else
                {
                    return RedirectToAction("Index", "Ilan", routeValues: new {uyari = 0});
                }
            }
            else
            {
                WebSecurity.Logout();
                return RedirectToAction("Login", "Account", new { id = 0 });//is arayan login seçenekleri
            }

        }

        [HttpPost]
        [Authorize]
        public ActionResult AddApp(IlanBasvuru addBasvuru)
        {
            var loginUser = Membertype();
            if (loginUser != null && loginUser.MemberTypeId == 0) //isarayan ise
            {
                addBasvuru.BasvuruTarih=DateTime.Now;
                _efIlanBasvuruDal.Add(addBasvuru);

               return RedirectToAction("MyAppList", "Employee", new { userId = addBasvuru.UserId });
            }
            else
            {
                WebSecurity.Logout();
                return RedirectToAction("Login", "Account",new {id=0});//is arayan login seçenekleri
            }
        }
        public ActionResult EditApp(int id)
        {
            var editBasvuru = _efIlanBasvuruDal.Get(new IlanBasvuru() {BasvuruId = id});

            return View(editBasvuru);
        }
        [HttpPost]
        [Authorize]
        public ActionResult EditApp(IlanBasvuru updBasvuru)
        {
            _efIlanBasvuruDal.Update(updBasvuru);
            return RedirectToAction("MyAppList");
        }
        [Authorize]
        public ActionResult MyAppList()
        {

            var loginUser = Membertype();
            if (loginUser != null && loginUser.MemberTypeId == 0) //isarayan ise
            {
                var userid = loginUser.UserId;
                 var myAppList = _efIlanBasvuruDal.GetList().Where(b => b.UserId == userid && b.BasvuruDurum == true).OrderByDescending(b => b.BasvuruTarih);
                return View(myAppList);
                }
             else
             {
                 WebSecurity.Logout();
                 return RedirectToAction("Login", "Account", new { id = 0 });//is arayan login seçenekleri
             }
        }

        [Authorize]
        public ActionResult SearchApp(string keyword,string firmaAdi)
        {
            var loginUser = Membertype();
            if (loginUser != null && loginUser.MemberTypeId == 0) //isarayan ise
            {
                var userid = loginUser.UserId;
                var myAppList =
                    _efIlanBasvuruDal.GetList()
                        .Where(
                            b =>
                                b.UserId == userid && b.BasvuruDurum == true &&
                                (b.Ilan.IlanTitle.Contains(keyword) || b.Ilan.AcikPozisyon.Contains(keyword)) &&
                                b.Ilan.Firma.FirmaAd.StartsWith(firmaAdi))
                        .OrderByDescending(b => b.BasvuruTarih);
                return View("MyAppList", myAppList);
            }
            else
            {
                WebSecurity.Logout();
                return RedirectToAction("Login", "Account", new {id = 0}); //is arayan login seçenekleri
            }
        }
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
        public static UserProfile Membertype()
        {
            if (WebSecurity.IsAuthenticated)
            {
                var userDal = new EfUserDal();
                var loginUser =
                    userDal.Get(new UserProfile() {UserId = WebSecurity.GetUserId(WebSecurity.CurrentUserName)});
                return loginUser ?? null;
            }
            else
            {
                return null;
            }
           
        }
    }
}