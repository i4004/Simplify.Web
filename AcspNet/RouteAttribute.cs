using System;

namespace AcspNet
{
	/// <summary>
	/// Set controller route path
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class RouteAttribute : Attribute
	{
		/// <summary>
		/// Gets the route.
		/// </summary>
		/// <value>
		/// The route.
		/// </value>
		public string Route { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="RouteAttribute"/> class.
		/// </summary>
		/// <param name="route">The controller route path.</param>
		public RouteAttribute(string route)
		{
			Route = route;
		}
	}
}