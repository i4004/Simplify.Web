namespace AcspNet
{
	public interface IController : IHideObjectMembers
	{
		/// <summary>
		/// Invokes the controller.
		/// </summary>
		IControllerResponse Invoke();
	}
}