using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KagiderKariyerPortal.Entities.ConCreate
{
    public class SetupCounty
    {
        [Key]
        public int CountyId { get; set; }
        public string County { get; set; }

        public int CityId { get; set; }

        //public SetupCity SetupCity { get; set; }
    }
}
