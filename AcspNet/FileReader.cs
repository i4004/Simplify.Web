using System;
using System.IO.Abstractions;
using System.Xml.Linq;

namespace AcspNet
{
	/// <summary>
	/// Loads text and XML localized files
	/// </summary>
	public sealed class FileReader : IFileReader
	{
		private static IFileSystem _fileSystemInstance;

		private readonly string _dataPath;
		private readonly string _sitePhysicalPath;
		private readonly string _language;
		private readonly string _defaultLanguage;

		/// <summary>
		/// Initializes a new instance of the <see cref="FileReader"/> class.
		/// </summary>
		internal FileReader(string dataPath, string sitePhysicalPath, string language, string defaultLanguage)
		{
			_dataPath = dataPath;
			_sitePhysicalPath = sitePhysicalPath;
			_language = language;
			_defaultLanguage = defaultLanguage;
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
		/// Gets the current language.
		/// </summary>
		/// <value>
		/// The current language.
		/// </value>
		public string Language
		{
			get { return _language; }
		}

		/// <summary>
		/// Gets the default language.
		/// </summary>
		/// <value>
		/// The default language.
		/// </value>
		public string DefaultLanguage
		{
			get { return _defaultLanguage; }
		}

		/// <summary>
		/// Get extension data file path
		/// </summary>
		/// <param name="extensionsDataFileName">Extension data file name</param>
		/// <returns>Extension data file path</returns>
		public string GetFilePath(string extensionsDataFileName)
		{
			return GetFilePath(extensionsDataFileName, Language);
		}

		/// <summary>
		/// Get extension data file path for specific language
		/// </summary>
		/// <param name="extensionsDataFileName">Extension data file name</param>
		/// <param name="language">Extension data file language</param>
		/// <returns>Extension data file path</returns>
		public string GetFilePath(string extensionsDataFileName, string language)
		{
			if (string.IsNullOrEmpty(extensionsDataFileName)) throw new ArgumentNullException("extensionsDataFileName");
			if (string.IsNullOrEmpty(language)) throw new ArgumentNullException("language");

			var indexOfPoint = extensionsDataFileName.IndexOf(".", StringComparison.Ordinal);

			if (indexOfPoint == -1)
				return string.Format("{0}/{1}/{2}.{3}", _sitePhysicalPath, _dataPath,
					extensionsDataFileName, language);

			var extensionsDataFileNameFirstPart = extensionsDataFileName.Substring(0, indexOfPoint);
			var extensionsDataFileNameLastPart = extensionsDataFileName.Substring(indexOfPoint, extensionsDataFileName.Length - indexOfPoint);

			var path = string.Format("{0}/{1}/{2}.{3}{4}", _sitePhysicalPath, _dataPath, extensionsDataFileNameFirstPart, language, extensionsDataFileNameLastPart);

			return !FileSystem.File.Exists(path)
				? string.Format("{0}/{1}/{2}.{3}{4}", _sitePhysicalPath, _dataPath, extensionsDataFileNameFirstPart, DefaultLanguage,
					extensionsDataFileNameLastPart)
				: path;
		}

		/// <summary>
		/// Load xml document from a extension data file
		/// </summary>
		/// <param name="extensionsDataFileName">Extension data file name</param>
		/// <returns>Xml document</returns>
		public XDocument LoadXDocument(string extensionsDataFileName)
		{
			return LoadXDocument(extensionsDataFileName, Language);
		}

		/// <summary>
		/// Load xml document from a extension data file with specific language
		/// </summary>
		/// <param name="extensionsDataFileName">Extension data file name</param>
		/// <param name="language">Extension data file language</param>
		/// <returns>Xml document</returns>
		public XDocument LoadXDocument(string extensionsDataFileName, string language)
		{
			var filePath = GetFilePath(extensionsDataFileName, language);

			return FileSystem.File.Exists(filePath) ? XDocument.Parse(FileSystem.File.ReadAllText(filePath)) : null;
		}

		/// <summary>
		/// Load text from a extension data file
		/// </summary>
		/// <param name="extensionsDataFileName">Extension data file name</param>
		/// <returns>Text from a extension data file</returns>
		public string LoadTextDocument(string extensionsDataFileName)
		{
			return LoadTextDocument(extensionsDataFileName, Language);
		}

		/// <summary>
		/// Load text from a extension data file with specific language
		/// </summary>
		/// <param name="extensionsDataFileName">Extension data file name</param>
		/// <param name="language">Extension data file language</param>
		/// <returns>Text from a extension data file</returns>
		public string LoadTextDocument(string extensionsDataFileName, string language)
		{
			var filePath = GetFilePath(extensionsDataFileName, language);

			return !FileSystem.File.Exists(filePath) ? null : FileSystem.File.ReadAllText(GetFilePath(extensionsDataFileName, language));
		}
	}
}
