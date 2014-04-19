using System;

namespace AcspNet
{
	/// <summary>
	/// Set assemblies which contains ACSP controllers
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class LoadControllersFromAssemblyOfAttribute : Attribute
	{
		/// <summary>
		/// Gets the types of class.
		/// </summary>
		/// <value>
		/// The types.
		/// </value>
		public Type[] Types { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="LoadControllersFromAssemblyOfAttribute"/> class.
		/// </summary>
		/// <param name="types">Specify class type from any assembly to load all AcspNet controllers from that assemblies</param>
		public LoadControllersFromAssemblyOfAttribute(params Type[] types)
		{
			Types = types;
		}
	}
}