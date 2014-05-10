using System;
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
		private readonly string _currentAction;
		private readonly string _currentMode;
		private readonly string _httpMethod;
		private readonly IControllerFactory _controllerFactory;

		private readonly IList<ControllerMetaContainer> _controllersMetaData;

		internal ControllersHandler(IControllersMetaStore controllersMetaStore, IControllerFactory controllerFactory, string currentAction, string currentMode, string httpMethod = null)
		{
			_currentAction = currentAction;
			_currentMode = currentMode;
			_httpMethod = httpMethod;
			_controllerFactory = controllerFactory;

			_controllersMetaData = controllersMetaStore.GetControllersMetaData();
		}

		public string AjaxResult { get; set; }

		/// <summary>
		/// Creates and invokes controllers.
		/// </summary>
		public ControllersHandlerResult Execute()
		{
			var isAnyCurrentPageControllerCalled = false;
			var securityControllerCalled = false;

			foreach (var metaContainer in FilterForStandardControllers().Where(ControllerCanBeExecutedOnCurrentPage))
			{
				var isCurrentPageController = false;

				if (IsSecurityRulesViolated(metaContainer))
				{
					var result = ExecuteHandlerController(HandlerControllerType.Http400Handler);

					if (!result)
						return ControllersHandlerResult.Http400;

					securityControllerCalled = true;
				}

				if (IsSpecificOrDefaultageController(metaContainer))
					isCurrentPageController = true;

				if (securityControllerCalled && isCurrentPageController)
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

				if (isCurrentPageController)
					isAnyCurrentPageControllerCalled = true;
			}

			if (isAnyCurrentPageControllerCalled || securityControllerCalled) return ControllersHandlerResult.Ok;

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

		private bool IsSpecificOrDefaultageController(ControllerMetaContainer metaContainer)
		{
			if (metaContainer.ExecParameters == null)
				return false;

			// Is default page
			if (string.IsNullOrEmpty(_currentAction) && string.IsNullOrEmpty(_currentMode))
			{
				// Is Default page controller
				if (metaContainer.ExecParameters.IsDefaultPageController)
					return true;
			}
			else
			{
				if (!metaContainer.ExecParameters.IsDefaultPageController)
				{
					// Is exact action mode controller and not default page controller
					if (String.Equals(metaContainer.ExecParameters.Action, _currentAction, StringComparison.CurrentCultureIgnoreCase) &&
					String.Equals(metaContainer.ExecParameters.Mode, _currentMode, StringComparison.CurrentCultureIgnoreCase))
						return true;
				}
			}

			return false;
		}

		private bool ControllerCanBeExecutedOnCurrentPage(ControllerMetaContainer metaContainer)
		{
			// Is default page
			if (string.IsNullOrEmpty(_currentAction) && string.IsNullOrEmpty(_currentMode))
			{
				// Is any page controller
				if (metaContainer.ExecParameters == null)
					return true;

				// Is Default page controller
				if (metaContainer.ExecParameters.IsDefaultPageController)
					return true;

				// Is any page controller
				if (string.IsNullOrEmpty(metaContainer.ExecParameters.Action))
					return true;
			}
			else
			{
				// Is any page controller
				if (metaContainer.ExecParameters == null)
					return true;

				if (!metaContainer.ExecParameters.IsDefaultPageController)
				{
					// Is any page controller
					if (string.IsNullOrEmpty(metaContainer.ExecParameters.Action))
						return true;

					// Is exact action mode controller and not default page controller
					if (String.Equals(metaContainer.ExecParameters.Action, _currentAction, StringComparison.CurrentCultureIgnoreCase) &&
					String.Equals(metaContainer.ExecParameters.Mode, _currentMode, StringComparison.CurrentCultureIgnoreCase))
						return true;
				}
			}

			return false;
		}

		private bool IsSecurityRulesViolated(ControllerMetaContainer metaContainer)
		{
			// If there is no security
			if (metaContainer.Security == null)
				return false;

			if (metaContainer.Security.IsHttpGet && _httpMethod != "GET")
				return true;

			if (metaContainer.Security.IsHttpPost && _httpMethod != "POST")
				return true;

			return false;
		}
	}
}