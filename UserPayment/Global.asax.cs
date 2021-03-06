﻿using BootstrapSupport;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using UserPayment.Models;

namespace UserPayment
{
    // Примечание: Инструкции по включению классического режима IIS6 или IIS7 
    // см. по ссылке http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutofacConfig.ConfigureContainer();
            //
            AreaRegistration.RegisterAllAreas();
			//
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
			BootstrapBundleConfig.RegisterBundles(BundleTable.Bundles);
			AuthConfig.RegisterAuth();
            //
            Database.SetInitializer(new DBInitializer());
        }
    }
}