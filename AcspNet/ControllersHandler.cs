using System.Collections.Generic;
using System.Linq;
using AcspNet.Meta;

namespace AcspNet
{
	/// <summary>
	/// Creates and executes controllers for current HTTP request
	/// </summary>
	public class ControllersHandler
	{
		private readonly IControllerFactory _controllerFactory;
		private readonly IControllerExecutionAgent _executionAgent;

		private readonly IList<ControllerMetaContainer> _controllersMetaData;

		internal ControllersHandler(IControllersMetaStore controllersMetaStore, IControllerFactory controllerFactory, IControllerExecutionAgent executionAgent)
		{
			_controllerFactory = controllerFactory;
			_executionAgent = executionAgent;

			_controllersMetaData = controllersMetaStore.GetControllersMetaData();
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

			foreach (var metaContainer in FilterForStandardControllers().Where(_executionAgent.IsControllerCanBeExecutedOnCurrentPage))
			{
				var isNonAnyPageController = false;

				if (!securityControllerCalled)
				{
					var securityCheckResult = _executionAgent.IsSecurityRulesViolated(metaContainer);

					if (securityCheckResult == SecurityViolationResult.RequestTypeViolated)
					{
						var result = ExecuteHandlerController(HandlerControllerType.Http400Handler);

						if (!result)
							return ControllersHandlerResult.Http400;

						securityControllerCalled = true;
					}

					if (securityCheckResult == SecurityViolationResult.AuthenticationRequired)
					{
						var result = ExecuteHandlerController(HandlerControllerType.Http403Handler);

						if (!result)
							return ControllersHandlerResult.Http403;

						securityControllerCalled = true;
					}
				}

				if (_executionAgent.IsNonAnyPageController(metaContainer))
					isNonAnyPageController = true;

				if (securityControllerCalled && isNonAnyPageController)
					continue;

				var controller = _controllerFactory.CreateController(metaContainer.ControllerType);
				controller.Invoke();

				if (metaContainer.ExecParameters != null && metaContainer.ExecParameters.IsAjax)
				{
					AjaxResult = controller.AjaxResult;
					return ControllersHandlerResult.AjaxRequest;
				}

				if (controller.StopExecution)
					return ControllersHandlerResult.StopExecution;

				if (isNonAnyPageController)
					isAnyNonAnyPageControllerCalled = true;
			}

			if (isAnyNonAnyPageControllerCalled || securityControllerCalled) return ControllersHandlerResult.Ok;

			return ExecuteHandlerController(HandlerControllerType.Http404Handler) ? ControllersHandlerResult.Ok : ControllersHandlerResult.Http404;
		}

		private bool ExecuteHandlerController(HandlerControllerType controllerType)
		{
			ControllerMetaContainer metaContainer = null;

			switch (controllerType)
			{
					case HandlerControllerType.Http400Handler:
						metaContainer = _controllersMetaData.FirstOrDefault(x => x.Role != null && x.Role.Is400Handler);
						break;
					case HandlerControllerType.Http403Handler:
						metaContainer = _controllersMetaData.FirstOrDefault(x => x.Role != null && x.Role.Is403Handler);
						break;
					case HandlerControllerType.Http404Handler:
						metaContainer = _controllersMetaData.FirstOrDefault(x => x.Role != null && x.Role.Is404Handler);
						break;
			}

			if (metaContainer == null)
				return false;

			var controller = _controllerFactory.CreateController(metaContainer.ControllerType);
			controller.Invoke();

			return true;
		}

		private IEnumerable<ControllerMetaContainer> FilterForStandardControllers()
		{
			return
				_controllersMetaData.Where(
					x => x.Role == null || (x.Role.Is400Handler == false && x.Role.Is403Handler == false && x.Role.Is404Handler == false));
		}
	}
}