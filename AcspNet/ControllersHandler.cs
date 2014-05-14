using System.Collections.Generic;
using System.Linq;
using AcspNet.Meta;

namespace AcspNet
{
	internal enum SecurityCheckResult
	{
		Ok,
		SecurityControllerCalled,
		Http400,
		Http403
	}

	internal enum ProcessViewModelResult
	{
		Ok,
		Http400
	}

	/// <summary>
	/// Creates and executes controllers for current HTTP request
	/// </summary>
	public class ControllersHandler
	{
		private readonly IControllerExecutor _controllerExecutor;
		private readonly IControllerExecutionAgent _executionAgent;

		internal ControllersHandler(IControllerExecutor controllerExecutor,
			IControllerExecutionAgent executionAgent)
		{
			_controllerExecutor = controllerExecutor;
			_executionAgent = executionAgent;
		}

		/// <summary>
		/// Gets or sets the ajax result.
		/// </summary>
		/// <value>
		/// The ajax result.
		/// </value>
		public string AjaxResult { get; set; }

		/// <summary>
		/// Creates and invokes controllers.
		/// </summary>
		public ControllersHandlerResult Execute()
		{
			var isAnyNonAnyPageControllerCalled = false;
			var securityControllerCalled = false;

			//foreach (var metaContainer in FilterForStandardControllers().Where(_executionAgent.IsControllerCanBeExecutedOnCurrentPage))
			//{
			//	var isNonAnyPageController = false;

			//	if (!securityControllerCalled)
			//	{
			//		var result = ProcessSecurityChecks(metaContainer);

			//		switch (result)
			//		{
			//			case SecurityCheckResult.Ok:
			//				securityControllerCalled = true;
			//				break;
			//			case SecurityCheckResult.Http400:
			//				return ControllersHandlerResult.Http400;
			//			case SecurityCheckResult.Http403:
			//				return ControllersHandlerResult.Http403;
			//		}
			//	}

			//	if (_executionAgent.IsNonAnyPageController(metaContainer))
			//		isNonAnyPageController = true;

			//	if (metaContainer.Data != null)
			//	{
			//		ProcessViewModelResult processViewModelResult;
			//		var viewMode = ProcessViewModel(metaContainer, out processViewModelResult);

			//		if (processViewModelResult == ProcessViewModelResult.Http400)
			//			securityControllerCalled = true;
			//	}

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

			//return _controllerExecutor.ExecuteHandlerController(HandlerControllerType.Http404Handler) ? ControllersHandlerResult.Ok : ControllersHandlerResult.Http404;

			return ControllersHandlerResult.Ok;
		}

		//private object ProcessViewModel(ControllerMetaContainer metaContainer, out ProcessViewModelResult result)
		//{
		//	result = ProcessViewModelResult.Ok;
		//	CreateViewModelResult createViewModelResult;

		//	var viewModel = _viewModelFactory.CreateViewModel(metaContainer.Data.ViewModel, out createViewModelResult);

		//	if (createViewModelResult != CreateViewModelResult.BadData) return viewModel;

		//	var handlerResult = ExecuteHandlerController(HandlerControllerType.Http400Handler);

		//	if (!handlerResult)
		//		result = ProcessViewModelResult.Http400;

		//	return viewModel;
		//}
	}
}