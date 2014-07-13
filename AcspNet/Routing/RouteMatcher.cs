using System.Text.RegularExpressions;

namespace AcspNet.Routing
{
	/// <summary>
	/// Provides HTTP route parser and matcher
	/// </summary>
	public class RouteMatcher : IRouteMatcher
	{
		/// <summary>
		/// Matches the specified route.
		/// </summary>
		/// <param name="sourceRoute">The source route.</param>
		/// <param name="checkingRoute">The checking route.</param>
		/// <returns></returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public IRouteMatchResult Match(string sourceRoute, string checkingRoute)
		{
			if(string.IsNullOrEmpty(sourceRoute) || string.IsNullOrEmpty(checkingRoute))
				return new RouteMatchResult();

			// Slash at the end is not allowed
			if (checkingRoute != "/" && checkingRoute.EndsWith("/"))
				return new RouteMatchResult();

			// Restoring missing slash
			if (!checkingRoute.StartsWith("/"))
				checkingRoute = "/" + checkingRoute;

			// Getting parameter between bracters
			var matches = Regex.Matches(checkingRoute, @"{([a-zA-Z0-9:_\-]*)}");
	
			// No parameter, simple compare of routes
			if (matches.Count == 0)
				return CompareTwoPaths(sourceRoute, checkingRoute);

			// Multiple parameters not allowed
			if (matches.Count > 1)
				return new RouteMatchResult();

			// Getting source string path for compare

			var sourceRouteForChecking = sourceRoute.Substring(0, sourceRoute.LastIndexOf('/'));
			var sourceRouteValue = sourceRoute.Substring(sourceRoute.LastIndexOf('/') + 1);
			var checkingRouteForChecking = checkingRoute.Substring(0, checkingRoute.LastIndexOf('/'));

			// Block restricted characters (route will not match)
			if (!Regex.Match(sourceRouteValue, @"^[a-zA-Z0-9_\-]+$").Success)
				return new RouteMatchResult();

			// Comparing routes without value and parameter
			if (sourceRouteForChecking != checkingRouteForChecking)
				return new RouteMatchResult();

			// Checking for specifed parameter type, for example, parameter should be an int
			var parameterValueMatch = Regex.Match(matches[0].Value, ":(.*)}");
			if (parameterValueMatch.Success)
			{
				var valueType = parameterValueMatch.Groups[1].Value;

				// Parse and return an integer
				if (valueType == "int")
				{
					int buffer;
					if (int.TryParse(sourceRouteValue, out buffer))
						return new RouteMatchResult(true, buffer);

					return new RouteMatchResult();
				}
			}

			// Return string value
			return new RouteMatchResult(true, sourceRouteValue);
		}

		public RouteMatchResult CompareTwoPaths(string sourceRoute, string checkingRoute)
		{
			return new RouteMatchResult(sourceRoute == checkingRoute);
		}
	}
}