namespace AcspNet.Meta
{
	/// <summary>
	/// Provides controller execution parameters
	/// </summary>
	public class ControllerExecParameters
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerExecParameters"/> class.
		/// </summary>
		/// <param name="route">The route.</param>
		/// <param name="execPriority">The execute priority.</param>
		public ControllerExecParameters(string route = null, int execPriority = 0)
		{
			Route = route;
			RunPriority = execPriority;
		}

		/// <summary>
		/// Gets the route.
		/// </summary>
		/// <value>
		/// The route.
		/// </value>
		public string Route { get; private set; }

		/// <summary>
		/// Gets the run priority.
		/// </summary>
		/// <value>
		/// The run priority.
		/// </value>
		public int RunPriority { get; private set; }
	}
}