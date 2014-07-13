using System;

namespace AcspNet
{
	/// <summary>
	/// Creates and executes controllers for current request
	/// </summary>
	public class ControllersHandler : IControllersHandler
	{
		private readonly IControllersAgent _controllersAgent;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllersHandler" /> class.
		/// </summary>
		public ControllersHandler(IControllersAgent controllersAgent)
		{
			_controllersAgent = controllersAgent;
		}

		/// <summary>
		/// Creates and invokes controllers instances.
		/// </summary>
		/// <param name="route">The route path.</param>
		/// <param name="method">The HTTP method.</param>
		/// <returns></returns>
		public ControllersHandlerResult Execute(string route, string method)
		{
			//var isAnyNonAnyPageControllerCalled = false;
			//var securityControllerCalled = false;

			//foreach (var metaContainer in GetStandartControllersMetaData())
			//{
				//var matcherResult = _routeMatcher.Match(route, metaContainer.)
			//	var isNonAnyPageController = false;

			//	if (!securityControllerCalled)
			//	{
			//		var securityCheckResult = _executionAgent.IsSecurityRulesViolated(metaContainer);

			//		if (securityCheckResult == SecurityViolationResult.RequestTypeViolated)
			//		{
			//			var result = ExecuteHandlerController(HandlerControllerType.Http400Handler);

			//			if (!result)
			//				return ControllersHandlerResult.Http400;

			//			securityControllerCalled = true;
			//		}

			//		if (securityCheckResult == SecurityViolationResult.AuthenticationRequired)
			//		{
			//			var result = ExecuteHandlerController(HandlerControllerType.Http403Handler);

			//			if (!result)
			//				return ControllersHandlerResult.Http403;

			//			securityControllerCalled = true;
			//		}
			//	}

			//	if (_executionAgent.IsNonAnyPageController(metaContainer))
			//		isNonAnyPageController = true;

			//	if (securityControllerCalled && isNonAnyPageController)
			//		continue;

			//	var controller = _controllerFactory.CreateController(metaContainer.ControllerType);
			//	controller.Invoke();

			//	if (metaContainer.ExecParameters != null && metaContainer.ExecParameters.IsAjax)
			//	{
			//		AjaxResult = controller.AjaxResult;
			//		return ControllersHandlerResult.AjaxRequest;
			//	}

			//	if (controller.StopExecution)
			//		return ControllersHandlerResult.StopExecution;

			//	if (isNonAnyPageController)
			//		isAnyNonAnyPageControllerCalled = true;
			//}

			//if (isAnyNonAnyPageControllerCalled || securityControllerCalled) return ControllersHandlerResult.Ok;

			//return ExecuteHandlerController(HandlerControllerType.Http404Handler) ? ControllersHandlerResult.Ok : ControllersHandlerResult.Http404;

			throw new NotImplementedException();
		}

		///// <summary>
		///// Determines whether controller can be executed on current page.
		///// </summary>
		///// <param name="metaData">The controller meta data.</param>
		///// <returns></returns>
		//public bool IsControllerCanBeExecutedOnCurrentPage(IControllerMetaData metaData, string route)
		//{
		//	if (metaData.ExecParameters == null)
		//		return true;

		//	if (metaData.ExecParameters.Route == null)
		//		return true;

		//	return _routeMatcher.Match(route, metaData.ExecParameters.Route).Success;
		//}
	}
}