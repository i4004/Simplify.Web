using System;

namespace AcspNet.Attributes
{
	/// <summary>
	/// Indicates what controller handles HTTP 403 errors
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class Http403Attribute : Attribute
	{
	}
}