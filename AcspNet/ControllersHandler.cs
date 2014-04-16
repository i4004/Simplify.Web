using System;
using System.Collections.Generic;

namespace AcspNet
{
	/// <summary>
	/// Creates and executes controllers for current HTTP request
	/// </summary>
	public class ControllersHandler// : IAcspProcessor, IAcspProcessorContoller
	{
		private readonly IList<ControllerMetaContainer> _controllersMetaContainers;
		private readonly string _currentAction;
		private readonly string _currentMode;
		private readonly IContainerFactory _containerFactory;

		private bool _isExecutionStopped;

		internal ControllersHandler(IList<ControllerMetaContainer> controllersMetaContainers, string currentAction, string currentMode, IContainerFactory containerFactory)//, IList<LibExtensionMetaContainer> libExtensionMetaContainers)
		{
			_controllersMetaContainers = controllersMetaContainers;
			_currentAction = currentAction;
			_currentMode = currentMode;
			_containerFactory = containerFactory;

			CreateControllersInstances();
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

		/// <summary>
		/// Stop controllers execution
		/// </summary>
		private void StopExecution()
		{
			_isExecutionStopped = true;
		}

		private void CreateControllersInstances()
		{
			foreach (var metaContainer in _controllersMetaContainers)
			{
				if ((_currentAction == "" && _currentMode == "" && metaContainer.RunOnDefaultPage) ||
					(String.Equals(metaContainer.Action, _currentAction, StringComparison.CurrentCultureIgnoreCase) &&
					 String.Equals(metaContainer.Mode, _currentMode, StringComparison.CurrentCultureIgnoreCase)) ||
					(metaContainer.Action == "" && !metaContainer.RunOnDefaultPage))
				{
					if (!_isExecutionStopped)
						_containerFactory.CreateController(metaContainer.ControllerType).Invoke();
				}
			}
		}
	}
}