using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using KagiderKariyerPortal.Dal.ConCreate.EntityFramework;
using KagiderKariyerPortal.Entities.ConCreate;
using KagiderKariyerPortalMvc.Models;
using WebMatrix.WebData;

namespace KagiderKariyerPortalMvc.Controllers
{
    
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        readonly EfUserDal _userDal=new EfUserDal();
        KagiderContext dbContext = new KagiderContext();
       // KagiderContext _context=new KagiderContext();
        [Authorize(Users = "panel")]
        public ActionResult Index()
        {
            if (WebSecurity.IsAuthenticated && WebSecurity.GetUserId("panel")>0)
            {
                var userList = _userDal.GetList().Where(o => o.MemberTypeId == 1 && o.OnayDurumu == false).ToList();

                var listCategory = dbContext.MainCategories.ToList();

                var listOfValues = Enum.GetValues(typeof(EnumCategories));
                if (listCategory.Count == 0)
                {
                    listCategory = new List<MainCategory>();

                    foreach (var item in listOfValues)
                    {
                        listCategory.Add(new MainCategory() { MainCategoryId = (int)item, MainCategoryName = item.ToString() });
                    }

                    dbContext.MainCategories.AddRange(listCategory.AsEnumerable());
                    dbContext.SaveChanges();
                }
                ViewBag.ListCategory = listCategory;

                var subCategories = (from mcat in dbContext.MainCategories.ToList()
                                     join scat in dbContext.SubCategories.ToList() on mcat.MainCategoryId equals scat.MainCategoryId
                                     join rcat in dbContext.MainCategories.ToList() on scat.RelatedId equals rcat.MainCategoryId into rc
                                     from related in rc.DefaultIfEmpty()
                                     select
                                           new SubCategoryView()
                                           {
                                               MCatName = scat.MainCategory.MainCategoryName,
                                               SCatName = scat.SubCategoryName,
                                               //  RCatName = (from k in dbContext.MainCategories where k.MainCategoryId == scat.RelatedId select k.MainCategoryName ).SingleOrDefault()

                                               RCatName = related == null ? string.Empty : related.MainCategoryName

                                           }
                                         ).ToList();
                ViewBag.subCatList = subCategories;



                return View(userList);
            }
            else
            {
               return RedirectToAction("Login", "Account", routeValues: new {id = 99,viewtype=0,returnUrl="/Admin/Index"});
            }
        }
   [Authorize(Users = "panel")]
        [HttpPost]
        public ActionResult Onayla(List<int> onay,UserProfile updUser)
        {
            if(onay!=null)
            { 
                    foreach (var ouserid in onay)
                    {
                        dbContext.Database.ExecuteSqlCommand("UPDATE Users Set OnayDurumu=1,OnayTarihi=GETDATE() WHERE UserId=" +
                                                        ouserid); 
                    }


                    dbContext.SaveChanges();
            }
           // _userDal.Update();
            return RedirectToAction("Index");
        }
         [Authorize(Users = "panel")]
        public ActionResult SearchUser(bool onay)
        {
           var userList = _userDal.GetList().Where(o => o.MemberTypeId == 1 && o.OnayDurumu == onay).ToList();

           var listCategory = dbContext.MainCategories.ToList();

           var listOfValues = Enum.GetValues(typeof(EnumCategories));
           if (listCategory.Count == 0)
           {
               listCategory = new List<MainCategory>();

               foreach (var item in listOfValues)
               {
                   listCategory.Add(new MainCategory() { MainCategoryId = (int)item, MainCategoryName = item.ToString() });
               }

               dbContext.MainCategories.AddRange(listCategory.AsEnumerable());
               dbContext.SaveChanges();
           }
           ViewBag.ListCategory = listCategory;

           var subCategories = (from mcat in dbContext.MainCategories.ToList()
                                join scat in dbContext.SubCategories.ToList() on mcat.MainCategoryId equals scat.MainCategoryId
                                join rcat in dbContext.MainCategories.ToList() on scat.RelatedId equals rcat.MainCategoryId into rc
                                from related in rc.DefaultIfEmpty()
                                select
                                      new SubCategoryView()
                                      {
                                          MCatName = scat.MainCategory.MainCategoryName,
                                          SCatName = scat.SubCategoryName,
                                          //  RCatName = (from k in dbContext.MainCategories where k.MainCategoryId == scat.RelatedId select k.MainCategoryName ).SingleOrDefault()

                                          RCatName = related == null ? string.Empty : related.MainCategoryName

                                      }
                                    ).ToList();
           ViewBag.subCatList = subCategories;


            return View("Index",userList);
        }
      
        // GET: Settings
        //  [Authorize]
        [Authorize(Users = "panel")]
        [HttpPost]
        public ActionResult AddSubCategory(SubCategory subCategory)
        {
            dbContext.SubCategories.Add(subCategory);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
       [Authorize(Users = "panel")]
        [HttpPost]
        public ActionResult DelSubCategory(int id)
        {
            var subCategory = dbContext.SubCategories.FirstOrDefault(p => p.SubCategoryId == id);

            subCategory.SubCategoryName = "a";
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        [Authorize(Users = "panel")]
         public ActionResult SearchSubCategory(int MainCategoryId)
        {
            var userList = _userDal.GetList().Where(o => o.MemberTypeId == 1 && o.OnayDurumu == false).ToList();
            var listCategory = dbContext.MainCategories.ToList();

            var listOfValues = Enum.GetValues(typeof(EnumCategories));
            if (listCategory.Count == 0)
            {
                listCategory = new List<MainCategory>();

                foreach (var item in listOfValues)
                {
                    listCategory.Add(new MainCategory() { MainCategoryId = (int)item, MainCategoryName = item.ToString() });
                }

                dbContext.MainCategories.AddRange(listCategory.AsEnumerable());
                dbContext.SaveChanges();
            }
            ViewBag.ListCategory = listCategory;

            var subCategories = (from mcat in dbContext.MainCategories.ToList().Where(p => p.MainCategoryId == MainCategoryId)
                                 join scat in dbContext.SubCategories.ToList() on mcat.MainCategoryId equals scat.MainCategoryId
                                 join rcat in dbContext.MainCategories.ToList() on scat.RelatedId equals rcat.MainCategoryId into rc
                                 from related in rc.DefaultIfEmpty()
                                 select
                                       new SubCategoryView()
                                       {
                                           MCatName = scat.MainCategory.MainCategoryName,
                                           SCatName = scat.SubCategoryName,
                                           //  RCatName = (from k in dbContext.MainCategories where k.MainCategoryId == scat.RelatedId select k.MainCategoryName ).SingleOrDefault()

                                           RCatName = related == null ? string.Empty : related.MainCategoryName

                                       }
                                     ).ToList();
            ViewBag.subCatList = subCategories;
            return View("Index", userList);
        }
    }
}
