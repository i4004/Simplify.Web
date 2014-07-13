namespace AcspNet
{
	/// <summary>
	/// Represent HTTP route parser and matcher
	/// </summary>
	public interface IRouteParser
	{
		/// <summary>
		/// Matches the specified route.
		/// </summary>
		/// <param name="route">The route.</param>
		/// <returns></returns>
		IRouteMatchResult Match(string route);
	}
}