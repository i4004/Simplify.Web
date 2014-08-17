using System;
using Microsoft.Owin;
using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Creates and executes controllers for current request
	/// </summary>
	public class ControllersHandler : IControllersHandler
	{
		private readonly IControllersAgent _agent;
		private readonly IControllerFactory _factory;
		private readonly IControllerResponseHandler _controllerResponseHandler;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllersHandler" /> class.
		/// </summary>
		public ControllersHandler(IControllersAgent controllersAgent, IControllerFactory controllerFactory, IControllerResponseHandler controllerResponseHandler)
		{
			_agent = controllersAgent;
			_factory = controllerFactory;
			_controllerResponseHandler = controllerResponseHandler;
		}

		/// <summary>
		/// Creates and invokes controllers instances.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public ControllersHandlerResult Execute(IDIContainerProvider containerProvider, IOwinContext context)
		{
			var atleastOneControllerMatched = false;

			foreach (var metaData in _agent.GetStandardControllersMetaData())
			{
				var matcherResult = _agent.MatchControllerRoute(metaData, context.Request.Path.Value, context.Request.Method);

				if (matcherResult.Success)
				{
					atleastOneControllerMatched = true;
					var result = ProcessController(metaData.ControllerType, containerProvider, context, matcherResult.RouteParameters);

					if(result == ControllerResponseResult.RawOutput)
						return ControllersHandlerResult.RawOutput;
				}
			}

			if (!atleastOneControllerMatched)
			{
				var http404Controller = _agent.GetHandlerController(HandlerControllerType.Http404Handler);

				if (http404Controller == null)
					return ControllersHandlerResult.Http404;

				ProcessController(http404Controller.ControllerType, containerProvider, context);
			}

			return ControllersHandlerResult.Ok;
		}

		private ControllerResponseResult ProcessController(Type controllerType, IDIContainerProvider containerProvider, IOwinContext context, dynamic routeParameters = null)
		{
			var controller = _factory.CreateController(controllerType, containerProvider, context, routeParameters);
			return _controllerResponseHandler.Process(controller.Invoke(), containerProvider);
		}
	}
}