using System;

namespace AcspNet
{
	/// <summary>
	/// Executable extension "action" query string parameter
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class ActionAttribute : Attribute
	{
		/// <summary>
		/// Gets the action.
		/// </summary>
		/// <value>
		/// The action.
		/// </value>
		public string Action { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionAttribute"/> class.
		/// </summary>
		/// <param name="action">The action.</param>
		public ActionAttribute(string action)
		{
			Action = action;
		}
	}
}