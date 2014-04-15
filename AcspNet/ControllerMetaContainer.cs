namespace AcspNet
{
	/// <summary>
	/// Controller meta-data information container
	/// </summary>
	public class ControllerMetaContainer
	{
		private readonly string _action;
		private readonly string _mode;
		private readonly int _runPriority;
		private readonly bool _runOnDefaultPage;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerMetaContainer" /> class.
		/// </summary>
		/// <param name="action">The action.</param>
		/// <param name="mode">The mode.</param>
		/// <param name="runPriority">The run priority.</param>
		/// <param name="runOnDefaultPage">if set to <c>true</c> then the controller will be launched only on default page.</param>
		public ControllerMetaContainer(string action, string mode, int runPriority, bool runOnDefaultPage = false)
		{
			_action = action;
			_mode = mode;
			_runPriority = runPriority;
			_runOnDefaultPage = runOnDefaultPage;
		}

		/// <summary>
		/// Gets the action.
		/// </summary>
		/// <value>
		/// The action.
		/// </value>
		public string Action
		{
			get { return _action; }
		}

		/// <summary>
		/// Gets the mode.
		/// </summary>
		/// <value>
		/// The mode.
		/// </value>
		public string Mode
		{
			get { return _mode; }
		}

		/// <summary>
		/// Gets the run priority.
		/// </summary>
		/// <value>
		/// The run priority.
		/// </value>
		public int RunPriority
		{
			get { return _runPriority; }
		}

		/// <summary>
		/// Gets a value indicating whether the controller will be run only on default page
		/// </summary>
		/// <value>
		///   <c>true</c> if the controller will be run only on default page; otherwise, <c>false</c>.
		/// </value>
		public bool RunOnDefaultPage
		{
			get { return _runOnDefaultPage; }
		}
	}
}