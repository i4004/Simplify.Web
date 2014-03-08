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
		public static void RegisterRoutes(string indexPageName)
		{
			RouteTable.Routes.MapPageRoute("AcspNetAction", "{action}", "~/" + indexPageName);
			RouteTable.Routes.MapPageRoute("AcspNetActionID", "{action}/{id}", "~/" + indexPageName);
			RouteTable.Routes.MapPageRoute("AcspNetActionModeID", "{action}/{mode}/{id}", "~/" + indexPageName);
		}
	}
}
