using System;
using System.Globalization;
using Microsoft.Owin;

namespace Simplify.Web.Owin
{
	/// <summary>
	/// Provides OWIN request helper
	/// </summary>
	public static class OwinRequestHelper
	{
		/// <summary>
		/// Gets If-Modified-Since time header from headers collection.
		/// </summary>
		/// <param name="headers">The headers.</param>
		/// <returns></returns>
		public static DateTime? GetIfModifiedSinceTime(IHeaderDictionary headers)
		{
			DateTime? ifModifiedSinceTime = null;

			if (headers.ContainsKey("If-Modified-Since"))
				ifModifiedSinceTime = DateTime.ParseExact(headers["If-Modified-Since"], "r",
					CultureInfo.InvariantCulture);

			return ifModifiedSinceTime;
		}

		/// <summary>
		/// Determines whether cache can be used by browser.
		/// </summary>
		/// <param name="cacheControlHeader">The cache control header.</param>
		/// <returns></returns>
		public static bool IsCacheCanBeUsed(string cacheControlHeader)
		{
			return string.IsNullOrEmpty(cacheControlHeader) || !cacheControlHeader.Contains("no-cache");
		}
	}
}