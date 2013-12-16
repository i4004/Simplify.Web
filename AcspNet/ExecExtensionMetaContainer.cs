namespace AcspNet
{
	/// <summary>
	/// Executable extension meta-data information container
	/// </summary>
	public class ExecExtensionMetaContainer : ExtensionMetaContainer
	{
		private readonly string _action;
		private readonly string _mode;
		private readonly RunType _runType;

		/// <summary>
		/// Initializes a new instance of the <see cref="ExecExtensionMetaContainer"/> class.
		/// </summary>
		/// <param name="baseContainer">The base container.</param>
		/// <param name="action">The action.</param>
		/// <param name="mode">The mode.</param>
		/// <param name="runType">Type of the run.</param>
		public ExecExtensionMetaContainer(ExtensionMetaContainer baseContainer, string action, string mode, RunType runType)
			: base(baseContainer)
		{
			_action = action;
			_mode = mode;
			_runType = runType;
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
		/// Gets the type of the run.
		/// </summary>
		/// <value>
		/// The type of the run.
		/// </value>
		public RunType RunType
		{
			get { return _runType; }
		}
	}
}