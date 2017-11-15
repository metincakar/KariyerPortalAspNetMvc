using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace KagiderKariyerPortal.Entities.ConCreate
{
    public class SetupCity
    {
        [Key]
        public int CityId { get; set; }
        public string City { get; set; }

        public int CountryId { get; set; }

        //public List<SetupCounty> SetupCounties { get; set; }

       
    }
  
}
