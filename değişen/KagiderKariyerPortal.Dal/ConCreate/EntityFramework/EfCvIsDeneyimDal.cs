using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KagiderKariyerPortal.Dal.Abstract;
using KagiderKariyerPortal.Entities.ConCreate;

namespace KagiderKariyerPortal.Dal.ConCreate.EntityFramework
{
   public class EfCvIsDeneyimDal:IEntityDal<CvIsDeneyimleri>
   {
       KagiderContext _context=new KagiderContext();
       public void Add(CvIsDeneyimleri entity)
       {
           entity.BaslangicTarihi = DateTime.Now;
           entity.BitisTarihi = DateTime.Now;
           _context.CvIsDeneyimleris.Add(entity);
           _context.SaveChanges();
       }

       public CvIsDeneyimleri Get(CvIsDeneyimleri entity)
       {
           return _context.CvIsDeneyimleris.FirstOrDefault(i => i.IsDeneyimleriId == entity.IsDeneyimleriId);
       }

       public List<CvIsDeneyimleri> GetList()
       {
           return _context.CvIsDeneyimleris.ToList();
       }

       public void Del(CvIsDeneyimleri entity)
       {
           _context.CvIsDeneyimleris.Remove(entity);
           _context.SaveChanges();
       }

       public void Update(CvIsDeneyimleri entity)
       {
         CvIsDeneyimleri updIsDeneyim=_context.CvIsDeneyimleris.FirstOrDefault(i => i.IsDeneyimleriId == entity.IsDeneyimleriId);
           updIsDeneyim.IsAlani = entity.IsAlani;
           updIsDeneyim.Istanimi = entity.Istanimi;
           updIsDeneyim.FirmadakiPozisyon = entity.FirmadakiPozisyon;
           updIsDeneyim.FirmaSektoru = entity.FirmaSektoru;
           updIsDeneyim.FirmaAdi = entity.FirmaAdi;
           updIsDeneyim.BitisTarihi = entity.BitisTarihi;
           updIsDeneyim.BaslangicTarihi = entity.BaslangicTarihi;
           _context.SaveChanges();
       }
   }
}
