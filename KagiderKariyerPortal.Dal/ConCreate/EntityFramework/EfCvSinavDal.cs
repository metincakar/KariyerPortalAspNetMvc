using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KagiderKariyerPortal.Dal.Abstract;
using KagiderKariyerPortal.Entities.ConCreate;
using System.Data.Entity;

namespace KagiderKariyerPortal.Dal.ConCreate.EntityFramework
{
    public class EfCvSinavDal:IEntityDal<CvSinavBilgi>
    {
        KagiderContext _context=new KagiderContext();
        public void Add(CvSinavBilgi entity)
        {
            _context.CvSinavBilgis.Add(entity);
            _context.SaveChanges();
        }

        public CvSinavBilgi Get(CvSinavBilgi entity)
        {
            return _context.CvSinavBilgis.FirstOrDefault(c => c.SinavId == entity.SinavId);
        }

        public List<CvSinavBilgi> GetList()
        {
            return _context.CvSinavBilgis.ToList();
        }

        public void Del(CvSinavBilgi entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
                _context.CvSinavBilgis.Attach(entity);
            _context.CvSinavBilgis.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(CvSinavBilgi entity)
        {
            var upd = _context.CvSinavBilgis.FirstOrDefault(c => c.SinavId == entity.SinavId);
            if (upd != null)
            {
                upd.Aciklama = entity.Aciklama;
                upd.SinavAdi = entity.SinavAdi;
                upd.SinavSkor = entity.SinavSkor;
                upd.SinavTarih = entity.SinavTarih;
                upd.SinavTuru = entity.SinavTuru;
                upd.SinavYapanKurulus = entity.SinavYapanKurulus;
                upd.UserId = entity.UserId;
            }
            _context.SaveChanges();
        }
    }
}
