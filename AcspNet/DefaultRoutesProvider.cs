using System.Web;
using System.Web.Routing;

namespace AcspNet
{
	public class DefaultRoutesProvider : IRoutesProvider
	{
		public RouteData GetRouteData()
		{
			return RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));
		}
	}
}