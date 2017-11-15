using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KagiderKariyerPortal.Dal.Abstract;
using KagiderKariyerPortal.Entities.ConCreate;

namespace KagiderKariyerPortal.Dal.ConCreate.EntityFramework
{
    public class EfSetupCityDal:IEntityDal<SetupCity>
    {
        KagiderContext _context=new KagiderContext();
        public void Add(SetupCity entity)
        {
            _context.SetupCities.Add(entity);
        }

        public SetupCity Get(SetupCity entity)
        {
            return _context.SetupCities.FirstOrDefault(s => s.CityId == entity.CityId);
        }

        public List<SetupCity> GetList()
        {
            return _context.SetupCities.ToList();
        }

        public void Del(SetupCity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(SetupCity entity)
        {
            throw new NotImplementedException();
        }


       
    }
}
