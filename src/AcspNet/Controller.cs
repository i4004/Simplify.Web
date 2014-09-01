namespace AcspNet
{
	/// <summary>
	/// AcspNet synchronous controllers base class
	/// </summary>
	public abstract class Controller : ControllerBase
	{
		/// <summary>
		/// Invokes the controller.
		/// </summary>
		public abstract ControllerResponse Invoke();
	}
}