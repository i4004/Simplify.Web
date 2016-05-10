using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using Simplify.Web.Owin;

namespace Simplify.Web.Core.StaticFiles
{
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

		public StaticFileHandler(IList<string> staticFilesPaths, string sitePhysicalPath)
		{
			_staticFilesPaths = staticFilesPaths;
			_sitePhysicalPath = sitePhysicalPath;
		}

		public bool IsStaticFileRoutePath(string relativeFilePath)
		{
			return _staticFilesPaths.Where(relativeFilePath.StartsWith).Any(path => FileSystem.File.Exists(_sitePhysicalPath + relativeFilePath));
		}

		public bool IsFileCanBeUsedFromCache(string cacheControlHeader, DateTime? ifModifiedSinceHeader, DateTime fileLastModifiedTime)
		{
			return OwinRequestHelper.IsCacheCanBeUsed(cacheControlHeader) && ifModifiedSinceHeader != null && fileLastModifiedTime <= ifModifiedSinceHeader.Value;
		}

		public DateTime GetFileLastModificationTime(string relativeFilePath)
		{
			return TrimMilliseconds(FileSystem.File.GetLastWriteTimeUtc(relativeFilePath));
		}

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