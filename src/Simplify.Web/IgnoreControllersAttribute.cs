using System;

namespace Simplify.Web
{
	/// <summary>
	/// Specify controllers types which should be ignored by AcspNet
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
		/// <param name="types">Controllers types which should be ignored by AcspNet</param>
		public IgnoreControllersAttribute(params Type[] types)
		{
			Types = types;
		}
	}
}