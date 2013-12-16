using System;

namespace AcspNet
{
	/// <summary>
	/// Set extensions execute/initialize Priority
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class PriorityAttribute : Attribute
	{
		/// <summary>
		/// Gets the priority.
		/// </summary>
		/// <value>
		/// The priority.
		/// </value>
		public int Priority { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PriorityAttribute"/> class.
		/// </summary>
		/// <param name="priority">The priority.</param>
		public PriorityAttribute(int priority)
		{
			Priority = priority;
		}
	}
}