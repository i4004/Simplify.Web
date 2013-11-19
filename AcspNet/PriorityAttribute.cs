using System;

namespace AcspNet
{
	[AttributeUsage(AttributeTargets.Class)]
	public class PriorityAttribute : Attribute
	{
		public int Priority { get; private set; }

		public PriorityAttribute(int priority)
		{
			Priority = priority;
		}
	}
}