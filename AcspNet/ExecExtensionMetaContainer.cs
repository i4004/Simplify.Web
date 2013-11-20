using System;

namespace AcspNet
{
	public class ExecExtensionMetaContainer : ExtensionMetaContainer
	{
		private readonly string _action;
		private readonly string _mode;
		//private readonly ExtensionProtectionTypes ProtectionType;
		private readonly RunType _runType;

		public ExecExtensionMetaContainer(ExtensionMetaContainer baseContainer, string action, string mode, RunType runType)
			: base(baseContainer)
		{
			_action = action;
			_mode = mode;
			_runType = runType;
		}

		public string Action
		{
			get { return _action; }
		}

		public string Mode
		{
			get { return _mode; }
		}

		public RunType RunType
		{
			get { return _runType; }
		}
	}
}