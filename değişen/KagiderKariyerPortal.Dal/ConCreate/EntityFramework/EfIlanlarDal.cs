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
           throw new NotImplementedException();
       }
   }
}
