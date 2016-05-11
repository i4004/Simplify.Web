using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using Simplify.Web.Owin;

namespace Simplify.Web.Core.StaticFiles
{
	/// <summary>
	/// Provides static file handler
	/// </summary>
	/// <seealso cref="Simplify.Web.Core.StaticFiles.IStaticFileHandler" />
	public class StaticFileHandler : IStaticFileHandler
	{
		private readonly IList<string> _staticFilesPaths;
		private readonly string _sitePhysicalPath;
		private static IFileSystem _fileSystemInstance;

		/// <summary>
		/// Gets or sets the file system.
		/// </summary>
		/// <value>
		/// The file system.
		/// </value>
		/// <exception cref="ArgumentNullException"></exception>
		public static IFileSystem FileSystem
		{
			get { return _fileSystemInstance ?? (_fileSystemInstance = new FileSystem()); }

			set
			{
				if (value == null)
					throw new ArgumentNullException();

				_fileSystemInstance = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StaticFileHandler"/> class.
		/// </summary>
		/// <param name="staticFilesPaths">The static files paths.</param>
		/// <param name="sitePhysicalPath">The site physical path.</param>
		public StaticFileHandler(IList<string> staticFilesPaths, string sitePhysicalPath)
		{
			_staticFilesPaths = staticFilesPaths;
			_sitePhysicalPath = sitePhysicalPath;
		}

		/// <summary>
		/// Determines whether  relative file path is a static file route path.
		/// </summary>
		/// <param name="relativeFilePath">The relative file path.</param>
		/// <returns></returns>
		public bool IsStaticFileRoutePath(string relativeFilePath)
		{
			return _staticFilesPaths.Where(relativeFilePath.StartsWith).Any(path => FileSystem.File.Exists(_sitePhysicalPath + relativeFilePath));
		}

		/// <summary>
		/// Determines whether static file can be used from cache by browser.
		/// </summary>
		/// <param name="cacheControlHeader">The cache control header.</param>
		/// <param name="ifModifiedSinceHeader">If modified since header.</param>
		/// <param name="fileLastModifiedTime">The file last modified time.</param>
		/// <returns></returns>
		public bool IsFileCanBeUsedFromCache(string cacheControlHeader, DateTime? ifModifiedSinceHeader, DateTime fileLastModifiedTime)
		{
			return OwinRequestHelper.IsCacheCanBeUsed(cacheControlHeader) && ifModifiedSinceHeader != null && fileLastModifiedTime <= ifModifiedSinceHeader.Value;
		}

		/// <summary>
		/// Gets the file last modification time.
		/// </summary>
		/// <param name="relativeFilePath">The relative file path.</param>
		/// <returns></returns>
		public DateTime GetFileLastModificationTime(string relativeFilePath)
		{
			return TrimMilliseconds(FileSystem.File.GetLastWriteTimeUtc(relativeFilePath));
		}

		/// <summary>
		/// Gets the file data.
		/// </summary>
		/// <param name="relativeFilePath">The relative file path.</param>
		/// <returns></returns>
		public byte[] GetFileData(string relativeFilePath)
		{
			return FileSystem.File.ReadAllBytes(_sitePhysicalPath + relativeFilePath);
		}

		private static DateTime TrimMilliseconds(DateTime dt)
		{
			return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, 0);
		}
	}
}