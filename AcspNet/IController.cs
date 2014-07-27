namespace AcspNet
{
	/// <summary>
	/// Represents controller interface
	/// </summary>
	public interface IController : IHideObjectMembers
	{
		/// <summary>
		/// Invokes the controller.
		/// </summary>
		IControllerResponse Invoke();
	}
}