namespace AcspNet
{
	/// <summary>
	/// AcspNet model controllers base class
	/// </summary>
	public abstract class ModelController<T> : ControllerBase
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