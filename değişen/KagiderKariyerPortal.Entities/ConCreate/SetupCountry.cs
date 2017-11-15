using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KagiderKariyerPortal.Entities.ConCreate
{
    public class SetupCountry
    {
        [Key]
        public int CountryId { get; set; }
        public string Country { get; set; }
    }
}
