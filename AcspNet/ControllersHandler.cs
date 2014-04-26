using System;

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
		public ControllersHandlerResult CreateAndInvokeControllers()
		{
			var controllersMetaData = _controllersMetaStore.GetControllersMetaData();

			foreach (var metaContainer in controllersMetaData)
			{
				if ((string.IsNullOrEmpty(_currentAction) && string.IsNullOrEmpty(_currentMode)
					&& (metaContainer.ExecParameters == null || metaContainer.ExecParameters.IsDefaultPageController))
					|| (metaContainer.ExecParameters != null && !metaContainer.ExecParameters.IsDefaultPageController
					&& String.Equals(metaContainer.ExecParameters.Action, _currentAction, StringComparison.CurrentCultureIgnoreCase) &&
					 String.Equals(metaContainer.ExecParameters.Mode, _currentMode, StringComparison.CurrentCultureIgnoreCase))
					|| (metaContainer.ExecParameters == null || (string.IsNullOrEmpty(metaContainer.ExecParameters.Action) && !metaContainer.ExecParameters.IsDefaultPageController)))
				{
					if (metaContainer.Security != null &&
						((metaContainer.Security.IsHttpGet && _httpMethod != "GET") || (metaContainer.Security.IsHttpPost && _httpMethod != "POST")))
						return ControllersHandlerResult.Error;

					var controller = _controllerFactory.CreateController(metaContainer.ControllerType);
					controller.Invoke();

					if (metaContainer.ExecParameters != null && metaContainer.ExecParameters.IsAjaxRequest)
					{
						AjaxResult = controller.AjaxResult;
						return ControllersHandlerResult.AjaxRequest;
					}

					if (controller.StopExecution)
						return ControllersHandlerResult.StopExecution;
				}
			}

			return ControllersHandlerResult.Ok;
		}

		public string AjaxResult { get; set; }
	}
}