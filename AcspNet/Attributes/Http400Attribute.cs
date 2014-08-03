using System;

namespace AcspNet.Attributes
{
	/// <summary>
	/// Indicates what controller handles HTTP 400 errors
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class Http400Attribute : Attribute
	{
	}
}