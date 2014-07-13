using System.Collections.Generic;
using System.Linq;
using AcspNet.Meta;
using AcspNet.Routing;

namespace AcspNet
{
	/// <summary>
	/// Provides controllers agent
	/// </summary>
	public class ControllersAgent : IControllersAgent
	{
		private readonly IList<IControllerMetaData> _controllersMetaData;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllersAgent"/> class.
		/// </summary>
		/// <param name="controllersMetaStore">The controllers meta store.</param>
		public ControllersAgent(IControllersMetaStore controllersMetaStore)
		{
			_controllersMetaData = controllersMetaStore.GetControllersMetaData();
		}

		/// <summary>
		/// Gets the standart controllers meta data.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<IControllerMetaData> GetStandartControllersMetaData()
		{
			return _controllersMetaData.Where(
				x =>
					x.Role == null || (x.Role.Is400Handler == false && x.Role.Is403Handler == false && x.Role.Is404Handler == false));
		}

		/// <summary>
		/// Matches the controller route.
		/// </summary>
		/// <param name="controllerMetaData">The controller meta data.</param>
		/// <param name="sourceRoute">The source route.</param>
		/// <param name="controllerRoute">The controller route.</param>
		/// <param name="httpMethod">The HTTP method.</param>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public IRouteMatchResult MatchControllerRoute(IControllerMetaData controllerMetaData, string sourceRoute, string controllerRoute, string httpMethod)
		{
			throw new System.NotImplementedException();
		}
	}
}