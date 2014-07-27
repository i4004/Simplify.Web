namespace AcspNet
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
		/// <param name="route">The route path.</param>
		/// <param name="method">The HTTP method.</param>
		/// <returns></returns>
		public ControllersHandlerResult Execute(string route, string method)
		{
			var atleastOneControllerMatched = false;

			foreach (var metaData in _agent.GetStandartControllersMetaData())
			{
				var matcherResult = _agent.MatchControllerRoute(metaData, route, method);

				if (matcherResult.Success)
				{
					atleastOneControllerMatched = true;
					var controller = _factory.CreateController(metaData.ControllerType);
					controller.Invoke();
				}
			}

			if (!atleastOneControllerMatched)
			{
				var http404Controller = _agent.GetHandlerController(HandlerControllerType.Http404Handler);

				if (http404Controller == null)
					return ControllersHandlerResult.Http404;
				
				var controller = _factory.CreateController(http404Controller.ControllerType);
				controller.Invoke();
			}

			return ControllersHandlerResult.Ok;
		}
	}
}