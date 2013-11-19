using System;

namespace AcspNet
{
	[AttributeUsage(AttributeTargets.Class)]
	public class VersionAttribute : Attribute
	{
		public string Version { get; private set; }

		public VersionAttribute(string version)
		{
			Version = version;
		}
	}
}