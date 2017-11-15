using System;
using System.Collections.Generic;

using System.Data.Entity;
using System.Data.Entity.Migrations.Model;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using KagiderKariyerPortal.Dal.ConCreate.EntityFramework;
using KagiderKariyerPortal.Dal.Migrations;
using WebMatrix.WebData;

namespace KagiderKariyerPortalMvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
;
           //Database.SetInitializer(new DropCreateDatabaseAlways<KagiderContext>());
           // Database.SetInitializer(new MigrateDatabaseToLatestVersion<KagiderContext, Configuration>());
            Database.SetInitializer(new CreateDatabaseIfNotExists<KagiderContext>());
           
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            ViewEngines.Engines.Clear(); //performansı artırmak için eklendi 
            ViewEngines.Engines.Add(new RazorViewEngine());
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }
        public class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                using (var context = new KagiderContext())
                    context.Users.Find(1);

                if (!WebSecurity.Initialized)
                    WebSecurity.InitializeDatabaseConnection("KagiderContext", "Users", "UserId", "UserName", autoCreateTables: true);
            }
        }
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            CultureInfo newCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            newCulture.DateTimeFormat.DateSeparator = "/";
            
            Thread.CurrentThread.CurrentCulture = newCulture;
        }
    }
}