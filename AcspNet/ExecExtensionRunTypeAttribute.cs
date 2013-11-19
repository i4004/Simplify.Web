using System;

namespace AcspNet
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ExecExtensionRunTypeAttribute : Attribute
	{
		public ExecExtensionRunType RunType { get; private set; }

		public ExecExtensionRunTypeAttribute(ExecExtensionRunType runType)
		{
			RunType = runType;
		}
	}
}