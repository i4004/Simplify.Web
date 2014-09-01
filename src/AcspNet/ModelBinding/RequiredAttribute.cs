using System;

namespace AcspNet.ModelBinding
{
	/// <summary>
	/// Indicates what this property is required
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class RequiredAttribute : Attribute
	{
	}
}