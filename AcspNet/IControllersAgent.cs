using AcspNet.Routing;

namespace AcspNet
{
	public interface IControllersAgent
	{
		/// <summary>
		/// Matches the controller route.
		/// </summary>
		/// <param name="sourceRoute">The source route.</param>
		/// <param name="controllerRoute">The controller route.</param>
		/// <param name="httpMethod">The HTTP method.</param>
		/// <returns></returns>
		IRouteMatchResult MatchControllerRoute(string sourceRoute, string controllerRoute, string httpMethod);
	}
}