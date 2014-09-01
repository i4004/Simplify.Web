namespace AcspNet
{
	/// <summary>
	/// AcspNet controllers base class
	/// </summary>
	public abstract class ControllerBase : ActionModulesAccessor
	{
		/// <summary>
		/// Gets the route parameters.
		/// </summary>
		/// <value>
		/// The route parameters.
		/// </value>
		public virtual dynamic RouteParameters { get; internal set; }

		/// <summary>
		/// Gets the model of current request.
		/// </summary>
		/// <value>
		/// The current request model.
		/// </value>
		public virtual object Model { get; internal set; }
	}
}