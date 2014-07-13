namespace AcspNet.Routing
{
	/// <summary>
	/// Represent HTTP route parser and matcher
	/// </summary>
	public interface IRouteMatcher
	{
		/// <summary>
		/// Matches the specified route.
		/// </summary>
		/// <param name="sourceRoute">The source route.</param>
		/// <param name="checkingRoute">The checking route.</param>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		IRouteMatchResult Match(string sourceRoute, string checkingRoute);
	}
}