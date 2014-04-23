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
		private readonly IControllerFactory _controllerFactory;

		internal ControllersHandler(IControllersMetaStore controllersMetaStore, IControllerFactory controllerFactory, string currentAction, string currentMode)
		{
			_controllersMetaStore = controllersMetaStore;
			_currentAction = currentAction;
			_currentMode = currentMode;
			_controllerFactory = controllerFactory;
		}

		/// <summary>
		/// Creates and invokes controllers.
		/// </summary>
		public void CreateAndInvokeControllers()
		{
			var controllersMetaData = _controllersMetaStore.GetControllersMetaData();

			foreach (var metaContainer in controllersMetaData)
			{
				if ((_currentAction == "" && _currentMode == "" && metaContainer.RunOnDefaultPage) ||
					(String.Equals(metaContainer.Action, _currentAction, StringComparison.CurrentCultureIgnoreCase) &&
					 String.Equals(metaContainer.Mode, _currentMode, StringComparison.CurrentCultureIgnoreCase)) ||
					(metaContainer.Action == "" && !metaContainer.RunOnDefaultPage))
				{
					_controllerFactory.CreateController(metaContainer.ControllerType).Invoke();
				}
			}
		}
	}
}