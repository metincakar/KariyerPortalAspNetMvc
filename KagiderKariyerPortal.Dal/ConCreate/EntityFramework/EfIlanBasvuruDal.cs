using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KagiderKariyerPortal.Dal.Abstract;
using KagiderKariyerPortal.Entities.ConCreate;

namespace KagiderKariyerPortal.Dal.ConCreate.EntityFramework
{
    public class EfIlanBasvuruDal:IEntityDal<IlanBasvuru>
    {
        KagiderContext _context=new KagiderContext();
        public void Add(IlanBasvuru entity)
        {
            _context.IlanBasvurus.Add(entity);
            _context.SaveChanges();
        }

        public IlanBasvuru Get(IlanBasvuru entity)
        {
           return _context.IlanBasvurus.FirstOrDefault(b => b.BasvuruId == entity.BasvuruId);
        }

        public List<IlanBasvuru> GetList()
        {
            return _context.IlanBasvurus.ToList();
        }

        public void Del(IlanBasvuru entity)
        {
            _context.IlanBasvurus.Remove(entity);
        }

        public void Update(IlanBasvuru entity)
        {
            var updbasvuru = _context.IlanBasvurus.FirstOrDefault(b => b.BasvuruId == entity.BasvuruId);
            if (updbasvuru != null)
            {
                updbasvuru.BasvuruTarih = entity.BasvuruTarih;
                updbasvuru.BasvuruDurum = entity.BasvuruDurum;
                updbasvuru.Onyazi = entity.Onyazi;
            }

            _context.SaveChanges();
        }
    }
}
