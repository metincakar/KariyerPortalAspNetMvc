using System.Data.Entity;
using KagiderKariyerPortal.Dal.Abstract;
using KagiderKariyerPortal.Entities.ConCreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KagiderKariyerPortal.Dal.ConCreate.EntityFramework
{
    public class EfCvDilBilgiDal:IEntityDal<CvDilBilgi>
    {
        KagiderContext _context = new KagiderContext();
        public void Add(CvDilBilgi entity)
        {
            _context.CvDilBilgis.Add(entity);
            _context.SaveChanges();
        }

        public CvDilBilgi Get(CvDilBilgi entity)
        {
          return  _context.CvDilBilgis.FirstOrDefault(d => d.DilBilgId == entity.DilBilgId);
        }

        public List<CvDilBilgi> GetList()
        {
            return _context.CvDilBilgis.ToList();
        }

        public void Del(CvDilBilgi entity)
        {
         
            var entry=_context.Entry(entity);
            if (entry.State == EntityState.Detached)
                _context.CvDilBilgis.Attach(entity);
            _context.CvDilBilgis.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(CvDilBilgi entity)
        {
            var updentity = _context.CvDilBilgis.FirstOrDefault(d => d.DilBilgId == entity.DilBilgId);
            if (updentity != null)
            {
                updentity.Dil = entity.Dil;
                updentity.Okuma = entity.Okuma;
                updentity.Konusma = entity.Konusma;
                updentity.Yazma = entity.Yazma;
                updentity.UserId = entity.UserId;
            }
            _context.SaveChanges();
        }
    }
}
