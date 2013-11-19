using System;

namespace AcspNet
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ModeAttribute : Attribute
	{
		public string Mode { get; private set; }

		public ModeAttribute(string mode)
		{
			Mode = mode;
		}
	}
}