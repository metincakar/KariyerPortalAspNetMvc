using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web;
using System.Web.Mvc;
using KagiderKariyerPortalMvc.Models;


namespace KagiderKariyerPortalMvc.HtmlHelpers
{
    //sınıf static olmalı
    public static class PagingHelpers
    {
        //metod static olmalı
        //ilk parametre neyi extend ettiğimiz bilgisini içermeli
        public static MvcHtmlString Pager(this HtmlHelper html, PagingInfo pagingInfo)
        {
            int totalPageCount = (int) Math.Ceiling((decimal)
                pagingInfo.TotalItemCount/pagingInfo.PageSize);

            var stringBuilder=new StringBuilder();

            for (int i = 1; i <= totalPageCount; i++)
            {
                var tagbuilder = new TagBuilder("a");
                tagbuilder.MergeAttribute("href",String.Format("/Ilan/Index/?page={0}&id={1}",i,pagingInfo.CurrentCategory));
                tagbuilder.InnerHtml = i.ToString();

                if (pagingInfo.CurrentPage == i)
                {
                    tagbuilder.AddCssClass("selected");
                }

                stringBuilder.Append(tagbuilder);
            }
            return MvcHtmlString.Create(stringBuilder.ToString());
        }
       
    }
}