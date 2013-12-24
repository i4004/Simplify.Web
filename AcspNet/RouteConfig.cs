using System.Web.Routing;

namespace AcspNet
{
	internal class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.MapPageRoute("AcspNetAction", "{action}", "~/" + Manager.Settings.IndexPage);
			routes.MapPageRoute("AcspNetActionID", "{action}/{id}", "~/" + Manager.Settings.IndexPage);
			routes.MapPageRoute("AcspNetActionModeID", "{action}/{mode}/{id}", "~/" + Manager.Settings.IndexPage);
		}
	}
}
