namespace AcspNet
{
	public class ControllerExecParameters
	{
		/// <param name="action">The action.</param>
		/// <param name="mode">The mode.</param>
		/// <param name="execPriority">The execution priority.</param>
		/// <param name="defaultPageController">if set to <c>true</c> then the controller will be launched only on default page.</param>
		/// <param name="isAjaxRequest">if set to <c>true</c> then indicates what controller handles ajax requests.</param>
		public ControllerExecParameters(string action = null, string mode = null, int execPriority = 0,
			bool defaultPageController = false, bool isAjaxRequest = false)
		{
			Action = action;
			Mode = mode;
			RunPriority = execPriority;
			IsDefaultPageController = defaultPageController;
			IsAjaxRequest = isAjaxRequest;
		}

		/// <summary>
		/// Gets the action.
		/// </summary>
		/// <value>
		/// The action.
		/// </value>
		public string Action { get; private set; }

		/// <summary>
		/// Gets the mode.
		/// </summary>
		/// <value>
		/// The mode.
		/// </value>
		public string Mode { get; private set; }

		/// <summary>
		/// Gets the run priority.
		/// </summary>
		/// <value>
		/// The run priority.
		/// </value>
		public int RunPriority { get; private set; }

		/// <summary>
		/// Gets a value indicating whether the controller will be run only on default page
		/// </summary>
		/// <value>
		///   <c>true</c> if the controller will be run only on default page; otherwise, <c>false</c>.
		/// </value>
		public bool IsDefaultPageController { get; private set; }

		/// <summary>
		/// Gets a value indicating whether controller is handles ajax request.
		/// </summary>
		public bool IsAjaxRequest { get; private set; }	 
	}
}