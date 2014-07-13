using System.Collections.Generic;
using System.Linq;
using AcspNet.Meta;
using AcspNet.Routing;

namespace AcspNet
{
	public class ControllersAgent : IControllersAgent
	{
		private readonly IList<IControllerMetaData> _controllersMetaData;

		public ControllersAgent(IControllersMetaStore controllersMetaStore)
		{
			_controllersMetaData = controllersMetaStore.GetControllersMetaData();
		}

		public IEnumerable<IControllerMetaData> GetStandartControllersMetaData()
		{
			return _controllersMetaData.Where(
				x =>
					x.Role == null || (x.Role.Is400Handler == false && x.Role.Is403Handler == false && x.Role.Is404Handler == false));
		}

		public IRouteMatchResult MatchControllerRoute(string sourceRoute, string controllerRoute, string httpMethod)
		{
			throw new System.NotImplementedException();
		}
	}
}