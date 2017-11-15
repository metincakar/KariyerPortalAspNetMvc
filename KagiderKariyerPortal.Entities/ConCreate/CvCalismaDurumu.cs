using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KagiderKariyerPortal.Entities.ConCreate
{
    class CvCalismaDurumu
    {
        public int UyeId { get; set; }

        public bool CalismaDurumu { get; set; }

        public string CalismakIstedigiSektor { get; set; }
        public string CalismakIstedigiAlan { get; set; }
    }
}
