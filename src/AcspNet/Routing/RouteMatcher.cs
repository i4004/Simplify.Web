using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AcspNet.Routing
{
	/// <summary>
	/// Provides HTTP route parser and matcher
	/// </summary>
	public class RouteMatcher : IRouteMatcher
	{
		private readonly IControllerPathParser _controllerPathParser;

		public RouteMatcher(IControllerPathParser controllerPathParser)
		{
			_controllerPathParser = controllerPathParser;
		}

		/// <summary>
		/// Matches the current path with controller path.
		/// Only "/", "/action", "/action/{userName}/{id}", "/action/{id:int}", "/{id}" etc. route types allowed
		/// </summary>
		/// <param name="currentPath">The current path.</param>
		/// <param name="controllerPath">The controllerPath.</param>
		/// <returns></returns>
		public IRouteMatchResult Match(string currentPath, string controllerPath)
		{
			if (string.IsNullOrEmpty(currentPath))
				return new RouteMatchResult();

			// Run on all pages route
			if (controllerPath == null)
				return new RouteMatchResult(true);

			if (controllerPath == "")
				return new RouteMatchResult();

			// Slash at the end is not allowed
			if (controllerPath != "/" && controllerPath.EndsWith("/"))
				return new RouteMatchResult();

			// Restoring missing slash
			if (!controllerPath.StartsWith("/"))
				controllerPath = "/" + controllerPath;

			// Getting parameter between bracters
			var matches = Regex.Matches(controllerPath, @"{([a-zA-Z0-9:_\-]*)}");

			//// No parameter, simple compare of routes
			//if (matches.Count == 0)
			//	return CompareTwoPaths(sourceRoute, checkingRoute);

			//// Multiple parameters not allowed
			//if (matches.Count > 1)
			//	return new RouteMatchResult();

			//// Getting source string path for compare

			//var sourceRouteForChecking = sourceRoute.Substring(0, sourceRoute.LastIndexOf('/'));
			//var sourceRouteValue = sourceRoute.Substring(sourceRoute.LastIndexOf('/') + 1);
			//var checkingRouteForChecking = checkingRoute.Substring(0, checkingRoute.LastIndexOf('/'));

			//// Block restricted characters (route will not match)
			//if (!Regex.Match(sourceRouteValue, @"^[a-zA-Z0-9_\-]+$").Success)
			//	return new RouteMatchResult();

			//// Comparing routes without value and parameter
			//if (sourceRouteForChecking != checkingRouteForChecking)
			//	return new RouteMatchResult();

			//// Checking for specifed parameter type, for example, parameter should be an int
			//var parameterValueMatch = Regex.Match(matches[0].Value, ":(.*)}");
			//if (parameterValueMatch.Success)
			//{
			//	var valueType = parameterValueMatch.Groups[1].Value;

			//	// Parse and return an integer
			//	if (valueType == "int")
			//	{
			//		int buffer;
			//		return int.TryParse(sourceRouteValue, out buffer) ? new RouteMatchResult(true, buffer) : new RouteMatchResult();
			//	}
			//}

			//// Return string value
			//return new RouteMatchResult(true, sourceRouteValue);
			return null;
		}

		//private static RouteMatchResult CompareTwoPaths(string sourceRoute, string checkingRoute)
		//{
		//	return new RouteMatchResult(sourceRoute == checkingRoute);
		//}
	}
}
