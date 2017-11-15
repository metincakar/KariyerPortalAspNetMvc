using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KagiderKariyerPortal.Entities.ConCreate;

namespace KagiderKariyerPortalMvc.Models
{
    public class JsonResponseFactory
    {
        public static object ErrorResponse(string error)
        {
            return new { Success = false, ErrorMessage = error };
        }

        public static object SuccessResponse()
        {
            return new { Success = true };
        }
        public static object SuccessResponse(object referenceObject, string returnUrl)
        {
            return new { Success = true, Object = referenceObject, returnUrl };
        }
        public static object SuccessResponse(object referenceObject, string action, SetupCountry ulke, SetupCity sehir, SetupCounty ilce)
        {
            return new { Success = true, Object = referenceObject, action, ulke, sehir, ilce };
        }

    }
}