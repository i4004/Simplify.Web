using System;

namespace AcspNet
{
	/// <summary>
	/// Set controller HTTP GET request route path
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class GetAttribute : Attribute
	{
		public string Route { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="GetAttribute"/> class.
		/// </summary>
		/// <param name="route">The route.</param>
		public GetAttribute(string route)
		{
			Route = route;
		}
	}
}