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
		private readonly IContainerFactory _containerFactory;

		//private bool _isExecutionStopped;

		internal ControllersHandler(IControllersMetaStore controllersMetaStore, string currentAction, string currentMode, IContainerFactory containerFactory)
		{
			_controllersMetaStore = controllersMetaStore;
			_currentAction = currentAction;
			_currentMode = currentMode;
			_containerFactory = containerFactory;
		}

		///// <summary>
		///// Creates and executes ACSP extensions for current HTTP request
		///// </summary>
		//public void Execute()
		//{
			//if (!_isExecutionStopped)
			//	_displayer.DisplayNoCache(_pageBuilder.Buid(_dataCollector.Items));

			//	if (Session[IsNewSessionFieldName] == null)
			//		Session.Add(IsNewSessionFieldName, "true");
		//}

		///// <summary>
		///// Stop controllers execution
		///// </summary>
		//private void StopExecution()
		//{
		//	_isExecutionStopped = true;
		//}

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
					//if (!_isExecutionStopped)
						((Controller)_containerFactory.CreateContainer(metaContainer.ControllerType)).Invoke();
				}
			}
		}
	}
}