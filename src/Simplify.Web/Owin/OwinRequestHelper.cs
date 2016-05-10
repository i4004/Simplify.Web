using System;
using System.Globalization;
using Microsoft.Owin;

namespace Simplify.Web.Owin
{
	public static class OwinRequestHelper
	{
		public static DateTime? GetIfModifiedSinceTime(IHeaderDictionary headers)
		{
			DateTime? ifModifiedSinceTime = null;

			if (headers.ContainsKey("If-Modified-Since"))
				ifModifiedSinceTime = DateTime.ParseExact(headers["If-Modified-Since"], "r",
					CultureInfo.InvariantCulture);

			return ifModifiedSinceTime;
		}

		public static bool IsCacheCanBeUsed(string cacheControlHeader)
		{
			return string.IsNullOrEmpty(cacheControlHeader) || !cacheControlHeader.Contains("no-cache");
		}
	}
}