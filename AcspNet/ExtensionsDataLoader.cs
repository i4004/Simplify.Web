using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace AcspNet
{
	/// <summary>
	/// Extension for loading data from extensions data directory
	/// </summary>
	public sealed class ExtensionsDataLoader
	{
		private readonly Environment _ev;

		public ExtensionsDataLoader(Environment ev)
		{
			_ev = ev;
		}

		/// <summary>
		/// Get extension data file path
		/// </summary>
		/// <param name="extensionsDataFileName">Extension data file name</param>
		/// <returns>Extension data file path</returns>
		public string GetFilePath(string extensionsDataFileName)
		{
			return GetFilePath(extensionsDataFileName, _ev.Language);
		}

		/// <summary>
		/// Get extension data file path for specific language
		/// </summary>
		/// <param name="extensionsDataFileName">Extension data file name</param>
		/// <param name="language">Extension data file language</param>
		/// <returns>Extension data file path</returns>
		public static string GetFilePath(string extensionsDataFileName, string language)
		{
			var indexOfPoint = extensionsDataFileName.IndexOf(".", System.StringComparison.Ordinal);

			if (indexOfPoint != -1)
			{
				var extensionsDataFileNameFirstPart = extensionsDataFileName.Substring(0, indexOfPoint);
				var extensionsDataFileNameLastPart = extensionsDataFileName.Substring(indexOfPoint, extensionsDataFileName.Length - indexOfPoint);

				var path = string.Format("{0}{1}/{2}.{3}{4}", Manager.SitePhysicalPath, Manager.Settings.ExtensionDataDir, extensionsDataFileNameFirstPart, language, extensionsDataFileNameLastPart);

				return !File.Exists(path) ? string.Format("{0}{1}/{2}.{3}{4}", Manager.SitePhysicalPath, Manager.Settings.ExtensionDataDir, extensionsDataFileNameFirstPart, Manager.Settings.DefaultLanguage, extensionsDataFileNameLastPart) : path;
			}

			return string.Format("{0}{1}/{2}.{3}", Manager.SitePhysicalPath, Manager.Settings.ExtensionDataDir, extensionsDataFileName, language);
		}

		/// <summary>
		/// Load xml document from a extension data file
		/// </summary>
		/// <param name="extensionsDataFileName">Extension data file name</param>
		/// <returns>Xml document</returns>
		public XmlDocument LoadXmlDocument(string extensionsDataFileName)
		{
			return LoadXmlDocument(extensionsDataFileName, _ev.Language);
		}

		/// <summary>
		/// Load xml document from a extension data file with specific language
		/// </summary>
		/// <param name="extensionsDataFileName">Extension data file name</param>
		/// <param name="language">Extension data file language</param>
		/// <returns>Xml document</returns>
		public static XmlDocument LoadXmlDocument(string extensionsDataFileName, string language)
		{
			XmlDocument xmlDoc = null;
			var filePath = GetFilePath(extensionsDataFileName, language);

			if (File.Exists(filePath))
			{
				xmlDoc = new XmlDocument();
				xmlDoc.Load(filePath);
			}

			return xmlDoc;
		}

		/// <summary>
		/// Load xml document from a extension data file
		/// </summary>
		/// <param name="extensionsDataFileName">Extension data file name</param>
		/// <returns>Xml document</returns>
		public XDocument LoadXDocument(string extensionsDataFileName)
		{
			return LoadXDocument(extensionsDataFileName, _ev.Language);
		}

		/// <summary>
		/// Load xml document from a extension data file with specific language
		/// </summary>
		/// <param name="extensionsDataFileName">Extension data file name</param>
		/// <param name="language">Extension data file language</param>
		/// <returns>Xml document</returns>
		public static XDocument LoadXDocument(string extensionsDataFileName, string language)
		{
			var filePath = GetFilePath(extensionsDataFileName, language);

			return File.Exists(filePath) ? XDocument.Load(filePath) : null;
		}

		/// <summary>
		/// Load text from a extension data file
		/// </summary>
		/// <param name="extensionsDataFileName">Extension data file name</param>
		/// <returns>Text from a extension data file</returns>
		public string LoadTextDocument(string extensionsDataFileName)
		{
			return LoadTextDocument(extensionsDataFileName, _ev.Language);
		}

		/// <summary>
		/// Load text from a extension data file with specific language
		/// </summary>
		/// <param name="extensionsDataFileName">Extension data file name</param>
		/// <param name="language">Extension data file language</param>
		/// <returns>Text from a extension data file</returns>
		public static string LoadTextDocument(string extensionsDataFileName, string language)
		{
			var filePath = GetFilePath(extensionsDataFileName, language);

			if (!File.Exists(filePath)) return "";

			using (var sr = new StreamReader(GetFilePath(extensionsDataFileName, language)))
				return sr.ReadToEnd();
		}
	}
}
