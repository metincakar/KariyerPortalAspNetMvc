using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KagiderKariyerPortal.Dal.Abstract;
using KagiderKariyerPortal.Entities.ConCreate;

namespace KagiderKariyerPortal.Dal.ConCreate.EntityFramework
{
    public class EfCvKisiselBilgiDal:IEntityDal<CvKisiselBilgi>
    {
        KagiderContext _context = new KagiderContext();
        public void Add(CvKisiselBilgi entity)
        {

            _context.CvKisiselBilgis.Add(entity);
            _context.SaveChanges();
        }

        public CvKisiselBilgi Get(CvKisiselBilgi entity)
        {
            return _context.CvKisiselBilgis.FirstOrDefault(i => i.KisiselBilgiId== entity.KisiselBilgiId);
        }

        public List<CvKisiselBilgi> GetList()
        {
            return _context.CvKisiselBilgis.ToList();
        }

        public void Del(CvKisiselBilgi entity)
        {
            throw new NotImplementedException();
        }

        public void Update(CvKisiselBilgi entity)
        {
            var updentity = _context.CvKisiselBilgis.FirstOrDefault(i => i.KisiselBilgiId == entity.KisiselBilgiId);
            updentity.MedeniDurum = entity.MedeniDurum;
            updentity.CityId = entity.CityId;
            updentity.DogumTarihi = entity.DogumTarihi;
            updentity.SurucuBelge = entity.SurucuBelge;
            updentity.SurucuBelgeSinifi = entity.SurucuBelgeSinifi;
            updentity.SurucuBelgeVerilisTarih = entity.SurucuBelgeVerilisTarih;
            updentity.Cinsiyet = entity.Cinsiyet;
            updentity.AskerlikDurum = entity.AskerlikDurum;
            updentity.TcNo = entity.TcNo;
            updentity.CountryId= entity.CountryId;
            _context.SaveChanges();
        }
    }
}
