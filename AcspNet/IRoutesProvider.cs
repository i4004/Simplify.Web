using System.Web.Routing;

namespace AcspNet
{
	public interface IRoutesProvider : IHideObjectMembers
	{
		RouteData GetRouteData();	 
	}
}