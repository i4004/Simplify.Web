using System;
using System.IO.Abstractions;
using System.Xml.Linq;

namespace AcspNet.Modules
{
	/// <summary>
	/// Provides localizable files reader
	/// </summary>
	public class FileReader : IFileReader
	{
		private readonly string _dataPhysicalPath;
		private readonly string _defaultLanguage;
		private readonly string _currentLanguage;
		private static IFileSystem _fileSystemInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="FileReader"/> class.
		/// </summary>
		/// <param name="dataPhysicalPath">The data physical path.</param>
		/// <param name="defaultLanguage">The default language.</param>
		/// <param name="currentLanguage">The language.</param>
		public FileReader(string dataPhysicalPath, string defaultLanguage, string currentLanguage)
		{
			_dataPhysicalPath = dataPhysicalPath;
			_defaultLanguage = defaultLanguage;
			_currentLanguage = currentLanguage;
		}

		/// <summary>
		/// Gets or sets the file system.
		/// </summary>
		/// <value>
		/// The file system.
		/// </value>
		/// <exception cref="System.ArgumentNullException"></exception>
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
		/// Get file path
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <returns>File path</returns>
		public string GetFilePath(string fileName)
		{
			return GetFilePath(fileName, _currentLanguage);
		}

		/// <summary>
		/// Get extension data file path for specific language
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <param name="language">File language</param>
		/// <returns>File path</returns>
		public string GetFilePath(string fileName, string language)
		{
			if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("extensionsDataFileName");
			if (string.IsNullOrEmpty(language)) throw new ArgumentNullException("language");

			var indexOfPoint = fileName.IndexOf(".", StringComparison.Ordinal);

			if (indexOfPoint == -1)
				return string.Format("{0}{1}.{2}", _dataPhysicalPath,
							fileName, language);

			var fileNameFirstPart = fileName.Substring(0, indexOfPoint);
			var fileNameLastPart = fileName.Substring(indexOfPoint, fileName.Length - indexOfPoint);

			var path = string.Format("{0}{1}.{2}{3}", _dataPhysicalPath, fileNameFirstPart, language, fileNameLastPart);

			return !FileSystem.File.Exists(path)
				? string.Format("{0}{1}.{2}{3}", _dataPhysicalPath, fileNameFirstPart, _defaultLanguage,
					fileNameLastPart)
				: path;
		}

		/// <summary>
		/// Load xml document from a file
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <returns>
		/// Xml document
		/// </returns>
		public XDocument LoadXDocument(string fileName)
		{
			return LoadXDocument(fileName, _currentLanguage);
		}

		/// <summary>
		/// Load xml document from a file with specific language
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <param name="language">File language</param>
		/// <returns>
		/// Xml document
		/// </returns>
		public XDocument LoadXDocument(string fileName, string language)
		{
			if (!fileName.EndsWith(".xml"))
				fileName = fileName + ".xml";

			var filePath = GetFilePath(fileName, language);

			return FileSystem.File.Exists(filePath) ? XDocument.Parse(FileSystem.File.ReadAllText(filePath)) : null;
		}

		/// <summary>
		/// Load text from a extension data file
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <returns>
		/// Text from a file
		/// </returns>
		public string LoadTextDocument(string fileName)
		{
			return LoadTextDocument(fileName, _currentLanguage);
		}

		/// <summary>
		/// Load text from a file with specific language
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <param name="language">File language</param>
		/// <returns>
		/// Text from a file
		/// </returns>
		public string LoadTextDocument(string fileName, string language)
		{
			var filePath = GetFilePath(fileName, language);

			return !FileSystem.File.Exists(filePath) ? null : FileSystem.File.ReadAllText(GetFilePath(fileName, language));
		}
	}
}
