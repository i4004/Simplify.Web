using System;

namespace AcspNet
{
	[AttributeUsage(AttributeTargets.Class)]
	public class RunTypeAttribute : Attribute
	{
		public RunType RunType { get; private set; }

		public RunTypeAttribute(RunType runType)
		{
			RunType = runType;
		}
	}
}