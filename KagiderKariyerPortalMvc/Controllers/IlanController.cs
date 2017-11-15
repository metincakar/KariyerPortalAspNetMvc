using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using KagiderKariyerPortal.Dal.ConCreate.EntityFramework;
using KagiderKariyerPortal.Entities.ConCreate;
using KagiderKariyerPortalMvc.Models;
using Microsoft.Ajax.Utilities;
using WebMatrix.WebData;

namespace KagiderKariyerPortalMvc.Controllers
{
    public class IlanController : Controller
    {
        readonly EfFimaDal _efFirmaDal=new EfFimaDal();
        readonly EfIlanlarDal _efIlanDal=new EfIlanlarDal();

        readonly EfSetupCityDal _efSetupCityDal = new EfSetupCityDal();
        readonly EfSetupCountyDal _efSetupCountyDal = new EfSetupCountyDal();
        readonly EfSetupCountryDal _efSetupCountryDal = new EfSetupCountryDal();
        readonly EfCvIletisimBilgiDal _efCvIletisimBilgiDal = new EfCvIletisimBilgiDal();
        readonly EfUserDal _efUserDal=new EfUserDal();

        readonly EfIlanBasvuruDal _efIlanBasvuruDal = new EfIlanBasvuruDal();
        private readonly CvItemModel _itemModel = new CvItemModel();

        readonly int[] _isveren = new int[] { 1, 4 };
        readonly int[] _isarayan = new int[] { 0, 2, 3 };
        public int PageSize = 10;
        KagiderContext dbContext=new KagiderContext();
        public ViewResult Index(int page=1,int id=0)
        {

            ViewBag.logintype = 0;//isarayan

            ViewBag.Sehirler = _efSetupCityDal.GetList();
            ViewBag.Ilceler = _efSetupCountyDal.GetList();
            ViewBag.Ulkeler = _efSetupCountryDal.GetList();
            ViewBag.ListDepartman = _itemModel.ListDepartmanlar;
            ViewBag.Pozisyonlar = _itemModel.ListPozisyonlar;
            ViewBag.Sektorlist = _itemModel.ListSektorler;
            ViewBag.pozisyonSeviyeList = _itemModel.ListPozisyonseviye;
            ViewBag.pozisyonTipList = _itemModel.ListPozisyontip;
            ViewBag.selectedFirmaad ="";
            ViewBag.selectedSektorlist = null;
            ViewBag.selectedPozisyonlist = null;
            ViewBag.selectedPseviye = null;
            ViewBag.selectedPtip = null;
          
            if (WebSecurity.IsAuthenticated && Membertype().MemberTypeId==1)
            {
                ViewBag.logintype = 1;//isveren
            }

            int currentPage = page;
            //var ilanList = _efIlanDal.GetList().Where(p => p.IlanId== id || id == 0 && p.IlanFinishDate>=DateTime.Now && p.IlanDurum==true).ToList(); ;

          
                var ilanList = _efIlanDal.GetList().Where(p => p.IlanId == id || id == 0 && p.IlanDurum == true).ToList();
           

            /*
               var ilanList =
               (from iln in
                    _efIlanDal.GetList()
                        .Where(p => p.IlanId == id || id == 0 && p.IlanFinishDate >= DateTime.Now && p.IlanDurum == true)
                        .ToList()
                join bsv in _efIlanBasvuruDal.GetList().Where(b=>b.UserId==(int)Session["uid"]).ToList()
                    on iln.IlanId equals bsv.IlanId
                    into gr
                from ljoin in gr.DefaultIfEmpty()
                select new IlanBasvuruModel
                {
                    IlanId = iln.IlanId,
                    IlanTarihi = iln.IlanStartDate,
                    IlanTitle = iln.IlanTitle,
                    IlanBitisTarihi = iln.IlanFinishDate,
                    IlanFirma=iln.Firma,
                    IlanFirmaId=iln.FirmaId,
                    BasvuruId= ljoin.BasvuruId,
                    BasvuruTarihi = ljoin.BasvuruTarih,
                    BasvuruYapanId = ljoin.UserId
                }).ToList();
             * */

            List<Ilan> ilanListToBeListed =
                ilanList.Skip((page - 1) * PageSize).Take(PageSize).ToList();

            var pagingInfo = new PagingInfo
            {
                PageSize = PageSize,
                CurrentPage = page,
                TotalItemCount = ilanList.Count,
                CurrentCategory = id
            };
          
                return View("_IlanListPartial", new TumIlanViewModel
                {
                    IlanList = ilanListToBeListed,
                   PagingInfo =pagingInfo
                });

                   
        }

        [HttpPost]
        public ActionResult SearchIlan(IlanSearchModel searchIlan, int page = 1, int id=0)
        {
         if(searchIlan==null)
             searchIlan=new IlanSearchModel();

            ViewBag.ListDepartman = _itemModel.ListDepartmanlar;
            ViewBag.Pozisyonlar = _itemModel.ListPozisyonlar;
            ViewBag.Sektorlist = _itemModel.ListSektorler;
            ViewBag.selectedFirmaad = searchIlan.FirmaAd;
            ViewBag.selectedSektorlist = searchIlan.Sektorler;
            ViewBag.selectedPozisyonlist =searchIlan.ArananPozisyon ?? null;
          
            ViewBag.MPozisyon = new MultiSelectList(_itemModel.ListPozisyonlar, "Value", "Text", ViewBag.selectedPozisyonlist );
            ViewBag.selectedPseviye = searchIlan.PSeviye;
            ViewBag.selectedPtip = searchIlan.PTipi;
            ViewBag.pozisyonSeviyeList = _itemModel.ListPozisyonseviye;
            ViewBag.pozisyonTipList = _itemModel.ListPozisyontip;
            ViewBag.logintype = 0;//isarayan
            if (WebSecurity.IsAuthenticated && Membertype().MemberTypeId==1)
            {
                ViewBag.logintype = 1;//isveren
            }
          
            ViewBag.Sehirler = _efSetupCityDal.GetList();
            ViewBag.Ilceler = _efSetupCountyDal.GetList();
            ViewBag.Ulkeler = _efSetupCountryDal.GetList();
           

            int currentPage = page;
            var ilanList =
                _efIlanDal.GetList()
                    .Where(
                        p =>
                            p.IlanId == id ||
                            id == 0 && p.IlanDurum == true);
                       

                if (!String.IsNullOrEmpty(searchIlan.FirmaAd))
                {
                    ilanList = ilanList.Where(p => p.Firma.FirmaAd.Contains(searchIlan.FirmaAd));
                }
                if (!String.IsNullOrEmpty(searchIlan.Keyword))
                {
                    ilanList = ilanList.Where(p => p.AcikPozisyon.Contains(searchIlan.Keyword));
                }
            if (searchIlan.Sehir > 0)
            {
                ilanList = ilanList.Where(p => p.CityId==searchIlan.Sehir); 
               
            }
            if (searchIlan.Ilce > 0)
            {
                ilanList = ilanList.Where(p => p.CountyId == searchIlan.Ilce); }
            if (searchIlan.ArananPozisyon != null)
            {
                var listilan = new List<Ilan>();
                foreach (var item in searchIlan.ArananPozisyon)
                {
                    string aitem = string.Format(",{0},", item);
                    var a = ilanList.Where(p => p.ArananPozisyonlar != null && p.ArananPozisyonlar.Contains(aitem)).ToList();
                    foreach (var iln in a)
                    {
                        listilan.Add(iln);
                    }
                }
                
                ilanList = listilan.DistinctBy(p => p.IlanId);
            }
            if (searchIlan.Sektorler != null)
            {
                var listilan = new List<Ilan>();
                foreach (var item in searchIlan.Sektorler)
                {
                    string aitem = string.Format(",{0},", item);

                    var a = ilanList.Where(p => p.Firma.FirmaCalismaAlanlari != null && p.Firma.FirmaCalismaAlanlari.Contains(aitem)).ToList();
                    foreach (var iln in a)
                    {
                        listilan.Add(iln);
                    }
                }
                ilanList = listilan.DistinctBy(p => p.IlanId);
            }
            if (searchIlan.PSeviye != null)
            {
                ilanList = ilanList.Where(p => searchIlan.PSeviye.Contains(p.PozisyonSeviyesi.ToString()));

            }
            if (searchIlan.PTipi != null)
            {
                ilanList = ilanList.Where(p => searchIlan.PTipi.Contains(p.PozisyonTipi.ToString()));

            }
            List<Ilan> ilanListToBeListed =
                ilanList.ToList().Skip((page - 1) * PageSize).Take(PageSize).ToList();

            var pagingInfo = new PagingInfo
            {
                PageSize = PageSize,
                CurrentPage = page,
                TotalItemCount = ilanList.ToList().Count,
                CurrentCategory = id
            };

           

            return View("_IlanListPartial", new TumIlanViewModel
            {
                IlanList = ilanListToBeListed,
                PagingInfo = pagingInfo
            });
           

        }
        //[Authorize]
        public ActionResult AddIlan()
        {
            var loginUser = Membertype();
            if (loginUser!=null && loginUser.MemberTypeId==1) //isveren ise
            {
                ViewBag.Sehirler = _efSetupCityDal.GetList();
                ViewBag.Ilceler = _efSetupCountyDal.GetList();
                ViewBag.Ulkeler = _efSetupCountryDal.GetList();
                var egitimSeviyesiModel=new EgitimSeviyesiModel();
                ViewBag.egitimSeviyesiList = egitimSeviyesiModel.EgitimSeviyesiList;
                ViewBag.Pozisyonlar = _itemModel.ListPozisyonlar;
                var firma = _efFirmaDal.GetList().FirstOrDefault(i => i.UserId == loginUser.UserId);
                if(firma!=null)
                    return View("_IlanEklePartial",new Ilan(){Firma = firma,FirmaId = firma.FirmaId,UserId =firma.UserId,IlanDurum = true});
                else
                {
                    var modelfirma = new FirmaModel();
                    var listSelectListItems = GetListItemCityList("");

                    var citiesViewModel = new CitiesViewModel();
                    citiesViewModel.Cities = listSelectListItems;
                    modelfirma.CitiesViewModel = citiesViewModel;
                    modelfirma.Firma = new Firma() { UserId = loginUser.UserId };
                    ViewBag.Sektorlist = _itemModel.ListSektorler;
                    return View("_FirmaEklePartial", modelfirma);
                }
            }
            else
            {
                WebSecurity.Logout();
                return RedirectToAction("Login", "Account", new { id = 1 });//is veren login seçenekleri
            }
            
        }

        [HttpPost]
        [ValidateInput(false)]
        [Authorize]
        public ActionResult AddIlan(Ilan addIlan,string[] IstenilenPozisyon)
        {
            string pozisyonlar = string.Empty;
            if (IstenilenPozisyon.Length > 0)
            {
                pozisyonlar = ",";
                foreach (var item in IstenilenPozisyon)
                {
                    pozisyonlar += item+ ",";
                }
            }
            addIlan.ArananPozisyonlar = pozisyonlar;
            _efIlanDal.Add(addIlan);
            return RedirectToAction("MyIlan");
        }
        public ActionResult EditIlan(int id)
        {
            ViewBag.Sehirler = _efSetupCityDal.GetList();
            ViewBag.Ilceler = _efSetupCountyDal.GetList();
            ViewBag.Ulkeler = _efSetupCountryDal.GetList();
            var egitimSeviyesiModel = new EgitimSeviyesiModel();
            ViewBag.egitimSeviyesiList = egitimSeviyesiModel.EgitimSeviyesiList;
            ViewBag.Pozisyonlar = _itemModel.ListPozisyonlar;

            //var firma = _efFirmaDal.GetList().FirstOrDefault(i => i.UserId == (int)Session["uid"]);

            var ilan = _efIlanDal.Get(new Ilan() { IlanId = id });
            ViewBag.selectedPozisyonlar = ilan.ArananPozisyonlar != null ? ilan.ArananPozisyonlar.Split(',') : null;
            return View(ilan);
        }

        [HttpPost]
        [ValidateInput(false)]
        [Authorize]
        public ActionResult EditIlan(Ilan editilan,string[] IstenilenPozisyon)
        {
            string pozisyonlar = string.Empty;
            if (IstenilenPozisyon!=null)
            {
                pozisyonlar = ",";
                foreach (var item in IstenilenPozisyon)
                {
                    pozisyonlar += item + ",";
                }
            }
            editilan.ArananPozisyonlar = pozisyonlar;
            _efIlanDal.Update(editilan);

            return RedirectToAction("IlanDetail", new { id = editilan.IlanId });
        }
        [HttpPost]
        [Authorize]
        public ActionResult AddFirma(FirmaModel addModel, HttpPostedFileBase logofile)
        {
            if (addModel.CitiesViewModel != null)
            {
                StringBuilder selectedStringBuilder = new StringBuilder();
                selectedStringBuilder.Append(string.Join(",", addModel.CitiesViewModel.SelectedCities));
                addModel.Firma.FaliyetGosterdigiIller = selectedStringBuilder.ToString();
            }
            string sektorler = string.Empty;
            if (addModel.Sektorler!=null)
            {
                sektorler = ",";

                foreach (var item in addModel.Sektorler)
                {
                    sektorler += item.ToString() + ",";
                }
            }
            addModel.Firma.FirmaCalismaAlanlari = sektorler;
            if (logofile != null)
            {
                var fileName = Path.GetFileName(logofile.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/UserImages"), fileName);
                logofile.SaveAs(path);
                byte[] imageByteData = System.IO.File.ReadAllBytes(path);
                string imageBase64Data = Convert.ToBase64String(imageByteData);
                string imageDataUrl = string.Format("data:image/png;base64,{0}", imageBase64Data);
                addModel.Firma.FirmaLogo = imageDataUrl;
            }


            _efFirmaDal.Add(addModel.Firma);

            ViewBag.Sehirler = _efSetupCityDal.GetList();
            ViewBag.Ilceler = _efSetupCountyDal.GetList();
            ViewBag.Ulkeler = _efSetupCountryDal.GetList();
            var egitimSeviyesiModel = new EgitimSeviyesiModel();
            ViewBag.egitimSeviyesiList = egitimSeviyesiModel.EgitimSeviyesiList;

            return RedirectToAction("AddIlan");
            //return PartialView("_IlanEklePartial", new Ilan() { Firma = addModel.Firma, FirmaId = addModel.Firma.FirmaId, UserId = addModel.Firma.UserId });
        }

        public ActionResult GetFirma(int id)
        {
            var loginUser = Membertype();

            if (loginUser == null)
                return RedirectToAction("Login", "Account", new {id = 1});

            var firma = _efFirmaDal.GetList().FirstOrDefault(i => i.UserId ==id);
            if(firma!=null)
            return RedirectToAction("EditFirma", new {id = firma.FirmaId});
            else
            {
                var modelfirma = new FirmaModel();
                var listSelectListItems = GetListItemCityList("");

                var citiesViewModel = new CitiesViewModel();
                citiesViewModel.Cities = listSelectListItems;
                modelfirma.CitiesViewModel = citiesViewModel;
                modelfirma.Firma = new Firma() { UserId = id };
                ViewBag.Sektorlist = _itemModel.ListSektorler;
                return View("_FirmaEklePartial", modelfirma);
            }
        }
        public ActionResult EditFirma(int id)
        {
            var firma=_efFirmaDal.Get(new Firma() {FirmaId = id});
            var listSelectListItems = GetListItemCityList(firma.FaliyetGosterdigiIller);
            var citiesViewModel=new CitiesViewModel();
            citiesViewModel.Cities = listSelectListItems;
            
            var modelfirma = new FirmaModel();
            modelfirma.Firma = firma;
            modelfirma.CitiesViewModel = citiesViewModel;

            ViewBag.calisanSayisi = modelfirma.CalisanSayisiList;
            ViewBag.Sektorlist = _itemModel.ListSektorler;


            return View(modelfirma);
        }
        [HttpPost]
        [Authorize]
        public ActionResult EditFirma(FirmaModel addModel,HttpPostedFileBase logofile)
        {
            if (addModel.CitiesViewModel!= null)
            {
                StringBuilder selectedStringBuilder = new StringBuilder();
                selectedStringBuilder.Append(string.Join(",", addModel.CitiesViewModel.SelectedCities));
                addModel.Firma.FaliyetGosterdigiIller = selectedStringBuilder.ToString();
            }

            if (logofile != null)
            {
                var fileName = Path.GetFileName(logofile.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/UserImages"), fileName);
                logofile.SaveAs(path);
                byte[] imageByteData = System.IO.File.ReadAllBytes(path);
                string imageBase64Data = Convert.ToBase64String(imageByteData);
                string imageDataUrl = string.Format("data:image/png;base64,{0}", imageBase64Data);
                addModel.Firma.FirmaLogo = imageDataUrl;
            }
            string sektorler = string.Empty;
            if (addModel.Sektorler!=null)
            {
                sektorler = ",";

                foreach (var item in addModel.Sektorler)
                {
                    sektorler += item.ToString() + ",";
                }
            }
            addModel.Firma.FirmaCalismaAlanlari = sektorler;
            _efFirmaDal.Update(addModel.Firma);

           // return View(addModel);
            return RedirectToAction("MyIlan");
        }
        public ActionResult IlanDetail(int id)
        {
            if (WebSecurity.IsAuthenticated && Membertype().MemberTypeId==0) //isarayan ise
            {
               
                var basvurusuVarmi =
                    _efIlanBasvuruDal.GetList().FirstOrDefault(i => i.IlanId == id && i.UserId == Membertype().UserId && i.BasvuruDurum == true);
                ViewBag.logintype = 0;
                ViewBag.BasvurusuVarmi = basvurusuVarmi != null;
            }
            else if (WebSecurity.IsAuthenticated && Membertype().MemberTypeId == 1)
            {
                ViewBag.logintype = 1;
               
            }
            else
            {
                ViewBag.logintype = 0;
                ViewBag.BasvurusuVarmi = false;
            }
            var ilan=_efIlanDal.Get(new Ilan() {IlanId =id});
            return View(ilan);
        }

        public List<SelectListItem> GetListItemCityList(string selecteditem)
        {
            var listSelectListItems = new List<SelectListItem>();
            foreach (SetupCity city in _efSetupCityDal.GetList())
            {
              var sec= selecteditem != null && selecteditem.Split(',').Contains(city.CityId.ToString());
                var selectListItem = new SelectListItem()
                {
                    Text = city.City,
                    Value = city.CityId.ToString(CultureInfo.InvariantCulture),
                    Selected = sec
                };
                listSelectListItems.Add(selectListItem);
            }
            return listSelectListItems;
        }
        [Authorize]
        public ActionResult MyIlan(int page=0)
        {
            ViewBag.logintype = 0;

            ViewBag.Sehirler = _efSetupCityDal.GetList();
            ViewBag.Ilceler = _efSetupCountyDal.GetList();
            ViewBag.Ulkeler = _efSetupCountryDal.GetList();
            ViewBag.ListDepartman = _itemModel.ListDepartmanlar;
            ViewBag.Pozisyonlar = _itemModel.ListPozisyonlar;
            ViewBag.Sektorlist = _itemModel.ListSektorler;
            ViewBag.pozisyonSeviyeList = _itemModel.ListPozisyonseviye;
            ViewBag.pozisyonTipList = _itemModel.ListPozisyontip;
            var loginUser = Membertype();
             if (loginUser.MemberTypeId==1) //isveren ise
            {
                ViewBag.logintype = 1;
                    int currentPage = page;
                    var ilanList = _efIlanDal.GetList().Where(u => u.UserId == loginUser.UserId).ToList();


                    List<Ilan> ilanListToBeListed =
                        ilanList.Skip((page - 1) * PageSize).Take(PageSize).ToList();

                    var pagingInfo = new PagingInfo
                    {
                        PageSize = PageSize,
                        CurrentPage = page,
                        TotalItemCount = ilanList.Count,
                        CurrentCategory = 0
                    };

                    return View("_IlanListPartial", new TumIlanViewModel
                    {
                        IlanList = ilanListToBeListed,
                        PagingInfo = pagingInfo
                    });

            }
             else
             {
                 WebSecurity.Logout();
                 return RedirectToAction("Login", "Account",new{id=1});//is veren login seçenekleri
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