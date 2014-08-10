using AcspNet.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Creates and executes controllers for current request
	/// </summary>
	public class ControllersHandler : IControllersHandler
	{
		private readonly IControllersAgent _agent;
		private readonly IControllerFactory _factory;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllersHandler" /> class.
		/// </summary>
		public ControllersHandler(IControllersAgent controllersAgent, IControllerFactory controllerFactory)
		{
			_agent = controllersAgent;
			_factory = controllerFactory;
		}

		/// <summary>
		/// Creates and invokes controllers instances.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="route">The route path.</param>
		/// <param name="method">The HTTP method.</param>
		/// <returns></returns>
		public ControllersHandlerResult Execute(IDIContainerProvider containerProvider, string route, string method)
		{
			var atleastOneControllerMatched = false;

			foreach (var metaData in _agent.GetStandardControllersMetaData())
			{
				var matcherResult = _agent.MatchControllerRoute(metaData, route, method);

				if (matcherResult.Success)
				{
					atleastOneControllerMatched = true;
					var controller = _factory.CreateController(containerProvider, metaData.ControllerType);
					controller.Invoke();
				}
			}

			if (!atleastOneControllerMatched)
			{
				var http404Controller = _agent.GetHandlerController(HandlerControllerType.Http404Handler);

				if (http404Controller == null)
					return ControllersHandlerResult.Http404;

				var controller = _factory.CreateController(containerProvider, http404Controller.ControllerType);
				controller.Invoke();
			}

			return ControllersHandlerResult.Ok;
		}
	}
}