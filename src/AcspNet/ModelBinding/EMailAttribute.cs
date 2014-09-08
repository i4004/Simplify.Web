using System;

namespace AcspNet.ModelBinding
{
	/// <summary>
	/// Indicates what this property should be a valid email address
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class EMailAttribute : Attribute
	{
	}
}