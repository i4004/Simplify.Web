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
		/// <param name="currentPath">The current path.</param>
		/// <param name="controllerPath">The controllerPath.</param>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		IRouteMatchResult Match(string currentPath, string controllerPath);
	}
}