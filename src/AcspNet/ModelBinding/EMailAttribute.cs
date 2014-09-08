using System;

namespace AcspNet.ModelBinding
{
	/// <summary>
	/// Indicates what this property should be an email address and should be not null or empty
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class EMailAttribute : Attribute
	{
	}
}