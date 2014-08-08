namespace AcspNet.Meta
{
	/// <summary>
	/// Provides controller route information
	/// </summary>
	public class ControllerRouteInfo
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerRouteInfo"/> class.
		/// </summary>
		/// <param name="getRoute">The HTTP GET request route.</param>
		/// <param name="postRoute">The HTTP POST request route.</param>
		/// <param name="putRoute">The HTTP PUT request route.</param>
		/// <param name="deleteRoute">The HTTP DELETE route.</param>
		public ControllerRouteInfo(string getRoute = null, string postRoute = null, string putRoute = null, string deleteRoute = null)
		{
			GetRoute = getRoute;
			PostRoute = postRoute;
			PutRoute = putRoute;
			DeleteRoute = deleteRoute;
		}

		/// <summary>
		/// Gets or sets the get route.
		/// </summary>
		/// <value>
		/// The get route.
		/// </value>
		public string GetRoute { get; set; }

		/// <summary>
		/// Gets or sets the post route.
		/// </summary>
		/// <value>
		/// The post route.
		/// </value>
		public string PostRoute { get; set; }

		/// <summary>
		/// Gets or sets the put route.
		/// </summary>
		/// <value>
		/// The put route.
		/// </value>
		public string PutRoute { get; set; }

		/// <summary>
		/// Gets or sets the delete route.
		/// </summary>
		/// <value>
		/// The delete route.
		/// </value>
		public string DeleteRoute { get; set; }
	}
}