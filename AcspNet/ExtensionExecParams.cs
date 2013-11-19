// /////////////////////////////////////////////////////////////

namespace AcspNet
{
	/// <summary>
	///     Parameters which are used to run extension
	/// </summary>
	public class ExtensionExecParams
	{
		public readonly string Action;
		public readonly string Mode;
		public readonly int Priority;
		public readonly ExtensionProtectionTypes ProtectionType;
		public readonly ExtensionRunTypes RunType;

		// /////////////////////////////////////////////////////////////

		public ExtensionExecParams(string action = "", string mode = "", ExtensionProtectionTypes protectionType = ExtensionProtectionTypes.None, int priority = 0,
			ExtensionRunTypes runType = ExtensionRunTypes.OnAction)
		{
			Action = action;
			Mode = mode;
			ProtectionType = protectionType;
			Priority = priority;
			RunType = runType;
		}

		public string GetUrl()
		{
			return string.Format("?act={0}&amp;mode={1}", Action, Mode);
		}
	}
}

// /////////////////////////////////////////////////////////////