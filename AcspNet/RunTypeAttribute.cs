using System;

namespace AcspNet
{
	/// <summary>
	/// Executable extension run type
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class RunTypeAttribute : Attribute
	{
		/// <summary>
		/// Gets the type of the run.
		/// </summary>
		/// <value>
		/// The type of the run.
		/// </value>
		public RunType RunType { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="RunTypeAttribute"/> class.
		/// </summary>
		/// <param name="runType">Type of the run.</param>
		public RunTypeAttribute(RunType runType)
		{
			RunType = runType;
		}
	}
}