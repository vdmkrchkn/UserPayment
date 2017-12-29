using NavigationRoutes;
using System.Web.Mvc;
using System.Web.Routing;

namespace UserPayment
{
	public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			//NavigationRouteFilters.Filters.Add(new AdministrationRouteFilter());

			routes.MapNavigationRoute("Home", "Home", "", new { controller = "Home", action = "Index" });
				//new NavigationRouteOptions { HasBreakAfter = true });

			routes.MapNavigationRoute("Users", "Пользователи", "Users", new { controller = "Users", action = "Index" });
			routes.MapNavigationRoute("Wallets", "Кошельки", "Wallets", new { controller = "Wallets", action = "Index" });
			routes.MapNavigationRoute("Accounts", "Счета", "Accounts", new { controller = "Accounts", action = "Index" });
			routes.MapNavigationRoute("About", "О программе", "About", new { controller = "Home", action = "About" });
			routes.MapNavigationRoute("Contacts", "Контакты", "Contact", new { controller = "Home", action = "Contact" });
			
			// this route will only show if users are in the role specified in the AdministrationRouteFilter
			// by default, when you run the site, you will not see this. Explore the AdministrationRouteFilter
			// class for more information.

			//routes.MapNavigationRoute<HomeController>("Administration Menu", c => c.Admin(), "",
			//										  new NavigationRouteOptions { HasBreakAfter = true, FilterToken = "admin" });

			//routes.MapNavigationRoute<ExampleLayoutsController>("Example Layouts", c => c.Starter())
			//	  .AddChildRoute<ExampleLayoutsController>("Marketing", c => c.Marketing())
			//	  .AddChildRoute<ExampleLayoutsController>("Fluid", c => c.Fluid(), new NavigationRouteOptions { HasBreakAfter = true })
			//	  .AddChildRoute<ExampleLayoutsController>("Sign In", c => c.SignIn())
			//	;

			routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Wallets", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}