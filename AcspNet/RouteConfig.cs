using System.Web.Routing;

namespace AcspNet
{
	internal class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes, AcspNetSettings settings)
		{
			routes.MapPageRoute("AcspNetAction", "{action}", "~/" + settings.IndexPage);
			routes.MapPageRoute("AcspNetActionID", "{action}/{id}", "~/" + settings.IndexPage);
			routes.MapPageRoute("AcspNetActionModeID", "{action}/{mode}/{id}", "~/" + settings.IndexPage);
		}
	}
}
