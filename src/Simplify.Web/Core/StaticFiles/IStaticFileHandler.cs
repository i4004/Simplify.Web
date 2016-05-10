using System;

namespace Simplify.Web.Core.StaticFiles
{
	public interface IStaticFileHandler
	{
		bool IsStaticFileRoutePath(string relativeFilePath);

		bool IsFileCanBeUsedFromCache(string cacheControlHeader, DateTime? ifModifiedSinceHeader, DateTime fileLastModifiedTime);

		DateTime GetFileLastModificationTime(string relativeFilePath);

		byte[] GetFileData(string relativeFilePath);
	}
}