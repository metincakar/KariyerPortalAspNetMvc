using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KagiderKariyerPortal.Dal.ConCreate.EntityFramework;

namespace KagiderKariyerPortalMvc.Models
{
     
    public class CvItemModel
    {
        private KagiderContext context;
        public List<DilSurec> Dilsureclist { get; set; }
        public List<Dil> Diller { get; set; }
      
        public List<Askerlik> AskerlikDurumList { get; set; }

        public List<SurucuSinif> SurucuSinifList { get; set; }
        public List<SelectListItem> ListSinavTurleri { get; set; }

        public List<SelectListItem> ListPozisyonlar { get; set; }

        public List<SelectListItem> ListUniversiteler { get; set; }

        public List<SelectListItem> ListSektorler { get; set; }

        public List<SelectListItem> ListDepartmanlar { get; set; }

        public List<SelectListItem> ListPozisyontip { get; set; }

        public List<SelectListItem> ListPozisyonseviye { get; set; }
        public CvItemModel()
        {
            context=new KagiderContext();
            var listSubKategories = (from m in context.MainCategories.ToList()
                join s in context.SubCategories.ToList() on m.MainCategoryId equals s.MainCategoryId
                select
                    new KategoriModel
                    {
                        KategoriId = m.MainCategoryId,
                        KategoriAd = m.MainCategoryName,
                        AltKategoriId = s.SubCategoryId,
                        AltKategoriDegeri = s.SubCategoryName
                    }
                ).ToList();
            ;

                 ListSinavTurleri=
                 (from d in listSubKategories.Where(d => d.KategoriId == (int)EnumCategories.SınavBilgileri)
                  select new SelectListItem()
                  {
                      Text = d.AltKategoriDegeri,
                      Value = d.AltKategoriId.ToString()
                  }).ToList();
                ListPozisyonlar = (from d in listSubKategories.Where(d => d.KategoriId == (int)EnumCategories.Pozisyon)
                                   select new SelectListItem()
                                   {
                                       Text = d.AltKategoriDegeri,
                                       Value = d.AltKategoriId.ToString()
                                   }).ToList();
                ListUniversiteler = (from d in listSubKategories.Where(d => d.KategoriId == (int)EnumCategories.Üniversite)
                                     select new SelectListItem()
                                     {
                                         Text = d.AltKategoriDegeri,
                                         Value = d.AltKategoriId.ToString()
                                     }).ToList();
                ListSektorler= (from d in listSubKategories.Where(d => d.KategoriId == (int)EnumCategories.Sektör)
                                     select new SelectListItem()
                                     {
                                         Text = d.AltKategoriDegeri,
                                         Value = d.AltKategoriId.ToString()
                                     }).ToList();
                ListDepartmanlar = (from d in listSubKategories.Where(d => d.KategoriId == (int)EnumCategories.Departmanlar)
                                    select new SelectListItem()
                                    {
                                        Text = d.AltKategoriDegeri,
                                        Value = d.AltKategoriId.ToString()
                                    }).ToList();

            Dilsureclist = new List<DilSurec> 
            {
                 new DilSurec(){SurecId="Temel",Surec="Temel"},
                 new DilSurec(){SurecId="Başlangıç",Surec="Başlangıç"},
                 new DilSurec(){SurecId="Orta",Surec="Orta"},
                 new DilSurec(){SurecId="İyi",Surec="İyi"},
                 new DilSurec(){SurecId="İleri",Surec="İleri"}
            };
            ListPozisyonseviye = new List<SelectListItem>()
            {
                new SelectListItem() {Value = "0", Text = "Stajyer"},
                new SelectListItem() {Value = "1", Text = "Uzman"},
                new SelectListItem() {Value = "2", Text = "UzmanYardımcısi"},
                new SelectListItem(){Value = "3",Text = "Diğer"}
            };
            ListPozisyontip=new List<SelectListItem>()
            {
                new SelectListItem(){Value = "0",Text = "Sürekli"},
                new SelectListItem(){Value = "1",Text = "Tam Zamanlı"},
                new SelectListItem(){Value = "2",Text = "Yarı Zamanlı"}
            };
            //Diller = new List<Dil>
            //{
            //    new Dil(){DilId = "Almanca",YDil="Almanca"},
            //    new Dil(){DilId = "Fransızca",YDil="Fransızca"},
            //    new Dil(){DilId = "İngilizce",YDil="İngilizce"},
            //    new Dil(){DilId = "İspanyolca",YDil="İspanyolca"}
            //};
            Diller = (from d in listSubKategories.Where(d => d.KategoriId == (int)EnumCategories.YabancıDil)
                select new Dil()
                {
                    DilId = d.AltKategoriId.ToString(),
                    YDil = d.AltKategoriDegeri
                }).ToList();
         
            AskerlikDurumList =new List<Askerlik>
            {
                new Askerlik(){DurumId="Yapıldı",Durum="Yapıldı"},
                new Askerlik(){DurumId="Yapılmadı",Durum="Yapılmadı"},
                new Askerlik(){DurumId="Muaf",Durum="Muaf"},
            };
            SurucuSinifList=new List<SurucuSinif>
            {
                new SurucuSinif{SinifId="A1",Sinif="A1"},
                new SurucuSinif{SinifId="A2",Sinif="A2"},

                new SurucuSinif{SinifId="B",Sinif="B"},
                new SurucuSinif{SinifId="C",Sinif="C"},
                new SurucuSinif{SinifId="D",Sinif="D"},
                new SurucuSinif{SinifId="E",Sinif="E"},
                new SurucuSinif{SinifId="F",Sinif="F"},
                new SurucuSinif{SinifId="H",Sinif="H"},
                new SurucuSinif{SinifId="G",Sinif="G"},
                new SurucuSinif{SinifId="K",Sinif="K"},
                  new SurucuSinif{SinifId="Uluslararası Sürücü Belgesi",Sinif="Uluslararası Sürücü Belgesi"},
            };
        }
    }

    public class SurucuSinif
    {
        public string SinifId { get; set; }
        public string Sinif { get; set; }
    }

    public class Askerlik
    {
        [Key]
        public string DurumId { get; set; }
        public string Durum { get; set; }
    }

    public class DilSurec
    {
        [Key]
        public string SurecId { get; set; }
        public string Surec { get; set; }
    }

    public class Dil
    {
        public string DilId { get; set; }
        public string YDil { get; set; }
    }

}