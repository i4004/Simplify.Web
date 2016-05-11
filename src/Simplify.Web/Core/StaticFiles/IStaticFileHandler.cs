using System;

namespace Simplify.Web.Core.StaticFiles
{
	/// <summary>
	/// Represent static files handler
	/// </summary>
	public interface IStaticFileHandler
	{
		/// <summary>
		/// Determines whether  relative file path is a static file route path.
		/// </summary>
		/// <param name="relativeFilePath">The relative file path.</param>
		/// <returns></returns>
		bool IsStaticFileRoutePath(string relativeFilePath);

		/// <summary>
		/// Determines whether static file can be used from cache by browser.
		/// </summary>
		/// <param name="cacheControlHeader">The cache control header.</param>
		/// <param name="ifModifiedSinceHeader">If modified since header.</param>
		/// <param name="fileLastModifiedTime">The file last modified time.</param>
		/// <returns></returns>
		bool IsFileCanBeUsedFromCache(string cacheControlHeader, DateTime? ifModifiedSinceHeader, DateTime fileLastModifiedTime);

		/// <summary>
		/// Gets the file last modification time.
		/// </summary>
		/// <param name="relativeFilePath">The relative file path.</param>
		/// <returns></returns>
		DateTime GetFileLastModificationTime(string relativeFilePath);

		/// <summary>
		/// Gets the file data.
		/// </summary>
		/// <param name="relativeFilePath">The relative file path.</param>
		/// <returns></returns>
		byte[] GetFileData(string relativeFilePath);
	}
}