using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KagiderKariyerPortal.Dal.Abstract;
using KagiderKariyerPortal.Entities.ConCreate;

namespace KagiderKariyerPortal.Dal.ConCreate.EntityFramework
{
    public class EfSetupCountyDal:IEntityDal<SetupCounty>
    {
        readonly KagiderContext _context=new KagiderContext();
        public void Add(SetupCounty entity)
        {
            _context.SetupCounties.Add(entity);
        }

        public SetupCounty Get(SetupCounty entity)
        {
            return _context.SetupCounties.FirstOrDefault(s => s.CountyId == entity.CountyId);
        }

        public List<SetupCounty> GetList()
        {
            return _context.SetupCounties.ToList();
        }

        public void Del(SetupCounty entity)
        {
            throw new NotImplementedException();
        }

        public void Update(SetupCounty entity)
        {
            throw new NotImplementedException();
        }
    }
}
