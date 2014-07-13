using System;

namespace AcspNet
{
	/// <summary>
	/// Set controller HTTP DELETE request route path
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class DeleteAttribute : Attribute
	{
		/// <summary>
		/// Gets the route.
		/// </summary>
		/// <value>
		/// The route.
		/// </value>
		public string Route { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="DeleteAttribute"/> class.
		/// </summary>
		/// <param name="route">The route.</param>
		public DeleteAttribute(string route)
		{
			Route = route;
		}
	}
}