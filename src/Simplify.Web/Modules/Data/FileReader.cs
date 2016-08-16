using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Xml.Linq;

namespace Simplify.Web.Modules.Data
{
	/// <summary>
	/// Provides localizable files reader, reads the files from data folder
	/// </summary>
	public class FileReader : IFileReader
	{
		private static readonly IDictionary<KeyValuePair<string, string>, XDocument> XmlCache = new Dictionary<KeyValuePair<string, string>, XDocument>();
		private static readonly IDictionary<KeyValuePair<string, string>, string> TextCache = new Dictionary<KeyValuePair<string, string>, string>();
		private static readonly object Locker = new object();

		private readonly string _dataPhysicalPath;
		private readonly string _defaultLanguage;
		private readonly ILanguageManagerProvider _languageManagerProvider;
		private readonly bool _disableCache;

		private ILanguageManager _languageManager;
		private static IFileSystem _fileSystemInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="FileReader" /> class.
		/// </summary>
		/// <param name="dataPhysicalPath">The data physical path.</param>
		/// <param name="defaultLanguage">The default language.</param>
		/// <param name="languageManagerProvider">The language manager provider.</param>
		/// <param name="disableCache">Disable FileReader cache.</param>
		public FileReader(string dataPhysicalPath, string defaultLanguage, ILanguageManagerProvider languageManagerProvider, bool disableCache = false)
		{
			_dataPhysicalPath = dataPhysicalPath;
			_defaultLanguage = defaultLanguage;
			_languageManagerProvider = languageManagerProvider;
			_disableCache = disableCache;
		}

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
		/// Clears the cache.
		/// </summary>
		public static void ClearCache()
		{
			lock (Locker)
			{
				XmlCache.Clear();
				TextCache.Clear();
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

			return $"{_dataPhysicalPath}{fileNameFirstPart}.{language}{fileNameLastPart}";
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
		/// <param name="memoryCache">Load file from memory cache if possible.</param>
		/// <returns>
		/// Xml document
		/// </returns>
		public XDocument LoadXDocument(string fileName, bool memoryCache = false)
		{
			return LoadXDocument(fileName, _languageManager.Language, memoryCache);
		}

		/// <summary>
		/// Load xml document from a file with specific language
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <param name="language">File language</param>
		/// <param name="memoryCache">Load file from memory cache if possible.</param>
		/// <returns>
		/// Xml document
		/// </returns>
		public XDocument LoadXDocument(string fileName, string language, bool memoryCache = false)
		{
			if (!memoryCache || _disableCache)
			{
				var filePath = GetFilePath(fileName, language);

				if (FileSystem.File.Exists(filePath))
					return XDocument.Parse(FileSystem.File.ReadAllText(filePath));

				filePath = GetFilePath(fileName, _defaultLanguage);

				return !FileSystem.File.Exists(filePath)
					? null
					: XDocument.Parse(FileSystem.File.ReadAllText(filePath));
			}

			XDocument data;

			if (TryToLoadXDocumentFromCache(fileName, language, out data))
				return data;

			lock (Locker)
			{
				if (TryToLoadXDocumentFromCache(fileName, language, out data))
					return data;

				var filePath = GetFilePath(fileName, language);

				if (FileSystem.File.Exists(filePath))
				{
					data = XDocument.Parse(FileSystem.File.ReadAllText(filePath));
					XmlCache.Add(new KeyValuePair<string, string>(fileName, language), data);
					return data;
				}

				filePath = GetFilePath(fileName, _defaultLanguage);

				if (!FileSystem.File.Exists(filePath)) return null;

				data = XDocument.Parse(FileSystem.File.ReadAllText(filePath));
				XmlCache.Add(new KeyValuePair<string, string>(fileName, _defaultLanguage), data);
				return data;
			}
		}

		/// <summary>
		/// Load text from a extension data file
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <param name="memoryCache">Load file from memory cache if possible.</param>
		/// <returns>
		/// Text from a file
		/// </returns>
		public string LoadTextDocument(string fileName, bool memoryCache = false)
		{
			return LoadTextDocument(fileName, _languageManager.Language, memoryCache);
		}

		/// <summary>
		/// Load text from a file with specific language
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <param name="language">File language</param>
		/// <param name="memoryCache">Load file from memory cache if possible.</param>
		/// <returns>
		/// Text from a file
		/// </returns>
		public string LoadTextDocument(string fileName, string language, bool memoryCache = false)
		{
			if (!memoryCache || _disableCache)
			{
				var filePath = GetFilePath(fileName, language);

				if (FileSystem.File.Exists(filePath))
					return FileSystem.File.ReadAllText(filePath);

				filePath = GetFilePath(fileName, _defaultLanguage);

				return !FileSystem.File.Exists(filePath)
					? null
					: FileSystem.File.ReadAllText(filePath);
			}

			string data;

			if (TryToLoadTextDocumentFromCache(fileName, language, out data))
				return data;

			lock (Locker)
			{
				if (TryToLoadTextDocumentFromCache(fileName, language, out data))
					return data;

				var filePath = GetFilePath(fileName, language);

				if (FileSystem.File.Exists(filePath))
				{
					data = FileSystem.File.ReadAllText(filePath);
					TextCache.Add(new KeyValuePair<string, string>(fileName, language), data);
					return data;
				}

				filePath = GetFilePath(fileName, _defaultLanguage);

				if (!FileSystem.File.Exists(filePath)) return null;

				data = FileSystem.File.ReadAllText(filePath);
				TextCache.Add(new KeyValuePair<string, string>(fileName, _defaultLanguage), data);
				return data;
			}
		}

		private bool TryToLoadXDocumentFromCache(string fileName, string language, out XDocument data)
		{
			data = null;

			var cacheItem = XmlCache.FirstOrDefault(x => x.Key.Key == fileName && x.Key.Value == language);

			if (!cacheItem.Equals(default(KeyValuePair<KeyValuePair<string, string>, XDocument>)))
			{
				data = cacheItem.Value;
				return true;
			}

			cacheItem = XmlCache.FirstOrDefault(x => x.Key.Key == fileName && x.Key.Value == _defaultLanguage);

			if (cacheItem.Equals(default(KeyValuePair<KeyValuePair<string, string>, XDocument>)))
				return false;

			data = cacheItem.Value;
			return true;
		}

		private bool TryToLoadTextDocumentFromCache(string fileName, string language, out string data)
		{
			data = null;

			var cacheItem = TextCache.FirstOrDefault(x => x.Key.Key == fileName && x.Key.Value == language);

			if (!cacheItem.Equals(default(KeyValuePair<KeyValuePair<string, string>, string>)))
			{
				data = cacheItem.Value;
				return true;
			}

			cacheItem = TextCache.FirstOrDefault(x => x.Key.Key == fileName && x.Key.Value == _defaultLanguage);

			if (cacheItem.Equals(default(KeyValuePair<KeyValuePair<string, string>, string>)))
				return false;

			data = cacheItem.Value;
			return true;
		}
	}
}