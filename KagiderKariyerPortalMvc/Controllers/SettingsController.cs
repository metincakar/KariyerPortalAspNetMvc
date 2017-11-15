
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KagiderKariyerPortal.Dal.ConCreate.EntityFramework;
using KagiderKariyerPortalMvc.Models;


namespace KagiderKariyerPortalMvc.Controllers
{
    public class SettingsController : Controller
    {
        KagiderContext dbContext = new KagiderContext();
        // GET: Settings
       //  [Authorize]
        public ActionResult Index()
        {
             var listCategory=dbContext.MainCategories.ToList();

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
                                            MCatName=scat.MainCategory.MainCategoryName,
                                            SCatName=scat.SubCategoryName,
                                          //  RCatName = (from k in dbContext.MainCategories where k.MainCategoryId == scat.RelatedId select k.MainCategoryName ).SingleOrDefault()
                                            
                                            RCatName = related == null ? string.Empty : related.MainCategoryName
                                                                                                                                                                                         
                                        }
                                      ).ToList();
             ViewBag.subCatList = subCategories;
         

             return View(new SubCategory { OurCompanyId=1});
        }
        [HttpPost]
        public ActionResult AddSubCategory(SubCategory subCategory)
         {
             dbContext.SubCategories.Add(subCategory);
             dbContext.SaveChanges();

             return RedirectToAction("Index");
         }
    }
}