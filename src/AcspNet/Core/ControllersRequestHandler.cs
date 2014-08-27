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
		private readonly IControllerExecutor _controllerExecutor;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllersRequestHandler" /> class.
		/// </summary>
		/// <param name="controllersAgent">The controllers agent.</param>
		/// <param name="controllerExecutor">The controller executor.</param>
		public ControllersRequestHandler(IControllersAgent controllersAgent, IControllerExecutor controllerExecutor)
		{
			_agent = controllersAgent;
			_controllerExecutor = controllerExecutor;
		}

		/// <summary>
		/// Process HTTP request to controllers
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public ControllersHandlerResult ProcessRequest(IDIContainerProvider containerProvider, IOwinContext context)
		{
			var atleastOneNonAnyPageControllerMatched = false;

			foreach (var metaData in _agent.GetStandardControllersMetaData())
			{
				var matcherResult = _agent.MatchControllerRoute(metaData, context.Request.Path.Value, context.Request.Method);

				if (!matcherResult.Success) continue;

				var securityResult = _agent.IsSecurityRulesViolated(metaData, context.Authentication.User);

				if(securityResult == SecurityRuleCheckResult.NotAuthenticated)
					return ControllersHandlerResult.Http401;

				if(securityResult == SecurityRuleCheckResult.Forbidden)
					return ProcessForbiddenSecurityRule(containerProvider, context);

				var result = _controllerExecutor.Execute(metaData.ControllerType, containerProvider, context, matcherResult.RouteParameters);

				if (result == ControllerResponseResult.RawOutput)
					return ControllersHandlerResult.RawOutput;

				if(result == ControllerResponseResult.Redirect)
					return ControllersHandlerResult.Redirect;

				if (!_agent.IsAnyPageController(metaData))
					atleastOneNonAnyPageControllerMatched = true;
			}

			if (!atleastOneNonAnyPageControllerMatched)
			{
				var result = ProcessOnlyAnyPageControllersMatched(containerProvider, context);
				if (result != ControllersHandlerResult.Ok)
					return result;
			}

			return ProcessAsyncControllersResponses(containerProvider);
		}

		private ControllersHandlerResult ProcessOnlyAnyPageControllersMatched(IDIContainerProvider containerProvider, IOwinContext context)
		{
			var http404Controller = _agent.GetHandlerController(HandlerControllerType.Http404Handler);

			if (http404Controller == null)
				return ControllersHandlerResult.Http404;

			var handlerControllerResult = _controllerExecutor.Execute(http404Controller.ControllerType, containerProvider, context);

			if (handlerControllerResult == ControllerResponseResult.RawOutput)
				return ControllersHandlerResult.RawOutput;

			if (handlerControllerResult == ControllerResponseResult.Redirect)
				return ControllersHandlerResult.Redirect;
			
			return ControllersHandlerResult.Ok;
		}

		private ControllersHandlerResult ProcessAsyncControllersResponses(IDIContainerProvider containerProvider)
		{
			foreach (var controllerResponseResult in _controllerExecutor.ProcessAsyncControllersResponses(containerProvider))
			{
				if (controllerResponseResult == ControllerResponseResult.RawOutput)
					return ControllersHandlerResult.RawOutput;

				if (controllerResponseResult == ControllerResponseResult.Redirect)
					return ControllersHandlerResult.Redirect;
			}

			return ControllersHandlerResult.Ok;
		}

		private ControllersHandlerResult ProcessForbiddenSecurityRule(IDIContainerProvider containerProvider, IOwinContext context)
		{
			var http403Controller = _agent.GetHandlerController(HandlerControllerType.Http403Handler);

			if (http403Controller == null)
				return ControllersHandlerResult.Http403;

			var handlerControllerResult = _controllerExecutor.Execute(http403Controller.ControllerType, containerProvider, context);

			if (handlerControllerResult == ControllerResponseResult.RawOutput)
				return ControllersHandlerResult.RawOutput;

			if (handlerControllerResult == ControllerResponseResult.Redirect)
				return ControllersHandlerResult.Redirect;

			return ControllersHandlerResult.Ok;
		}
	}
}