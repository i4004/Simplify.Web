using System;

namespace AcspNet
{
	/// <summary>
	/// Attribute for specifying ACSP extensions
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class LoadIndividualExtensionsAttribute : Attribute
	{
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