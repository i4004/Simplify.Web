using System;

namespace AcspNet
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ActionAttribute : Attribute
	{
		public string Action { get; private set; }

		public ActionAttribute(string action)
		{
			Action = action;
		}
	}
}