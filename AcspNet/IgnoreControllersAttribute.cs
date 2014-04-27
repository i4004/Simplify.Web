using System;

namespace AcspNet
{
	/// <summary>
	/// Specify controllers types what should be ignoreed by AcspNet
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class IgnoreControllersAttribute : Attribute
	{
		/// <summary>
		/// Gets the types of controllers.
		/// </summary>
		/// <value>
		/// The types of controllers.
		/// </value>
		public Type[] Types { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="IgnoreControllersAttribute"/> class.
		/// </summary>
		/// <param name="types">Controllers types what should be ignoreed by AcspNet</param>
		public IgnoreControllersAttribute(params Type[] types)
		{
			Types = types;
		}
	}
}