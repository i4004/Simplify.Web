using System;

namespace AcspNet.Routing
{
	/// <summary>
	/// Provides path parameter element
	/// </summary>
	public class PathParameter : PathItem
	{
		/// <summary>
		/// Gets the type of path paramter.
		/// </summary>
		/// <value>
		/// The type of path paramter.
		/// </value>
		public Type Type { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PathParameter"/> class.
		/// </summary>
		/// <param name="name">The name of path parameter.</param>
		/// <param name="type">The type of path paramter.</param>
		public PathParameter(string name, Type type) : base(name)
		{
			Type = type;
		}
	}
}