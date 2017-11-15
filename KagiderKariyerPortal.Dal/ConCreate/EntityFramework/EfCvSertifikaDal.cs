using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using KagiderKariyerPortal.Dal.Abstract;
using KagiderKariyerPortal.Entities.ConCreate;

namespace KagiderKariyerPortal.Dal.ConCreate.EntityFramework
{
  public  class EfCvSertifikaDal:IEntityDal<CvSertifikaBilgi>
    {
        KagiderContext _context=new KagiderContext();
        public void Add(CvSertifikaBilgi entity)
        {
            _context.CvSertifikaBilgis.Add(entity);
            _context.SaveChanges();
        }

        public CvSertifikaBilgi Get(CvSertifikaBilgi entity)
        {
            return _context.CvSertifikaBilgis.FirstOrDefault(s=>s.SertifikaId==entity.SertifikaId);
        }

        public List<CvSertifikaBilgi> GetList()
        {
            return _context.CvSertifikaBilgis.ToList();
        }

        public void Del(CvSertifikaBilgi entity)
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
                _context.CvSertifikaBilgis.Attach(entity);
            _context.CvSertifikaBilgis.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(CvSertifikaBilgi entity)
        {
            var updSertifika = _context.CvSertifikaBilgis.FirstOrDefault(s => s.SertifikaId == entity.SertifikaId);
            if (updSertifika != null)
            {
                updSertifika.SertifikaAd = entity.SertifikaAd;
                updSertifika.SertifikaAy = entity.SertifikaAy;
                updSertifika.SertifikaSene= entity.SertifikaSene;
                updSertifika.AlindigiKurum = entity.AlindigiKurum;
                updSertifika.Aciklama = entity.Aciklama;
                updSertifika.UserId = entity.UserId;
            }
            _context.SaveChanges();
        }
    }
}
