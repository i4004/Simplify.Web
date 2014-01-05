using System.Web.Routing;

namespace AcspNet
{
	/// <summary>
	/// Class for registering the AcspNet routes in ASP.NET routes collection.
	/// </summary>
	public class RouteConfig
	{
		internal static void RegisterRoutes(RouteCollection routes, AcspNetSettings settings)
		{
			routes.MapPageRoute("AcspNetAction", "{action}", "~/" + settings.IndexPage);
			routes.MapPageRoute("AcspNetActionID", "{action}/{id}", "~/" + settings.IndexPage);
			routes.MapPageRoute("AcspNetActionModeID", "{action}/{mode}/{id}", "~/" + settings.IndexPage);
		}

		/// <summary>
		/// Registers the AcspNet routes in ASP.NET routes collection.
		/// </summary>
		public static void RegisterRoutes()
		{
			RegisterRoutes(RouteTable.Routes, Manager.AcspNetSettings);
		}
	}
}
