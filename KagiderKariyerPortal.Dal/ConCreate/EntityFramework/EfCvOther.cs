using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KagiderKariyerPortal.Dal.Abstract;
using KagiderKariyerPortal.Entities.ConCreate;

namespace KagiderKariyerPortal.Dal.ConCreate.EntityFramework
{
    public class EfCvOther:IEntityDal<CvOther>
    {
        KagiderContext _context=new KagiderContext();
        public void Add(CvOther entity)
        {
            _context.CvOthers.Add(entity);
            _context.SaveChanges();
        }

        public CvOther Get(CvOther entity)
        {
           return _context.CvOthers.FirstOrDefault(c=>c.UserId==entity.UserId);
        }

        public List<CvOther> GetList()
        {
            return _context.CvOthers.ToList();
        }

        public void Del(CvOther entity)
        {
            throw new NotImplementedException();
        }

        public void Update(CvOther entity)
        {
            var updateOther = _context.CvOthers.FirstOrDefault(c => c.UserId == entity.UserId);
            if (updateOther != null)
            {

                if (!string.IsNullOrEmpty(entity.OzetBilgi))
                    updateOther.OzetBilgi = entity.OzetBilgi;
                if (!string.IsNullOrEmpty(entity.BilgisayarBilgi))
                    updateOther.BilgisayarBilgi = entity.BilgisayarBilgi;

                if (!string.IsNullOrEmpty(entity.BurslarProjeler))
                updateOther.BurslarProjeler = entity.BurslarProjeler ?? "";

                updateOther.CalismaDurumu = entity.CalismaDurumu ;
                updateOther.CalismakIstedigiAlan = entity.CalismakIstedigiAlan ?? "";
                updateOther.IstenilenPozisyon = entity.IstenilenPozisyon ?? "";
                
                if (!string.IsNullOrEmpty(entity.Hobiler))
                updateOther.Hobiler = entity.Hobiler ?? "";
                if (!string.IsNullOrEmpty(entity.Uyelikler))
                    updateOther.Uyelikler = entity.Uyelikler ?? "";
                if (!string.IsNullOrEmpty(entity.SosyalSorumlulukCalismalari))
                    updateOther.SosyalSorumlulukCalismalari = entity.SosyalSorumlulukCalismalari ?? "";

                if (!string.IsNullOrEmpty(entity.Ref1AdSoyad))
                updateOther.Ref1AdSoyad = entity.Ref1AdSoyad ?? "";
                if (!string.IsNullOrEmpty(entity.Ref1Mail))
                updateOther.Ref1Mail = entity.Ref1Mail ?? "";
                if (!string.IsNullOrEmpty(entity.Ref1Pozisyon))
                updateOther.Ref1Pozisyon = entity.Ref1Pozisyon ?? "";
                if (!string.IsNullOrEmpty(entity.Ref1Tel))
                updateOther.Ref1Tel = entity.Ref1Tel ?? "";
                if (!string.IsNullOrEmpty(entity.Ref2AdSoyad))
                updateOther.Ref2AdSoyad = entity.Ref2AdSoyad ?? "";
                if (!string.IsNullOrEmpty(entity.Ref2Mail))
                updateOther.Ref2Mail = entity.Ref2Mail ?? "";
                if (!string.IsNullOrEmpty(entity.Ref2Pozisyon))
                updateOther.Ref2Pozisyon = entity.Ref2Pozisyon ?? "";
                if (!string.IsNullOrEmpty(entity.Ref2Tel))
                updateOther.Ref2Tel = entity.Ref2Tel ?? "";
            }

            _context.SaveChanges();


        }
    }
}
