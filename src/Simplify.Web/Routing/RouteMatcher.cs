using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Simplify.Web.Routing
{
	/// <summary>
	/// Provides HTTP route parser and matcher
	/// </summary>
	public class RouteMatcher : IRouteMatcher
	{
		private readonly IControllerPathParser _controllerPathParser;

		/// <summary>
		/// Initializes a new instance of the <see cref="RouteMatcher"/> class.
		/// </summary>
		/// <param name="controllerPathParser">The controller path parser.</param>
		public RouteMatcher(IControllerPathParser controllerPathParser)
		{
			_controllerPathParser = controllerPathParser;
		}

		/// <summary>
		/// Matches the current path with controller path.
		/// Only "/", "/action", "/action/{userName}/{id}", "/action/{id:int}", "/{id}" etc. route types allowed
		/// </summary>
		/// <param name="currentPath">The current path.</param>
		/// <param name="controllerPath">The controller path.</param>
		/// <returns></returns>
		public IRouteMatchResult Match(string currentPath, string controllerPath)
		{
			if (string.IsNullOrEmpty(currentPath))
				return new RouteMatchResult();

			// Run on all pages route
			if (string.IsNullOrEmpty(controllerPath))
				return new RouteMatchResult(true);

			var controllerPathParsed = _controllerPathParser.Parse(controllerPath);
			var currentPathItems = currentPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

			if (currentPathItems.Length != controllerPathParsed.Items.Count)
				return new RouteMatchResult();

			IDictionary<string, Object> routeParameters = new ExpandoObject();

			for (var i = 0; i < controllerPathParsed.Items.Count; i++)
			{
				var currentItem = controllerPathParsed.Items[i];

				if (currentItem is PathSegment)
				{
					if (currentItem.Name != currentPathItems[i])
						return new RouteMatchResult();
				}
				else if (currentItem is PathParameter)
				{
					var value = GetParameterValue((PathParameter)currentItem, currentPathItems[i]);

					if (value == null)
						return new RouteMatchResult();

					routeParameters.Add(currentItem.Name, value);
				}
			}

			// Return string value
			return new RouteMatchResult(true, routeParameters);
		}

		private static object GetParameterValue(PathParameter pathParameter, string source)
		{
			if (pathParameter.Type == typeof(string))
				return source;

			if (pathParameter.Type == typeof(int))
			{
				int buffer;

				if (!int.TryParse(source, out buffer))
					return null;

				return buffer;
			}

			if (pathParameter.Type == typeof(decimal))
			{
				decimal buffer;

				if (!decimal.TryParse(source, out buffer))
					return null;

				return buffer;
			}

			return null;
		}
	}
}