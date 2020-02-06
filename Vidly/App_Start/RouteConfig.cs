using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapMvcAttributeRoutes();

			//create custom routes(but it is an old process as change of action name in moviescontroller doesnt change name here in action. so we use attribute routes.)
			/*routes.MapRoute(
				"MoviesByReleaseDate",
				"movies/released/{year}/{month}",
				new { controller="Movies", action = "ByReleaseDate"},
				new { year = @"\d{4}", month = @"\d{2}"});*/   // d for digit and 4 means number of digit
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
