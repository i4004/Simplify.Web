using System.Collections.Generic;
using System.Linq;
using AcspNet.Meta;

namespace AcspNet
{
	//public enum SecurityCheckResult
	//{
	//	Ok,
	//	SecurityControllerCalled,
	//	Http400,
	//	Http403
	//}

	/// <summary>
	/// Provides controller execution agent
	/// </summary>
	public class ControllerExecutor : IControllerExecutor
	{
		//private readonly IControllerExecutionAgent _executionAgent;
		//private readonly IControllerFactory _controllerFactory;

		//public ControllerExecutor(IControllerExecutionAgent executionAgent,
		//	IControllerFactory controllerFactory)
		//{
		//	_executionAgent = executionAgent;
		//	_controllerFactory = controllerFactory;
		//}

		//public SecurityCheckResult ProcessSecurityChecks(ControllerMetaContainer metaContainer)
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

		//public bool ExecuteHandlerController(HandlerControllerType controllerType, IList<ControllerMetaContainer> controllersMetaContainers)
		//{
		//	ControllerMetaContainer metaContainer = null;

		//	switch (controllerType)
		//	{
		//		case HandlerControllerType.Http400Handler:
		//			metaContainer = controllersMetaContainers.FirstOrDefault(x => x.Role != null && x.Role.Is400Handler);
		//			break;
		//		case HandlerControllerType.Http403Handler:
		//			metaContainer = controllersMetaContainers.FirstOrDefault(x => x.Role != null && x.Role.Is403Handler);
		//			break;
		//		case HandlerControllerType.Http404Handler:
		//			metaContainer = controllersMetaContainers.FirstOrDefault(x => x.Role != null && x.Role.Is404Handler);
		//			break;
		//	}

		//	if (metaContainer == null)
		//		return false;

		//	var controller = _controllerFactory.CreateController(metaContainer.ControllerType);
		//	controller.Invoke();

		//	return true;
		//}
	}
}