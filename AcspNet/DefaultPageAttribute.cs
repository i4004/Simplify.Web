using System;

namespace AcspNet
{
	/// <summary>
	/// Run controller only on default page
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class DefaultPageAttribute : Attribute
	{
	}
}