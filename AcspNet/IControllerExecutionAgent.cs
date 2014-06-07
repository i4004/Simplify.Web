using AcspNet.Meta;

namespace AcspNet
{
	/// <summary>
	/// Represent controller execution agent
	/// </summary>
	public interface IControllerExecutionAgent
	{
		/// <summary>
		/// Determines whether controller is not executed on any page.
		/// </summary>
		/// <param name="metaContainer">The controller meta container.</param>
		/// <returns></returns>
		bool IsNonAnyPageController(ControllerMetaContainer metaContainer);

		/// <summary>
		/// Determines whether controller can be executed on current page.
		/// </summary>
		/// <param name="metaContainer">The controller meta container.</param>
		/// <returns></returns>
		bool IsControllerCanBeExecutedOnCurrentPage(ControllerMetaContainer metaContainer);

		/// <summary>
		/// Determines whether controller security rules violated.
		/// </summary>
		/// <param name="metaContainer">The controller meta container.</param>
		/// <returns></returns>
		SecurityViolationResult IsSecurityRulesViolated(ControllerMetaContainer metaContainer);
	}
}