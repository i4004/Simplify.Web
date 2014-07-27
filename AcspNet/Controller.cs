namespace AcspNet
{
	/// <summary>
	/// AcspNet controller base class
	/// </summary>
	public class Controller : IController
	{
		/// <summary>
		/// Invokes the controller.
		/// </summary>
		public virtual IControllerResponse Invoke()
		{
			return null;
		}
	}
}