using System;

namespace AcspNet
{
	/// <summary>
	/// Set assemblies which contains ACSP controllers and views
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class LoadClassesFromAssemblyOfAttribute : Attribute
	{
		/// <summary>
		/// Gets the types of class.
		/// </summary>
		/// <value>
		/// The types.
		/// </value>
		public Type[] Types { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="LoadClassesFromAssemblyOfAttribute"/> class.
		/// </summary>
		/// <param name="types">Specify class type from any assembly to load all AcspNet controllers and views from that assemblies</param>
		public LoadClassesFromAssemblyOfAttribute(params Type[] types)
		{
			Types = types;
		}
	}
}