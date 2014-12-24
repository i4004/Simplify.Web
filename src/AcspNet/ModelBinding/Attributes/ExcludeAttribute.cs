using System;

namespace AcspNet.ModelBinding.Attributes
{
	/// <summary>
	/// Indicates what this property should be excluded from model binding
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class ExcludeAttribute : Attribute
	{
	}
}