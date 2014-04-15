using System;

namespace AcspNet
{
	/// <summary>
	/// Set AcspNet controller and views
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class LoadIndividualClassesAttribute : Attribute
	{
		/// <summary>
		/// Gets the types of class.
		/// </summary>
		/// <value>
		/// The types.
		/// </value>
		public Type[] Types { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="LoadIndividualClassesAttribute"/> class.
		/// </summary>
		/// <param name="types">Specify AcspNet controllers and views types to load</param>
		public LoadIndividualClassesAttribute(params Type[] types)
		{
			Types = types;
		}
	}
}