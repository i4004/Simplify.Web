using System;

namespace AcspNet
{
	/// <summary>
	/// Set AcspNet controller
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class LoadIndividualControllersAttribute : Attribute
	{
		/// <summary>
		/// Gets the types of class.
		/// </summary>
		/// <value>
		/// The types.
		/// </value>
		public Type[] Types { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="LoadIndividualControllersAttribute"/> class.
		/// </summary>
		/// <param name="types">Specify AcspNet controllers types to load</param>
		public LoadIndividualControllersAttribute(params Type[] types)
		{
			Types = types;
		}
	}
}