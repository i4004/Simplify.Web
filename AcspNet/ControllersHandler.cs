using System;
using System.Linq;
using AcspNet.Meta;

namespace AcspNet
{
	/// <summary>
	/// Creates and executes controllers for current HTTP request
	/// </summary>
	public class ControllersHandler
	{
		private readonly IControllersMetaStore _controllersMetaStore;
		private readonly string _currentAction;
		private readonly string _currentMode;
		private readonly string _httpMethod;
		private readonly IControllerFactory _controllerFactory;

		internal ControllersHandler(IControllersMetaStore controllersMetaStore, IControllerFactory controllerFactory, string currentAction, string currentMode, string httpMethod = null)
		{
			_controllersMetaStore = controllersMetaStore;
			_currentAction = currentAction;
			_currentMode = currentMode;
			_httpMethod = httpMethod;
			_controllerFactory = controllerFactory;
		}

		/// <summary>
		/// Creates and invokes controllers.
		/// </summary>
		public ControllersHandlerResult Execute()
		{
			var controllersMetaData = _controllersMetaStore.GetControllersMetaData();

			var currentPageControllers = controllersMetaData.Where(IsCurrentPageController);

			if (!currentPageControllers.Any())
			{
				
			}

			//foreach (var metaContainer in controllersMetaData)
			//{
			//	if (!CheckExecRules(metaContainer)) continue;

			//	if(!CheckSecurityRules(metaContainer))
			//		return ControllersHandlerResult.Error;

			//	var controller = _controllerFactory.CreateController(metaContainer.ControllerType);
			//	controller.Invoke();

			//	if (metaContainer.ExecParameters != null && metaContainer.ExecParameters.IsAjaxRequest)
			//	{
			//		AjaxResult = controller.AjaxResult;
			//		return ControllersHandlerResult.AjaxRequest;
			//	}

			//	if (controller.StopExecution)
			//		return ControllersHandlerResult.StopExecution;
			//}

			//return ControllersHandlerResult.Ok;
		}

		private bool IsCurrentPageController(ControllerMetaContainer metaContainer)
		{
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

		//private bool CheckExecRules(ControllerMetaContainer metaContainer)
		//{
		//	// Is default page
		//	if (string.IsNullOrEmpty(_currentAction) && string.IsNullOrEmpty(_currentMode))
		//	{
		//		// Is any page controller
		//		if (metaContainer.ExecParameters == null)
		//			return true;

		//		// Is Default page controller
		//		if (metaContainer.ExecParameters.IsDefaultPageController)
		//			return true;
		//	}
		//	else
		//	{
		//		// Is any page controller
		//		if (metaContainer.ExecParameters == null)
		//			return true;

		//		if (!metaContainer.ExecParameters.IsDefaultPageController)
		//		{
		//			// Is any page controller
		//			if (string.IsNullOrEmpty(metaContainer.ExecParameters.Action))
		//				return true;

		//			// Is exact action mode controller and not default page controller
		//			if (String.Equals(metaContainer.ExecParameters.Action, _currentAction, StringComparison.CurrentCultureIgnoreCase) &&
		//			String.Equals(metaContainer.ExecParameters.Mode, _currentMode, StringComparison.CurrentCultureIgnoreCase))
		//				return true;
		//		}
		//	}

		//	return false;
		//}

		private bool CheckSecurityRules(ControllerMetaContainer metaContainer)
		{
			// If there is no security
			if (metaContainer.Security == null)
				return true;

			if (metaContainer.Security.IsHttpGet && _httpMethod != "GET")
				return false;

			if (metaContainer.Security.IsHttpPost && _httpMethod != "POST")
				return false;

			return true;
		}

		public string AjaxResult { get; set; }
	}
}