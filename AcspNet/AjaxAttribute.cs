using System;

namespace AcspNet
{
	/// <summary>
	/// Indicates what this controller handles ajax request, controllers execution will be stopped, AjaxResult data will be returent to the user
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class AjaxAttribute : Attribute
	{
	}
}