using System;

namespace AcspNet
{
	/// <summary>
	/// Indicates what controller handles HTTP 403 errors
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class Http403Attribute : Attribute
	{
	}
}