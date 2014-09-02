namespace AcspNet
{
	/// <summary>
	/// AcspNet synchronous model controllers base class
	/// </summary>
	public abstract class Controller<T> : ModelController<T>
		where T : class
	{
		/// <summary>
		/// Invokes the controller.
		/// </summary>
		public abstract ControllerResponse Invoke();
	}
}