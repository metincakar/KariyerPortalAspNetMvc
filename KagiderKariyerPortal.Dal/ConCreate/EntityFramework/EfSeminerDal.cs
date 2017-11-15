using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using KagiderKariyerPortal.Dal.Abstract;
using KagiderKariyerPortal.Entities.ConCreate;

namespace KagiderKariyerPortal.Dal.ConCreate.EntityFramework
{
    public class EfSeminerDal:IEntityDal<CvSeminerler>
    {
        KagiderContext _context=new KagiderContext();
        public void Add(CvSeminerler entity)
        {
            _context.CvSeminerlers.Add(entity);
            _context.SaveChanges();
        }

        public CvSeminerler Get(CvSeminerler entity)
        {
            return _context.CvSeminerlers.FirstOrDefault(c => c.SeminerId == entity.SeminerId);
        }

        public List<CvSeminerler> GetList()
        {
            return _context.CvSeminerlers.ToList();
        }

        public void Del(CvSeminerler entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
                _context.CvSeminerlers.Attach(entity);
            _context.CvSeminerlers.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(CvSeminerler entity)
        {
            var upd = _context.CvSeminerlers.FirstOrDefault(c => c.SeminerId == entity.SeminerId);
            if (upd != null)
            {
                upd.EgitimAd = entity.EgitimAd;
                upd.Aciklama = entity.Aciklama;
                upd.EgitimBaslangic = entity.EgitimBaslangic;
                upd.EgitimBitis = entity.EgitimBitis;
                upd.EgitimKurumu = entity.EgitimKurumu;
                upd.EgitimSure = entity.EgitimSure;
                upd.UserId = entity.UserId;
            }
            _context.SaveChanges();
        }
    }
}
