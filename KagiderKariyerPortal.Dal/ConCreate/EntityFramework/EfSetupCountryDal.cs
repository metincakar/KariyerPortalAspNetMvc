using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KagiderKariyerPortal.Dal.Abstract;
using KagiderKariyerPortal.Entities.ConCreate;

namespace KagiderKariyerPortal.Dal.ConCreate.EntityFramework
{
    public class EfSetupCountryDal:IEntityDal<SetupCountry>
    {
        KagiderContext _context=new KagiderContext();
        public void Add(SetupCountry entity)
        {
            _context.SetupCountries.Add(entity);
        }

        public SetupCountry Get(SetupCountry entity)
        {
            return _context.SetupCountries.FirstOrDefault(s => s.CountryId == entity.CountryId);
        }

        public List<SetupCountry> GetList()
        {
            return _context.SetupCountries.ToList();
        }

        public void Del(SetupCountry entity)
        {
            throw new NotImplementedException();
        }

        public void Update(SetupCountry entity)
        {
            throw new NotImplementedException();
        }
    }
}
