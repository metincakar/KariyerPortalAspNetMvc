using System.Data.Entity;
using KagiderKariyerPortal.Dal.Abstract;
using KagiderKariyerPortal.Entities.ConCreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KagiderKariyerPortal.Dal.ConCreate.EntityFramework
{
    public class EfCvEgitimDal:IEntityDal<CvEgitimUniversiteBilgi>
    {
        KagiderContext _context = new KagiderContext();
        public void Add(CvEgitimUniversiteBilgi entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            _context.CvEgitimUniversiteBilgis.Add(entity);
            _context.SaveChanges();
        }

        public CvEgitimUniversiteBilgi Get(CvEgitimUniversiteBilgi entity)
        {
           return _context.CvEgitimUniversiteBilgis.FirstOrDefault(e => e.EgitimId == entity.EgitimId);
        }

        public List<CvEgitimUniversiteBilgi> GetList()
        {
            return _context.CvEgitimUniversiteBilgis.ToList();
        }

        public void Del(CvEgitimUniversiteBilgi entity)
        {
           
            
            _context.Entry(entity).State = EntityState.Deleted;
            _context.CvEgitimUniversiteBilgis.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(CvEgitimUniversiteBilgi entity)
        {
            var updentity = _context.CvEgitimUniversiteBilgis.FirstOrDefault(e => e.EgitimId == entity.EgitimId);
            updentity.Bolum = entity.Bolum;
            updentity.MezuniyetDerece = entity.MezuniyetDerece;
            updentity.MezuniyetYil = entity.MezuniyetYil;
            updentity.UniversiteAd = entity.UniversiteAd;
            updentity.UniversiteId = entity.UniversiteId;
            updentity.UserId = entity.UserId;
            _context.SaveChanges();

        }
    }
}
