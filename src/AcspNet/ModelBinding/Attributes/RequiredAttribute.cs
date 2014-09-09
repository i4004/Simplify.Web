using System;

namespace AcspNet.ModelBinding.Attributes
{
	/// <summary>
	/// Indicates what this property should be not null or empty
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class RequiredAttribute : Attribute
	{
	}
}