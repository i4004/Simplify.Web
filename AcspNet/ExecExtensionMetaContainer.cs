using System;

namespace AcspNet
{
	public class ExecExtensionMetaContainer : ExtensionMetaContainer
	{
		private readonly string _action;
		private readonly string _mode;
		//private readonly ExtensionProtectionTypes ProtectionType;
		private readonly ExecExtensionRunType _runType;

		public ExecExtensionMetaContainer(ExtensionMetaContainer baseContainer, string action, string mode, ExecExtensionRunType runType)
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

		public ExecExtensionRunType RunType
		{
			get { return _runType; }
		}
	}
}