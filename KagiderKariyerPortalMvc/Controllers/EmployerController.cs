using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using KagiderKariyerPortal.Dal.ConCreate.EntityFramework;
using KagiderKariyerPortal.Entities.ConCreate;
using KagiderKariyerPortalMvc.Models;
using System.Collections.Generic;
using Microsoft.Ajax.Utilities;
using WebMatrix.WebData;

namespace KagiderKariyerPortalMvc.Controllers
{

    public class EmployerController : Controller
    {
        readonly int[] _isveren = new int[] { 1, 4 };

        readonly EfIlanBasvuruDal _efIlanBasvuruDal = new EfIlanBasvuruDal();

        readonly EfUserDal _efUserDal = new EfUserDal();

        readonly EfCvIletisimBilgiDal _efCvIletisimBilgiDal = new EfCvIletisimBilgiDal();
        readonly EfCvKisiselBilgiDal _efCvKisiselBilgiDal = new EfCvKisiselBilgiDal();
        readonly  EfCvIsDeneyimDal _efCvIsDeneyimDal=new EfCvIsDeneyimDal();
        readonly EfCvEgitimDal _efCvEgitimDal = new EfCvEgitimDal();
        readonly EfCvDilBilgiDal _efCvDilBilgiDal = new EfCvDilBilgiDal();

        readonly EfSetupCityDal _efSetupCityDal = new EfSetupCityDal();
        readonly EfSetupCountyDal _efSetupCountyDal = new EfSetupCountyDal();
        readonly EfSetupCountryDal _efSetupCountryDal = new EfSetupCountryDal();
        readonly EfCvOther _efCvOther=new EfCvOther();
        private readonly CvItemModel _itemModel = new CvItemModel();
        public ActionResult Index()
        {
            //Session["uid"] = 4;
            //Session["typeid"] = 4;
            ViewBag.Message = "Sisteme giriş yaparak iş ilanı bırakabilirsiniz.";
            //Session["uname"] = "test işveren";
            return View();
        }
        //[Authorize]
        public ActionResult CvBank()
        {

            var loginUser = Membertype();
            if (loginUser != null && loginUser.MemberTypeId == 1) //isveren ise
            {
                ViewBag.Sehirler = _efSetupCityDal.GetList();
                ViewBag.Ilceler = _efSetupCountyDal.GetList();
                ViewBag.Ulkeler = _efSetupCountryDal.GetList();

                ViewBag.ListDepartman = _itemModel.ListDepartmanlar;
                ViewBag.Pozisyonlar = _itemModel.ListPozisyonlar;
                ViewBag.Sektorlist = _itemModel.ListSektorler;
                ViewBag.pozisyonSeviyeList = _itemModel.ListPozisyonseviye;
                ViewBag.pozisyonTipList = _itemModel.ListPozisyontip;
                ViewBag.selectedFirmaad = "";
                ViewBag.selectedSektorlist = null;
                ViewBag.selectedPozisyonlist = null;
                ViewBag.selectedPseviye = null;
                ViewBag.selectedPtip = null;

                var cvlistmodel = (from usr in _efUserDal.GetList()
                    join cv in _efCvIletisimBilgiDal.GetList() on usr.UserId equals cv.UserId
                    select new CvBankModel
                    {
                        UserId=usr.UserId,
                        MemberName=usr.MemberName,
                        MemberSurName=usr.MemberSurName,
                        MobilTel=cv.MobilTel,
                        Email=cv.UyeMail,
                        Resim=usr.ResimYol
                    }).AsEnumerable();
                return View(cvlistmodel);
            }
            else
            {
                WebSecurity.Logout();
                return RedirectToAction("Login", "Account", new { id = 1 });//is veren login seçenekleri
            }
        }
        //[Authorize]
        public ActionResult AppIlan() //ilana yapılan basvurular
        {
            var loginUser = Membertype();
            if (loginUser != null && loginUser.MemberTypeId==1) //isveren ise
            {
                var userid =loginUser.UserId;
                var ilanbasvurularmodel =
                    (from bsv in
                        _efIlanBasvuruDal.GetList()
                            .Where(b => b.Ilan.UserId == userid)
                            .OrderByDescending(b => b.BasvuruTarih)
                    join usr in _efUserDal.GetList() on bsv.UserId equals usr.UserId
                    orderby bsv.BasvuruTarih descending 
                    select new IlanBasvuruModel
                     {
                        BasvuruId = bsv.BasvuruId,
                        BasvuruTarihi =bsv.BasvuruTarih,
                        IlanTarihi = bsv.Ilan.IlanStartDate,
                        IlanBitisTarihi = bsv.Ilan.IlanFinishDate,
                        BasvuruYapanAd=usr.MemberName,
                        BasvuruYapanSoyad=usr.MemberSurName,
                        BasvuruYapanId=usr.UserId,
                        IlanId=bsv.IlanId,
                        IlanTitle=bsv.Ilan.IlanTitle,
                        AcikPozisyon=bsv.Ilan.AcikPozisyon
                      }).AsEnumerable();

                //var ilanbasvurularmodel = _efIlanBasvuruDal.GetList().Where(b => b.Ilan.UserId == userid).OrderByDescending(b => b.BasvuruTarih);
                
                return View(ilanbasvurularmodel);
            }
            else
            {
               WebSecurity.Logout();
                return RedirectToAction("Login", "Account", new { id = 1 });//is veren login seçenekleri
            }
        
        }
          [Authorize]
        public ActionResult SearchCv(IlanSearchModel searchModel,int page = 1, int id = 0)
        {
            var loginUser = Membertype();
            if (loginUser != null && loginUser.MemberTypeId == 1) //isveren ise
            {
                ViewBag.Sehirler = _efSetupCityDal.GetList();
                ViewBag.Ilceler = _efSetupCountyDal.GetList();
                ViewBag.Ulkeler = _efSetupCountryDal.GetList();

                ViewBag.ListDepartman = _itemModel.ListDepartmanlar;
                ViewBag.Pozisyonlar = _itemModel.ListPozisyonlar;
                ViewBag.Sektorlist = _itemModel.ListSektorler;
                ViewBag.pozisyonSeviyeList = _itemModel.ListPozisyonseviye;
                ViewBag.pozisyonTipList = _itemModel.ListPozisyontip;
                ViewBag.selectedFirmaad = searchModel.FirmaAd;
                ViewBag.selectedSektorlist = searchModel.Sektorler;
                ViewBag.selectedPozisyonlist = searchModel.ArananPozisyon;
                ViewBag.selectedPseviye = searchModel.PSeviye;
                ViewBag.selectedPtip = searchModel.PTipi;

                var cvlistmodel = (from usr in _efUserDal.GetList()
                                   join cv in _efCvIletisimBilgiDal.GetList() on usr.UserId equals cv.UserId
                                   join kiss in _efCvKisiselBilgiDal.GetList() on cv.UserId equals  kiss.UserId into al
                                   from ljoinkiss in al.DefaultIfEmpty()
                                   join isb in _efCvIsDeneyimDal.GetList() on usr.UserId equals isb.UserId into bl
                                   from ljoinIsb in bl.DefaultIfEmpty()
                                   join egt in _efCvEgitimDal.GetList() on usr.UserId equals egt.UserId into cl
                                   from ljoinEgt in cl.DefaultIfEmpty()
                                   join dil in _efCvDilBilgiDal.GetList() on usr.UserId equals dil.UserId into dl
                                   from ljoinDil in dl.DefaultIfEmpty()
                                   join oth in _efCvOther.GetList() on usr.UserId equals oth.UserId into ot
                                   from ljoinOth in ot.DefaultIfEmpty()
                
                                   select new
                                   {
                                     usr,
                                     cv,
                                     ljoinkiss,
                                     ljoinIsb,
                                     ljoinEgt,
                                     ljoinDil,
                                     ljoinOth
                                   }).AsEnumerable();
                if (searchModel.Keyword!=null)
                {
                    cvlistmodel =
                        cvlistmodel.Where(i => i.usr.MemberName.Contains(searchModel.Keyword) || i.usr.MemberSurName.Contains(searchModel.Keyword));

                        
                }
                if (searchModel.Sehir > 0)
                    cvlistmodel = cvlistmodel.Where(i => i.cv.CityId == searchModel.Sehir);
                if (searchModel.Ilce > 0)
                    cvlistmodel = cvlistmodel.Where(i => i.cv.CountyId == searchModel.Ilce);
                if (searchModel.ArananPozisyon != null)
                {
                   var listCvOther = new List<CvOther>();
                   foreach (var item in searchModel.ArananPozisyon)
                    {
                        string aitem = string.Format(",{0},", item);
                        var a = cvlistmodel.Where(p => p.ljoinOth.IstenilenPozisyon != null && p.ljoinOth.IstenilenPozisyon.Contains(aitem));
                        foreach (var iln in a)
                        {
                            listCvOther.Add(iln.ljoinOth);
                        }
                    }
                    cvlistmodel = (from cv in cvlistmodel
                        join cvOther in listCvOther on cv.ljoinOth.OtherId equals cvOther.OtherId
                        select cv
                        );


                    // ilanList = listilan.DistinctBy(p => p.IlanId);
                }
                //if (searchIlan.Sektorler != null)
                //{
                //    var listilan = new List<Ilan>();
                //    foreach (var item in searchIlan.Sektorler)
                //    {
                //        string aitem = string.Format(",{0},", item);

                //        var a = ilanList.Where(p => p.Firma.FirmaCalismaAlanlari != null && p.Firma.FirmaCalismaAlanlari.Contains(aitem)).ToList();
                //        foreach (var iln in a)
                //        {
                //            listilan.Add(iln);
                //        }
                //    }
                //    ilanList = listilan.DistinctBy(p => p.IlanId);
                //}
                if (searchModel.PSeviye != null)
                {
                   // cvlistmodel =cvlistmodel.Where(p=>searchModel.PSeviye.Contains(p.cv.))
                   // ilanList = ilanList.Where(p => searchIlan.PSeviye.Contains(p.PozisyonSeviyesi.ToString()));

                }
                //if (searchIlan.PTipi != null)
                //{
                //    ilanList = ilanList.Where(p => searchIlan.PTipi.Contains(p.PozisyonTipi.ToString()));

                //}
                var qselect = (from cv in cvlistmodel
                    select
                        new CvBankModel()
                        {
                            MemberName = cv.usr.MemberName,
                            MemberSurName = cv.usr.MemberSurName,
                            Email = cv.cv.UyeMail,
                            IlceId = cv.cv.CityId,
                            MobilTel = cv.cv.MobilTel,
                            Resim = cv.usr.ResimYol,
                            SehirId = cv.cv.CityId,
                            UserId = cv.cv.UserId
                        }).DistinctBy(c => c.UserId); ;
                return View("CvBank",qselect);
            }
            else
            {
                WebSecurity.Logout();
                return RedirectToAction("Login", "Account", new { id = 1 });//is veren login seçenekleri
            }
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