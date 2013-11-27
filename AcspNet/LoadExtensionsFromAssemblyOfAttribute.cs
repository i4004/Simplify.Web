using System;

namespace AcspNet
{
	/// <summary>
	/// Attribute for specifying assemblies which contains ACSP extensions
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class LoadExtensionsFromAssemblyOfAttribute : Attribute
	{
		/// <summary>
		/// Gets the types of extensions.
		/// </summary>
		/// <value>
		/// The types.
		/// </value>
		public Type[] Types { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="LoadExtensionsFromAssemblyOfAttribute"/> class.
		/// </summary>
		/// <param name="types">Specify class type from any assembly to load all ACSP extensions from that assemblies</param>
		public LoadExtensionsFromAssemblyOfAttribute(params Type[] types)
		{
			Types = types;
		}
	}
}