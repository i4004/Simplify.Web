using System.Collections.Generic;
using AcspNet.Meta;
using AcspNet.Routing;

namespace AcspNet
{
	/// <summary>
	/// Represent controllers agent
	/// </summary>
	public interface IControllersAgent
	{
		/// <summary>
		/// Gets the standart controllers meta data.
		/// </summary>
		/// <returns></returns>
		IEnumerable<IControllerMetaData> GetStandartControllersMetaData();

		/// <summary>
		/// Matches the controller route.
		/// </summary>
		/// <param name="controllerMetaData">The controller meta data.</param>
		/// <param name="sourceRoute">The source route.</param>
		/// <param name="httpMethod">The HTTP method.</param>
		/// <returns></returns>
		IRouteMatchResult MatchControllerRoute(IControllerMetaData controllerMetaData, string sourceRoute, string httpMethod);
	}
}