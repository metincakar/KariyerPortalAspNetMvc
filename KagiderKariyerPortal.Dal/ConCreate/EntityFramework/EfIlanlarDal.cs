using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KagiderKariyerPortal.Dal.Abstract;
using KagiderKariyerPortal.Entities.ConCreate;

namespace KagiderKariyerPortal.Dal.ConCreate.EntityFramework
{
   public  class EfIlanlarDal:IEntityDal<Ilan>
   {
       KagiderContext _context=new KagiderContext();
       public void Add(Ilan entity)
       {
           entity.IlanStartDate = DateTime.Now;
           entity.IlanFinishDate = DateTime.Now;
           _context.Ilanlar.Add(entity);
           _context.SaveChanges();
       }

       public Ilan Get(Ilan entity)
       {
           return _context.Ilanlar.FirstOrDefault(i => i.IlanId == entity.IlanId);
       }

       public List<Ilan> GetList()
       {
           return _context.Ilanlar.ToList();
       }

       public void Del(Ilan entity)
       {
           _context.Ilanlar.Remove(entity);
           _context.SaveChanges();
       }

       public void Update(Ilan entity)
       {
           var updIlan = _context.Ilanlar.FirstOrDefault(i => i.IlanId == entity.IlanId);

           if (updIlan != null)
           {
               updIlan.AcikPozisyon = entity.AcikPozisyon;
               updIlan.ArananPersonelSayisi = entity.ArananPersonelSayisi;
               updIlan.CityId = entity.CityId;
               updIlan.CountryId = entity.CountryId;
               updIlan.CountyId = entity.CountyId;
               updIlan.Detail = entity.Detail;
               updIlan.EgitimSeviyesi = entity.EgitimSeviyesi;
               updIlan.FirmaId = entity.FirmaId;
               updIlan.IlanFinishDate = entity.IlanFinishDate;
               updIlan.IlanStartDate = entity.IlanStartDate;
               updIlan.IlanTitle = entity.IlanTitle;
               updIlan.PozisyonSeviyesi = entity.PozisyonSeviyesi;
               updIlan.PozisyonTipi = entity.PozisyonTipi;
               updIlan.Tecrube = entity.Tecrube;
               updIlan.UserId = entity.UserId;
               updIlan.ArananPozisyonlar = entity.ArananPozisyonlar;
           }

           _context.SaveChanges();
       }
   }
}
