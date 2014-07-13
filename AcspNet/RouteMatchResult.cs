namespace AcspNet
{
	/// <summary>
	/// Provides HTTP route matching resuklt
	/// </summary>
	public class RouteMatchResult : IRouteMatchResult
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RouteMatchResult"/> class.
		/// </summary>
		/// <param name="matched">if set to <c>true</c> [matched].</param>
		/// <param name="value">The value.</param>
		public RouteMatchResult(bool matched, object value)
		{
			Success = matched;
			Value = value;
		}

		/// <summary>
		/// Gets a value indicating whether the route was matched successfully
		/// </summary>
		/// <value>
		/// <c>true</c> if the route was matched successfully; otherwise, <c>false</c>.
		/// </value>
		public bool Success { get; private set; }

		/// <summary>
		/// Gets the route parsed dynamic value (if was present).
		/// </summary>
		/// <value>
		/// The route parsed dynamic value (if was present).
		/// </value>
		public object Value { get; private set; }
	}
}