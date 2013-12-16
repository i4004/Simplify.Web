using System;

namespace AcspNet
{
	/// <summary>
	/// Attribute for set ACSP extensions
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class LoadIndividualExtensionsAttribute : Attribute
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
		/// <param name="types">Specify ACSP extensions types to load</param>
		public LoadIndividualExtensionsAttribute(params Type[] types)
		{
			Types = types;
		}
	}
}