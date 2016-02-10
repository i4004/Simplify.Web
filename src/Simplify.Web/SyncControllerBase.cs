namespace Simplify.Web
{
	/// <summary>
	/// AcspNet synchronous controllers base class
	/// </summary>
	public abstract class SyncControllerBase : ControllerBase
	{
		/// <summary>
		/// Invokes the controller.
		/// </summary>
		public abstract ControllerResponse Invoke();
	}
}