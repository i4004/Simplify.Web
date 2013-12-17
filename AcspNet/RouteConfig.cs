using System.Web.Routing;

namespace AcspNet
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			//routes.Ignore("{file}.js");
			//routes.Ignore("{file}.css");
			//routes.Ignore("{file}.png");
			//routes.Ignore("{file}.gif");
			//routes.Ignore("{file}.jpg");
			//routes.Ignore("{file}.ico");

			routes.MapPageRoute("AcspNetAction", "{action}", "~/" + Manager.DefaultPage);
			routes.MapPageRoute("AcspNetActionID", "{action}/{id}", "~/" + Manager.DefaultPage);
			routes.MapPageRoute("AcspNetActionModeID", "{action}/{mode}/{id}", "~/" + Manager.DefaultPage);
		}
	}
}
