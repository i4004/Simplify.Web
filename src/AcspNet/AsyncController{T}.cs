namespace AcspNet
{
	/// <summary>
	/// AcspNet asynchronous model controllers base class
	/// </summary>
	public abstract class AsyncController<T> : AsyncControllerBase
		where T : class
	{
		/// <summary>
		/// Gets the model of current request.
		/// </summary>
		/// <value>
		/// The current request model.
		/// </value>
		public virtual T Model { get; internal set; }
	}
}