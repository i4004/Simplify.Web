using System;

namespace AcspNet
{
	/// <summary>
	/// Set executable extension "Mode" query string parameter
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class ModeAttribute : Attribute
	{
		/// <summary>
		/// Gets the mode.
		/// </summary>
		/// <value>
		/// The mode.
		/// </value>
		public string Mode { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ModeAttribute"/> class.
		/// </summary>
		/// <param name="mode">The mode.</param>
		public ModeAttribute(string mode)
		{
			Mode = mode;
		}
	}
}