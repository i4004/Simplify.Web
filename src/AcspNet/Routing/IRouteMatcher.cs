using System;

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

		/// <summary>
		/// Matches the specified route.
		/// </summary>
		/// <param name="currentUri">The current URI.</param>
		/// <param name="controllerRoute">The controller route.</param>
		/// <param name="siteVirtualPath">The site virtual path.</param>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		IRouteMatchResult Match(Uri currentUri, string controllerRoute, string siteVirtualPath);
	}
}