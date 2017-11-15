using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KagiderKariyerPortal.Dal.Abstract;
using KagiderKariyerPortal.Entities.ConCreate;

namespace KagiderKariyerPortal.Dal.ConCreate.EntityFramework
{
    public class EfCvIletisimBilgiDal:IEntityDal<CvIletisimBilgi>
    {
        KagiderContext _context=new KagiderContext();
        public void Add(CvIletisimBilgi entity)
        {
            _context.CvIletisimBilgis.Add(entity);
            _context.SaveChanges();
        }

        public CvIletisimBilgi Get(CvIletisimBilgi entity)
        {
           return _context.CvIletisimBilgis.FirstOrDefault(i => i.IletisimBilgiId == entity.IletisimBilgiId);
        }

        public List<CvIletisimBilgi> GetList()
        {
            return _context.CvIletisimBilgis.ToList();
        }

        public void Del(CvIletisimBilgi entity)
        {
            throw new NotImplementedException();
        }

        public void Update(CvIletisimBilgi entity)
        {
            var updInfo = _context.CvIletisimBilgis.FirstOrDefault(i => i.IletisimBilgiId == entity.IletisimBilgiId);
            updInfo.EvTel = entity.EvTel;
            updInfo.MobilTel = entity.MobilTel;
            updInfo.UyeMail = entity.UyeMail;
            updInfo.Adres = entity.Adres;
            updInfo.CountryId = entity.CountryId;
            updInfo.OzgecmisOzet = entity.OzgecmisOzet;
            updInfo.UyeAd = entity.UyeAd;
            updInfo.UyeSoyad = entity.UyeSoyad;
        
            updInfo.CityId = entity.CityId;
            updInfo.CountyId = entity.CountyId;
            updInfo.Semt = entity.Semt;
            updInfo.WebLink = entity.WebLink;
            _context.SaveChanges();

        }
    }
}
