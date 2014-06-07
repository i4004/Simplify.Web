using System;

namespace AcspNet
{
	/// <summary>
	/// Indicates what controller handles only HTTP POST requests, error will be returned on other request types
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class HttpPostAttribute : Attribute
	{
	}
}