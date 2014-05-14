using System;
using System.Collections.Generic;
using System.Linq;
using AcspNet.Meta;
using AcspNet.Modules.Identity;

namespace AcspNet
{
	/// <summary>
	/// Provides controller execution agent
	/// </summary>
	public class ControllerExecutor : IControllerExecutor
	{
		private readonly IControllerExecutionAgent _executionAgent;
		private readonly IControllerFactory _controllerFactory;

		private readonly IList<ControllerMetaContainer> _controllersMetaData;

		public ControllerExecutor(IControllerExecutionAgent executionAgent, IControllerFactory controllerFactory, IControllersMetaStore controllersMetaStore)
		{
			_executionAgent = executionAgent;
			_controllerFactory = controllerFactory;

			_controllersMetaData = controllersMetaStore.GetControllersMetaData();
		}

		//private SecurityCheckResult ProcessSecurityChecks(ControllerMetaContainer metaContainer)
		//{
		//	var securityCheckResult = _executionAgent.IsSecurityRulesViolated(metaContainer);

		//	switch (securityCheckResult)
		//	{
		//		case SecurityViolationResult.RequestTypeViolated:
		//			return !ExecuteHandlerController(HandlerControllerType.Http400Handler)
		//				? SecurityCheckResult.Http400 : SecurityCheckResult.SecurityControllerCalled;
		//		case SecurityViolationResult.AuthenticationRequired:
		//			return !ExecuteHandlerController(HandlerControllerType.Http403Handler)
		//				? SecurityCheckResult.Http403 : SecurityCheckResult.SecurityControllerCalled;
		//	}

		//	return SecurityCheckResult.Ok;
		//}

		public bool ExecuteHandlerController(HandlerControllerType controllerType)
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