using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KagiderKariyerPortal.Entities.ConCreate;

namespace KagiderKariyerPortalMvc.Models
{
    public class TumIlanViewModel
    {
        public PagingInfo PagingInfo { get; set; }

        public List<Ilan> IlanList { get; set; }

        public IlanSearchModel IlanSearchModel { get; set; }
    }
}