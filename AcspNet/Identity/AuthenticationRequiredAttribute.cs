using System;

namespace AcspNet.Identity
{
	/// <summary>
	/// Only authenticated users can execute controller
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class AuthenticationRequiredAttribute : Attribute
	{
	}
}