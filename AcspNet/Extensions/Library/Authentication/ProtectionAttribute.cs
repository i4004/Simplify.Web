using System;

namespace AcspNet.Extensions.Library.Authentication
{
	/// <summary>
	/// Set executable extensions Protection type
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class ProtectionAttribute : Attribute
	{
		/// <summary>
		/// Gets the type of the protection.
		/// </summary>
		/// <value>
		/// The type of the protection.
		/// </value>
		public Protection ProtectionType { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ProtectionAttribute"/> class.
		/// </summary>
		/// <param name="protectionType">Type of the protection.</param>
		public ProtectionAttribute(Protection protectionType)
		{
			ProtectionType = protectionType;
		}
	}
}