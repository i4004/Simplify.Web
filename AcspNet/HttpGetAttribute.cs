using System;

namespace AcspNet
{
	/// <summary>
	/// Indicates what controller handles only HTTP GET requests, error will be returned on other request types
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class HttpGetAttribute : Attribute
	{
	}
}