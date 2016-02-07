using System;
using System.IO.Abstractions;
using System.Xml.Linq;

namespace AcspNet.Modules
{
	/// <summary>
	/// Provides localizable files reader, reads the files from data folder
	/// </summary>
	public class FileReader : IFileReader
	{
		private readonly string _dataPhysicalPath;
		private readonly string _defaultLanguage;
		private readonly ILanguageManagerProvider _languageManagerProvider;
		private ILanguageManager _languageManager;
		private static IFileSystem _fileSystemInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="FileReader" /> class.
		/// </summary>
		/// <param name="dataPhysicalPath">The data physical path.</param>
		/// <param name="defaultLanguage">The default language.</param>
		/// <param name="languageManagerProvider">The language manager provider.</param>
		public FileReader(string dataPhysicalPath, string defaultLanguage, ILanguageManagerProvider languageManagerProvider)
		{
			_dataPhysicalPath = dataPhysicalPath;
			_defaultLanguage = defaultLanguage;
			_languageManagerProvider = languageManagerProvider;
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
			return GetFilePath(fileName, _languageManager.Language);
		}

		/// <summary>
		/// Get extension data file path for specific language
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <param name="language">File language</param>
		/// <returns>File path</returns>
		public string GetFilePath(string fileName, string language)
		{
			if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException(nameof(fileName));
			if (string.IsNullOrEmpty(language)) throw new ArgumentNullException(nameof(language));

			var indexOfPoint = fileName.LastIndexOf(".", StringComparison.Ordinal);

			if (indexOfPoint == -1)
				return $"{_dataPhysicalPath}{fileName}.{language}";

			var fileNameFirstPart = fileName.Substring(0, indexOfPoint);
			var fileNameLastPart = fileName.Substring(indexOfPoint, fileName.Length - indexOfPoint);

			var path = $"{_dataPhysicalPath}{fileNameFirstPart}.{language}{fileNameLastPart}";

			return !FileSystem.File.Exists(path)
				? $"{_dataPhysicalPath}{fileNameFirstPart}.{_defaultLanguage}{fileNameLastPart}"
				: path;
		}

		/// <summary>
		/// Setups the file reader.
		/// </summary>
		public void Setup()
		{
			_languageManager = _languageManagerProvider.Get();
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
			return LoadXDocument(fileName, _languageManager.Language);
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
			return LoadTextDocument(fileName, _languageManager.Language);
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
