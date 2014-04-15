using System.Web.Routing;

namespace AcspNet
{
	/// <summary>
	/// Class for registering the AcspNet routes in ASP.NET routes collection.
	/// </summary>
	public class AcspRouteConfig
	{
		/// <summary>
		/// Registers the AcspNet routes in ASP.NET routes collection.
		/// </summary>
		/// 
		public static void RegisterRoutes()
		{
			RouteTable.Routes.MapPageRoute("AcspNetAction", "{action}", "~/");
			RouteTable.Routes.MapPageRoute("AcspNetActionID", "{action}/{id}", "~/");
			RouteTable.Routes.MapPageRoute("AcspNetActionModeID", "{action}/{mode}/{id}", "~/");
		}
	}
}
