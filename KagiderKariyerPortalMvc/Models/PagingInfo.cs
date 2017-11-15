using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KagiderKariyerPortalMvc.Models
{
    public class PagingInfo
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int TotalItemCount { get; set; }

        public int CurrentCategory { get; set; }
    }
}