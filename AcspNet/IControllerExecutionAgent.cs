using AcspNet.Meta;

namespace AcspNet
{
	public interface IControllerExecutionAgent
	{
		bool IsNonAnyPageController(ControllerMetaContainer metaContainer);
		bool IsControllerCanBeExecutedOnCurrentPage(ControllerMetaContainer metaContainer);
		SecurityViolationResult IsSecurityRulesViolated(ControllerMetaContainer metaContainer);
	}
}