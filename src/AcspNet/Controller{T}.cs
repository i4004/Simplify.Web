namespace AcspNet
{
	/// <summary>
	/// AcspNet synchronous model controllers base class
	/// </summary>
	public abstract class Controller<T> : SyncControllerBase
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