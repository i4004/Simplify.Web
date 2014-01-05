using System.Web;

using AcspNet;

namespace WebApplication
{
	public class Global : HttpApplication
	{
		protected void Application_Start()
		{
			RouteConfig.RegisterRoutes();
		}
	}
}
