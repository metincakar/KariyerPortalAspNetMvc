using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Razor.Text;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using KagiderKariyerPortal.Dal.ConCreate.EntityFramework;
using KagiderKariyerPortal.Entities.ConCreate;
using KagiderKariyerPortalMvc.Models;
using Microsoft.Ajax.Utilities;
using WebMatrix.WebData;

namespace KagiderKariyerPortalMvc.Controllers
{
    public class CvController : Controller
    {
        private readonly EfUserDal _efUserDal = new EfUserDal();
        private readonly EfCvIletisimBilgiDal _efCvIletisimBilgiDal = new EfCvIletisimBilgiDal();
        private readonly EfCvKisiselBilgiDal _efCvKisiselBilgiDal = new EfCvKisiselBilgiDal();
        private readonly EfCvIsDeneyimDal _efCvIsDeneyimDal = new EfCvIsDeneyimDal();
        private readonly EfCvEgitimDal _efCvEgitimDal = new EfCvEgitimDal();
        private readonly EfCvDilBilgiDal _efCvDilBilgiDal = new EfCvDilBilgiDal();
        private readonly EfSetupCityDal _efSetupCityDal = new EfSetupCityDal();
        private readonly EfSetupCountyDal _efSetupCountyDal = new EfSetupCountyDal();
        private readonly EfSetupCountryDal _efSetupCountryDal = new EfSetupCountryDal();
        private readonly EfCvOther _efCvOther = new EfCvOther();
        private readonly EfCvSertifikaDal _efCvSertifikaDal = new EfCvSertifikaDal();
        private readonly EfSeminerDal _efSeminerDal = new EfSeminerDal();
        private readonly EfCvSinavDal _efCvSinavDal = new EfCvSinavDal();
        private readonly CvDetail _cvDetail = new CvDetail();
        private readonly CvItemModel _itemModel = new CvItemModel();
        KagiderContext _context=new KagiderContext();
        public static int userid;
        public ActionResult Index(int id = 0)
        {
            //Session["uid"] = 1;
            //Session["uname"] = "metin";
            //Session["typeid"] = 0;
            userid = id;
            var loginUser = Membertype();
            if (userid == 0) //0 ise isarayan girişinden gelmiştir.değil ise işverende cv görüntüleniyordur.
            {
                if (loginUser == null)
                {
                    //FormsAuthentication.SignOut();
                    return RedirectToAction("Login", "Account",
                        routeValues: new {id = 0});
                }
                userid = loginUser.UserId;
            }
            _cvDetail.IletisimBilgisi = _efCvIletisimBilgiDal.GetList().FirstOrDefault(info => info.UserId == userid);

            _cvDetail.KisiselBilgisi = _efCvKisiselBilgiDal.GetList().FirstOrDefault(k => k.UserId == userid);
            _cvDetail.IsDeneyimleri =
                (from isd in _efCvIsDeneyimDal.GetList()
                    .Where(i => i.UserId == userid)
                    .OrderByDescending(i => i.BaslangicTarihi)
                    .ToList()
                    join scat in _itemModel.ListSektorler on isd.FirmaSektoru equals scat.Value
                    join poz in _itemModel.ListPozisyonlar on isd.FirmadakiPozisyon equals poz.Value
                    select new CvIsDeneyimleri()
                    {
                       FirmaAdi = isd.FirmaAdi,
                       FirmaSektoru = scat.Text,
                       FirmadakiPozisyon = poz.Text,
                       IsAlani = isd.IsAlani,
                       Istanimi = isd.Istanimi,
                       BaslangicTarihi = isd.BaslangicTarihi,
                       BitisTarihi = isd.BitisTarihi,
                       UserId = isd.UserId,
                       User = isd.User,
                       IsDeneyimleriId = isd.IsDeneyimleriId
                    }
                    ).ToList();
            _cvDetail.Universiteler =
                _efCvEgitimDal.GetList().Where(e => e.UserId == userid).OrderBy(e => e.EgitimTipi).ToList();
            _cvDetail.Diller = _efCvDilBilgiDal.GetList().Where(d => d.UserId == userid).ToList();
            _cvDetail.Other = _efCvOther.GetList().FirstOrDefault(kk => kk.UserId == userid);
            _cvDetail.User = _efUserDal.Get(new UserProfile() {UserId = userid});
            _cvDetail.SertifikaBilgileri = _efCvSertifikaDal.GetList().Where(u => u.UserId == userid).ToList();
            _cvDetail.SeminerBilgileri = _efSeminerDal.GetList().Where(u => u.UserId == userid).ToList();
            _cvDetail.SinavBilgileri = _efCvSinavDal.GetList().Where(u => u.UserId == userid).ToList();
            ViewBag.selectedPozisyonlar = _cvDetail.Other.IstenilenPozisyon!=null ? _cvDetail.Other.IstenilenPozisyon.Split(',') : null;
            ViewBag.selectedDepartmanlar = _cvDetail.Other.CalismakIstedigiAlan!=null ? _cvDetail.Other.CalismakIstedigiAlan.Split(',') : null;
            ViewBag.calismatercihi = _cvDetail.Other.CalismaDurumu;
            ViewBag.ListDepartman = _itemModel.ListDepartmanlar;
            ViewBag.Pozisyonlar = _itemModel.ListPozisyonlar;
            //_cvDetail.KisiselBilgisi!=null ? _itemModel.AskerlikDurumList.Find(a => a.DurumId == _cvDetail.KisiselBilgisi.AskerlikDurum).Durum : "";
            var detail = new List<CvDetail>();
            detail.Add(_cvDetail);
            if (id == 0) //0 ise isarayan girişinden gelmiştir.değil ise işverende cv görüntüleniyordur.
            {
                ViewBag.editcv = true;
                return View(detail.AsEnumerable());
            }
            else
            {
                ViewBag.editcv = false;
                return View(detail.AsEnumerable());
            }
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserProfile user, string Command)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("Index");
            }

            return View(user);
        }

        public ActionResult CvEditInfo(int id)
        {

            var editCvIletisim = _efCvIletisimBilgiDal.GetList().FirstOrDefault(i => i.IletisimBilgiId == id);


            ViewBag.Sehirler = _efSetupCityDal.GetList();
            ViewBag.Ilceler = _efSetupCountyDal.GetList();
            ViewBag.Ulkeler = _efSetupCountryDal.GetList();
            return PartialView(editCvIletisim);
        }

        [HttpPost]
        public ActionResult CvEditInfo(CvIletisimBilgi editCvIletisim)
        {
            if (ModelState.IsValid)
            {
                _efCvIletisimBilgiDal.Update(editCvIletisim);
                editCvIletisim = _efCvIletisimBilgiDal.Get(editCvIletisim);
                var iletisimList = new List<CvIletisimBilgi>();
                iletisimList.Add(editCvIletisim);
                return PartialView("_IletisimBilgileriPartial", iletisimList.AsEnumerable());
            }
            else
            {

                return PartialView(editCvIletisim);
            }
        }

        
        public ActionResult CvAddInfo()
        {
            ViewBag.Sehirler = _efSetupCityDal.GetList();
            ViewBag.Ilceler = _efSetupCountyDal.GetList();
            ViewBag.Ulkeler = _efSetupCountryDal.GetList();
         
            var user = _efUserDal.Get(new UserProfile() {UserId = userid});
            return
                PartialView(new CvIletisimBilgi
                {
                    UserId = userid,
                    UyeAd = user.MemberName,
                    UyeSoyad = user.MemberSurName,
                    UyeMail = user.Email,
                    MobilTel = user.MobilPhone
                });
        }

        [HttpPost]
        public ActionResult CvAddInfo(CvIletisimBilgi addInfo)
        {
            if (ModelState.IsValid)
            {
                _efCvIletisimBilgiDal.Add(addInfo);
            }
            var iletisimList = new List<CvIletisimBilgi>();
            addInfo = _efCvIletisimBilgiDal.Get(addInfo);
            SetupCity city = _efSetupCityDal.Get(new SetupCity() {CityId = addInfo.CityId});
            SetupCounty county = _efSetupCountyDal.Get(new SetupCounty() {CountyId = addInfo.CountyId});
            SetupCountry country = _efSetupCountryDal.Get(new SetupCountry() {CountryId = addInfo.CountryId});
            addInfo.SetupCity = city;
            addInfo.SetupCounty = county;
            addInfo.SetupCountry = country;
            iletisimList.Add(addInfo);
            return PartialView("_IletisimBilgileriPartial", iletisimList.AsEnumerable());

        }

        public ActionResult CvEditKisiselBilgi(int id)
        {
            ViewBag.askerlik = _itemModel.AskerlikDurumList;
            ViewBag.surucuSinif = _itemModel.SurucuSinifList;
            ViewBag.Ulkeler = _efSetupCountryDal.GetList();
            ViewBag.Sehirler = _efSetupCityDal.GetList();
            var egitimSeviyesiModel = new EgitimSeviyesiModel();
            ViewBag.egitimSeviyesiList = egitimSeviyesiModel.EgitimSeviyesiList;
            var editCvKisiselBilgi = _efCvKisiselBilgiDal.GetList().FirstOrDefault(i => i.KisiselBilgiId == id);
            if (editCvKisiselBilgi == null) throw new ArgumentNullException("id");
            ViewBag.divid = "test";

            return PartialView(editCvKisiselBilgi);
        }

        [HttpPost]
        public ActionResult CvEditKisiselBilgi(CvKisiselBilgi editKisiselBilgi)
        {

            _efCvKisiselBilgiDal.Update(editKisiselBilgi);
            var kisiselBilgilist = new List<CvKisiselBilgi>();
            kisiselBilgilist.Add(_efCvKisiselBilgiDal.Get(editKisiselBilgi));

            ViewBag.askerlik = _itemModel.AskerlikDurumList;
            ViewBag.surucuSinif = _itemModel.SurucuSinifList;
            ViewBag.Ulkeler = _efSetupCountryDal.GetList();
            ViewBag.Sehirler = _efSetupCityDal.GetList();
            var egitimSeviyesiModel = new EgitimSeviyesiModel();
            ViewBag.egitimSeviyesiList = egitimSeviyesiModel.EgitimSeviyesiList;
            return PartialView("_KisiselBilgilerPartial", kisiselBilgilist.AsEnumerable());
        }

        public ActionResult CvCreateKisiselBilgi()
        {
            ViewBag.askerlik = _itemModel.AskerlikDurumList;
            ViewBag.surucuSinif = _itemModel.SurucuSinifList;
            ViewBag.Ulkeler = _efSetupCountryDal.GetList();
            ViewBag.Sehirler = _efSetupCityDal.GetList();
            var egitimSeviyesiModel = new EgitimSeviyesiModel();
            ViewBag.egitimSeviyesiList = egitimSeviyesiModel.EgitimSeviyesiList;
           
            return PartialView(new CvKisiselBilgi {UserId = userid});
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CvCreateKisiselBilgi(CvKisiselBilgi addCvKisiselBilgi)
        {

            if (ModelState.IsValid)
            {
                _efCvKisiselBilgiDal.Add(addCvKisiselBilgi);
            }

            ViewBag.askerlik = _itemModel.AskerlikDurumList;
            ViewBag.surucuSinif = _itemModel.SurucuSinifList;
            ViewBag.Ulkeler = _efSetupCountryDal.GetList();
            ViewBag.Sehirler = _efSetupCityDal.GetList();
            var egitimSeviyesiModel = new EgitimSeviyesiModel();
            ViewBag.egitimSeviyesiList = egitimSeviyesiModel.EgitimSeviyesiList;
            var kisiselBilgilist = new List<CvKisiselBilgi>();


            SetupCity city = _efSetupCityDal.Get(new SetupCity() {CityId = addCvKisiselBilgi.CityId});

            SetupCountry country = _efSetupCountryDal.Get(new SetupCountry() {CountryId = addCvKisiselBilgi.CountryId});
            addCvKisiselBilgi.SetupCity = city;
            addCvKisiselBilgi.SetupCountry = country;
            kisiselBilgilist.Add(addCvKisiselBilgi);


            return PartialView("_KisiselBilgilerPartial", kisiselBilgilist.AsEnumerable());


        }

        public ActionResult CvAddIsdeneyim()
        {
            ViewBag.Sektorlist = _itemModel.ListSektorler;
            ViewBag.Pozisyonlar = _itemModel.ListPozisyonlar;
            return PartialView(new CvIsDeneyimleri {UserId = userid});
        }

        [HttpPost]
        public ActionResult CvAddIsdeneyim(CvIsDeneyimleri addCvIsDeneyimleri)
        {
            _efCvIsDeneyimDal.Add(addCvIsDeneyimleri);
         
            //var editCvIsDeneyimlist = _efCvIsDeneyimDal.GetList().Where(i => i.UserId == userid).ToList();
            var editCvIsDeneyimlist = (from isd in _efCvIsDeneyimDal.GetList().Where(i => i.UserId == userid)
                                       join scat in _itemModel.ListSektorler on isd.FirmaSektoru equals scat.Value
                                       join poz in _itemModel.ListPozisyonlar on isd.FirmadakiPozisyon equals poz.Value
                                       select new CvIsDeneyimleri()
                                       {
                                           FirmaAdi = isd.FirmaAdi,
                                           FirmaSektoru = scat.Text,
                                           FirmadakiPozisyon = poz.Text,
                                           IsAlani = isd.IsAlani,
                                           Istanimi = isd.Istanimi,
                                           BaslangicTarihi = isd.BaslangicTarihi,
                                           BitisTarihi = isd.BitisTarihi,
                                           UserId = isd.UserId,
                                           User = isd.User,
                                           IsDeneyimleriId = isd.IsDeneyimleriId
                                       }
                    ).OrderByDescending(i => i.BaslangicTarihi).ToList();
            return PartialView("_IsDeneyimleriPartial", editCvIsDeneyimlist);
        }

        public ActionResult CvEditIsDeneyim(int id)
        {
            ViewBag.Sektorlist = _itemModel.ListSektorler;
            ViewBag.Pozisyonlar = _itemModel.ListPozisyonlar;
            var editCvIsDeneyim =_efCvIsDeneyimDal.GetList().FirstOrDefault(i => i.IsDeneyimleriId == id);


            return PartialView(editCvIsDeneyim);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CvEditIsDeneyim(CvIsDeneyimleri updIsDeneyimleri, int is_delete = 0)
        {
            if (ModelState.IsValid)
            {
                if (is_delete == 0)
                {
                    _efCvIsDeneyimDal.Update(updIsDeneyimleri);
                }
                else
                {
                    _efCvIsDeneyimDal.Del(updIsDeneyimleri);
                }

                var editCvIsDeneyimlist = (from isd in _efCvIsDeneyimDal.GetList().Where(i => i.UserId == userid)
                    join scat in _itemModel.ListSektorler on isd.FirmaSektoru equals scat.Value
                    join poz in _itemModel.ListPozisyonlar on isd.FirmadakiPozisyon equals poz.Value
                    select new CvIsDeneyimleri()
                    {
                        FirmaAdi = isd.FirmaAdi,
                        FirmaSektoru = scat.Text,
                        FirmadakiPozisyon = poz.Text,
                        IsAlani = isd.IsAlani,
                        Istanimi = isd.Istanimi,
                        BaslangicTarihi = isd.BaslangicTarihi,
                        BitisTarihi = isd.BitisTarihi,
                        UserId = isd.UserId,
                        User = isd.User,
                        IsDeneyimleriId = isd.IsDeneyimleriId
                    }
                    ).OrderByDescending(i => i.BaslangicTarihi).ToList();


                    //_efCvIsDeneyimDal.GetList()
                    //    .Where(i => i.UserId == userid)
                    //    .OrderByDescending(i => i.BaslangicTarihi)
                    //    .ToList();
                return PartialView("_IsDeneyimleriPartial", editCvIsDeneyimlist);
            }
            else
            {
                return PartialView(updIsDeneyimleri);
            }
        }

        public ActionResult AddEgitim()
        {
           

            var egitimSeviyesiModel = new EgitimSeviyesiModel();
            ViewBag.egitimSeviyesiList = egitimSeviyesiModel.EgitimSeviyesiList;
            return PartialView(new CvEgitimUniversiteBilgi() {UserId = userid});
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AddEgitim(CvEgitimUniversiteBilgi egitimadd)
        {
            if (ModelState.IsValid)
            {
                _efCvEgitimDal.Add(egitimadd);

            }
          


            var egitimSeviyesiModel = new EgitimSeviyesiModel();
            ViewBag.egitimSeviyesiList = egitimSeviyesiModel.EgitimSeviyesiList;


            List<CvEgitimUniversiteBilgi> universiteEgitimList =
                _efCvEgitimDal.GetList().Where(e => e.UserId == userid).OrderBy(e => e.EgitimTipi).ToList();
            return PartialView("_UniversiteEgitimPartial", universiteEgitimList.AsEnumerable());
        }

        public ActionResult EditEgitim(int id)
        {
            var editegitimUniversite = _efCvEgitimDal.GetList().FirstOrDefault(e => e.EgitimId == id);

            var egitimSeviyesiModel = new EgitimSeviyesiModel();
            ViewBag.egitimSeviyesiList = egitimSeviyesiModel.EgitimSeviyesiList;

            return PartialView(editegitimUniversite);
        }

        [HttpPost]
        public ActionResult EditEgitim(CvEgitimUniversiteBilgi updCvEgitimUniversiteBilgi, int is_delete = 0)
        {
            if (is_delete == 0)
                _efCvEgitimDal.Update(updCvEgitimUniversiteBilgi);
            else
            {
                _efCvEgitimDal.Del(updCvEgitimUniversiteBilgi);
            }
            var egitimSeviyesiModel = new EgitimSeviyesiModel();
            ViewBag.egitimSeviyesiList = egitimSeviyesiModel.EgitimSeviyesiList;

           
            var egitimUniversiteList =
                _efCvEgitimDal.GetList().Where(e => e.UserId == userid).OrderBy(e => e.EgitimTipi).ToList();

            return PartialView("_UniversiteEgitimPartial", egitimUniversiteList);
        }

        public ActionResult AddDil()
        {
            
            ViewBag.Dil = _itemModel.Diller;
            ViewBag.DilSurec = _itemModel.Dilsureclist;
          
            return PartialView(new CvDilBilgi() {UserId = userid});
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AddDil(CvDilBilgi addCvDilBilgi)
        {
            if (ModelState.IsValid)
            {
                _efCvDilBilgiDal.Add(addCvDilBilgi);

           
                var dillist = _efCvDilBilgiDal.GetList().Where(s => s.UserId == userid).ToList();
                return PartialView("_YabanciDillerPartial", dillist);
            }
            else
            {
                return PartialView(addCvDilBilgi);
            }
        }

        [AllowAnonymous]
        public ActionResult EditDil(int id)
        {
            ViewBag.Dil = _itemModel.Diller;
            ViewBag.DilSurec = _itemModel.Dilsureclist;
            var editYabanciDil = _efCvDilBilgiDal.GetList().FirstOrDefault(d => d.DilBilgId == id);
            return PartialView(editYabanciDil);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult EditDil(CvDilBilgi updCvDilBilgi, int is_delete = 0)
        {
            if (is_delete == 0)
                _efCvDilBilgiDal.Update(updCvDilBilgi);
            if (is_delete == 1)
                _efCvDilBilgiDal.Del(updCvDilBilgi);
        
            var dillist = _efCvDilBilgiDal.GetList().Where(s => s.UserId == userid).ToList();

            return PartialView("_YabanciDillerPartial", dillist);
        }


        public ActionResult IlceGetir(int ilId)
        {
            var ilceEnum =
                _efSetupCountyDal.GetList().Where(i => i.CityId == ilId).Select(x => new {x.CountyId, x.County});

            return Json(new SelectList(ilceEnum, "CountyId", "County"));
        }

        public ActionResult UserImage(int id)
        {
            var resimUser = _efUserDal.Get(new UserProfile() {UserId = id});
            return PartialView("_ResimYuklePartial", resimUser);
        }

        [HttpPost]
        public ActionResult UserImage(int userId, HttpPostedFileBase resimfile)
        {
            var userImage = _efUserDal.Get(new UserProfile() {UserId = userId});
            if (resimfile != null)
            {
                //Sunucuya dosya kaydedilirken, sunucunun dosya sistemini, yolunu bilemeyeceğimiz için
                //Server.MapPath() ile sitemizin ana dizinine gelmiş oluruz. Devamında da sitemizdeki
                //yolu tanımlarız.
                //resimfile.SaveAs(Server.MapPath("~/Images/UserImages/") + resimfile.FileName);
                //userImage.ResimYol = "/Images/UserImages/" + resimfile.FileName;

                //byte[] resimBytelar = new byte[resimfile.ContentLength];
                //resimfile.InputStream.Read(resimBytelar, 0, resimfile.ContentLength);
                //userImage.ResimYol = resimBytelar;
                var fileName = Path.GetFileName(resimfile.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/UserImages"), fileName);

                resimfile.SaveAs(path);
                byte[] imageByteData = System.IO.File.ReadAllBytes(path);
                string imageBase64Data = Convert.ToBase64String(imageByteData);
                string imageDataUrl = string.Format("data:image/png;base64,{0}", imageBase64Data);

                userImage.ResimYol = imageDataUrl;



                _efUserDal.Update(userImage);
            }
            return RedirectToAction("Index", "Cv");
        }

        public ActionResult CvYazdir(int id = 0)
        {
            userid = id;
            if (userid == 0) //0 ise isarayan girişinden gelmiştir.değil ise işverende cv görüntüleniyordur.
            {
                if (Membertype() == null)
                {
                    WebSecurity.Logout();
                    return RedirectToAction("Login", "Account",
                        routeValues: new {id = 0});
                }
                userid = Membertype().UserId;
            }



            _cvDetail.IletisimBilgisi = _efCvIletisimBilgiDal.GetList().FirstOrDefault(info => info.UserId == userid);

            _cvDetail.KisiselBilgisi = _efCvKisiselBilgiDal.GetList().FirstOrDefault(k => k.UserId == userid);
            _cvDetail.IsDeneyimleri = _efCvIsDeneyimDal.GetList().Where(i => i.UserId == userid).ToList();
            _cvDetail.Universiteler = _efCvEgitimDal.GetList().Where(e => e.UserId == userid).ToList();
            _cvDetail.Diller = _efCvDilBilgiDal.GetList().Where(d => d.UserId == userid).ToList();

            _cvDetail.User = _efUserDal.Get(new UserProfile() {UserId = userid});
            ViewBag.asker = 1;
                //_cvDetail.KisiselBilgisi!=null ? _itemModel.AskerlikDurumList.Find(a => a.DurumId == _cvDetail.KisiselBilgisi.AskerlikDurum).Durum : "";
            var detail = new List<CvDetail>();
            detail.Add(_cvDetail);

            return View(detail.AsEnumerable());


        }

        public ActionResult EditOther(int id, int tip)
        {

            var edtOther = _efCvOther.GetList().FirstOrDefault(i => i.OtherId == id);
            ViewBag.tip = tip;
            return PartialView("EditOther", model: edtOther);
        }

        [HttpPost]
        public string EditOther(CvOther editother, int tip)
        {
            _efCvOther.Update(editother);
            string valueHtml = "";
            if (tip == 0)
                valueHtml = editother.OzetBilgi;
            if (tip == 1)
                valueHtml = editother.BilgisayarBilgi;
            if (tip == 2)
            {
                valueHtml =
                    string.Format(
                        "<li><strong><span >Hobiler/İlgi Alanları</span></strong>: {0}</li><li><strong><span>Üye Olunan Topluluklar</span></strong>: {1}</li> <li><strong> <span >Sosyal sorululuk çalışmaları</span></strong>: {2}</li>",
                        editother.Hobiler, editother.Uyelikler, editother.SosyalSorumlulukCalismalari);
            }
            if (tip == 3)
            {
                valueHtml =
                    string.Format(
                        "<li><div class=ref-name>{0}</div><div class=ref-title>{1} - </div><div class=ref-detail>Tel : {2}</div><div class=ref-detail>Eposta : {3}</div></li>",
                        editother.Ref1AdSoyad, editother.Ref1Pozisyon, editother.Ref1Tel, editother.Ref1Mail);
                if (!string.IsNullOrEmpty(editother.Ref2AdSoyad))
                    valueHtml +=
                        string.Format(
                            "<hr><li><div class=ref-name>{0}</div><div class=ref-title>{1} - </div><div class=ref-detail>Tel : {2}</div><div class=ref-detail>Eposta : {3}</div></li>",
                            editother.Ref2AdSoyad, editother.Ref2Pozisyon, editother.Ref2Tel, editother.Ref2Mail);
            }
            if (tip == 4)
            {
                valueHtml = string.Format("<li>{0}</li>", editother.BurslarProjeler);
            }

            return HttpUtility.HtmlDecode(valueHtml);
        }

        public ActionResult EditSertifika(int id)
        {

            var edtSertifika = _efCvSertifikaDal.GetList().FirstOrDefault(i => i.SertifikaId == id);

            return PartialView("EditSertifika", model: edtSertifika);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult EditSertifika(CvSertifikaBilgi editSertifikaBilgi, int is_delete = 0)
        {
            if (is_delete == 1)
                _efCvSertifikaDal.Del(editSertifikaBilgi);
            else
                _efCvSertifikaDal.Update(editSertifikaBilgi);

            var serlist = _efCvSertifikaDal.GetList().Where(s => s.UserId == userid).ToList();
            return PartialView("_SertifikaPartial", serlist);

        }

        public ActionResult AddSertifika()
        {
           
            return PartialView(new CvSertifikaBilgi() {UserId = userid});
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AddSertifika(CvSertifikaBilgi addsSertifikaBilgi)
        {
            _efCvSertifikaDal.Add(addsSertifikaBilgi);
            var serlist = _efCvSertifikaDal.GetList().Where(s => s.UserId == userid).ToList();
            return PartialView("_SertifikaPartial", serlist);
        }

        public ActionResult AddSeminer()
        {
          
            return PartialView(new CvSeminerler() {UserId = userid});
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AddSeminer(CvSeminerler addseminer, string gun, string ay, string sene, string gunbitis,
            string aybitis, string senebitis)
        {

            string itarih = string.Format("{0}/{1}/{2}", gun, ay, sene);
            string starih = string.Format("{0}/{1}/{2}", gunbitis, aybitis, senebitis);
            addseminer.EgitimBaslangic = itarih;
            addseminer.EgitimBitis = starih;
            _efSeminerDal.Add(addseminer);
            
            var seminerList = _efSeminerDal.GetList().Where(u => u.UserId == userid).ToList();

            return PartialView("_SeminerPartial", seminerList);
        }

        public ActionResult EditSeminer(int id)
        {
            var edtSeminer = _efSeminerDal.GetList().FirstOrDefault(i => i.SeminerId == id);

            return PartialView(edtSeminer);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult EditSeminer(CvSeminerler editSeminerler, int is_delete, string gun, string ay, string sene,
            string gunbitis, string aybitis, string senebitis)
        {
            string itarih = string.Format("{0}/{1}/{2}", gun, ay, sene);
            string starih = string.Format("{0}/{1}/{2}", gunbitis, aybitis, senebitis);
            editSeminerler.EgitimBaslangic = itarih;
            editSeminerler.EgitimBitis = starih;
            if (is_delete == 1)
                _efSeminerDal.Del(editSeminerler);
            else
                _efSeminerDal.Update(editSeminerler);

            var serlist = _efSeminerDal.GetList().Where(s => s.UserId == userid).ToList();
            return PartialView("_SeminerPartial", serlist);
        }

        public ActionResult AddSinav()
        {
            ViewBag.sinavTurleri = _itemModel.ListSinavTurleri;
            return PartialView(new CvSinavBilgi() {UserId = userid});
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AddSinav(CvSinavBilgi addSinavBilgi, string ay, string sene)
        {
            string sinavtarihi = string.Format("{0}/{1}", ay, sene);
            addSinavBilgi.SinavTarih = sinavtarihi;
            _efCvSinavDal.Add(addSinavBilgi);
            var sinavlist = _efCvSinavDal.GetList().Where(s => s.UserId == userid).ToList();
            return PartialView("_SinavPartial", sinavlist);
        }

        public ActionResult EditSinav(int id)
        {
           ViewBag.sinavTurleri = _itemModel.ListSinavTurleri;

            var edtSinav = _efCvSinavDal.GetList().FirstOrDefault(i => i.SinavId == id);

            return PartialView(edtSinav);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult EditSinav(CvSinavBilgi editSinavBilgi,string ay,string sene)
        {
            string sinavtarihi = string.Format("{0}/{1}", ay, sene);
            editSinavBilgi.SinavTarih = sinavtarihi;
                   _efCvSinavDal.Update(editSinavBilgi);
                 
                   var sinavlist = _efCvSinavDal.GetList().Where(s => s.UserId == userid).ToList();
                   return PartialView("_SinavPartial", sinavlist);

        }
        [HttpPost]
        public ActionResult EditWorkDetail(string[] CalismakIstedigiAlan , string[] IstenilenPozisyon, string CalismaDurumu)
        {
            string departmanlar = string.Empty;
            string pozisyonlar = string.Empty;
            if (CalismakIstedigiAlan.Length > 0)
            {
                departmanlar = ",";
               
                foreach (var item in CalismakIstedigiAlan)
                {
                    departmanlar += item.ToString() + ",";
                }
            }
            if (IstenilenPozisyon.Length > 0)
            {
                pozisyonlar = ",";
                foreach (var item in IstenilenPozisyon)
                {
                    pozisyonlar += item.ToString() + ",";
                }
            }
                var editother= _efCvOther.GetList().FirstOrDefault(i => i.UserId==userid);
            editother.CalismakIstedigiAlan = departmanlar;
            editother.IstenilenPozisyon = pozisyonlar;
            editother.CalismaDurumu =CalismaDurumu;
            _efCvOther.Update(editother);
          return  RedirectToAction("Index", "Cv");
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