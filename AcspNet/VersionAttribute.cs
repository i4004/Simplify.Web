using System;

namespace AcspNet
{
	/// <summary>
	/// Extension version
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class VersionAttribute : Attribute
	{
		/// <summary>
		/// Gets the version.
		/// </summary>
		/// <value>
		/// The version.
		/// </value>
		public string Version { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="VersionAttribute"/> class.
		/// </summary>
		/// <param name="version">The version.</param>
		public VersionAttribute(string version)
		{
			Version = version;
		}
	}
}