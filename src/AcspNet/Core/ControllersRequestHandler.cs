using Microsoft.Owin;
using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Handler HTTP request for controllers
	/// </summary>
	public class ControllersRequestHandler : IControllersRequestHandler
	{
		private readonly IControllersAgent _agent;
		private readonly IControllersProcessor _controllersProcessor;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllersRequestHandler" /> class.
		/// </summary>
		/// <param name="controllersAgent">The controllers agent.</param>
		/// <param name="controllersProcessor">The controllers processor.</param>
		public ControllersRequestHandler(IControllersAgent controllersAgent, IControllersProcessor controllersProcessor)
		{
			_agent = controllersAgent;
			_controllersProcessor = controllersProcessor;
		}

		/// <summary>
		/// Creates and invokes controllers instances.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public ControllersHandlerResult Execute(IDIContainerProvider containerProvider, IOwinContext context)
		{
			var atleastOneNonAnyPageControllerMatched = false;

			foreach (var metaData in _agent.GetStandardControllersMetaData())
			{
				var matcherResult = _agent.MatchControllerRoute(metaData, context.Request.Path.Value, context.Request.Method);

				if (!matcherResult.Success) continue;

				var result = _controllersProcessor.Process(metaData.ControllerType, containerProvider, context, matcherResult.RouteParameters);

				if (result == ControllerResponseResult.RawOutput)
					return ControllersHandlerResult.RawOutput;

				if(result == ControllerResponseResult.Redirect)
					return ControllersHandlerResult.Redirect;

				if (!_agent.IsAnyPageController(metaData))
					atleastOneNonAnyPageControllerMatched = true;
			}

			if (!atleastOneNonAnyPageControllerMatched)
			{
				var http404Controller = _agent.GetHandlerController(HandlerControllerType.Http404Handler);

				if (http404Controller == null)
					return ControllersHandlerResult.Http404;

				var handlerControllerResult = _controllersProcessor.Process(http404Controller.ControllerType, containerProvider, context);

				if (handlerControllerResult == ControllerResponseResult.RawOutput)
					return ControllersHandlerResult.RawOutput;

				if (handlerControllerResult == ControllerResponseResult.Redirect)
					return ControllersHandlerResult.Redirect;
			}

			foreach (var controllerResponseResult in _controllersProcessor.ProcessAsyncControllersResponses(containerProvider))
			{
				if (controllerResponseResult == ControllerResponseResult.RawOutput)
					return ControllersHandlerResult.RawOutput;

				if (controllerResponseResult == ControllerResponseResult.Redirect)
					return ControllersHandlerResult.Redirect;
			}		

			return ControllersHandlerResult.Ok;
		}
	}
}