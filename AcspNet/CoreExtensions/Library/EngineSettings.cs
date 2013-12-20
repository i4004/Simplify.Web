using System.Collections.Specialized;
using System.Configuration;

namespace AcspNet.CoreExtensions.Library
{
	/// <summary>
	/// ACSP.NET base settings
	/// </summary>
	[Priority(-11)]
	[Version("1.2")]
	public sealed class EngineSettings : LibExtension
	{
		private static string DefaultTemplatesDirInstance = "Templates";
		private static string DefaultLanguageInstance = "en";
		private static string ExtensionsDataDirInstance = "ExtensionsData";

		/// <summary>
		/// Default templates directory, for example: Templates, default value is "Templates"
		/// </summary>
		public static string DefaultTemplatesDir
		{
			get
			{
				return DefaultTemplatesDirInstance;
			}
		}

		/// <summary>
		/// Default site style
		/// </summary>
		public static string DefaultStyle { get; private set; }
		
		/// <summary>
		/// Default language, for example: "en", "ru", "de" etc., default value is "en"
		/// </summary>
		public static string DefaultLanguage 
		{
			get
			{
				return DefaultLanguageInstance;
			}
		}

		/// <summary>
		/// Extension data path, for example: ExtensionsData, default value is "ExtensionsData"
		/// </summary>
		public static string ExtensionDataDir
		{
			get
			{
				return ExtensionsDataDirInstance;
			}
		}

		/// <summary>
		/// Initializes the library extension.
		/// </summary>
		public override void Initialize()
		{
			var config = ConfigurationManager.GetSection("EngineSettings") as NameValueCollection;

			if (config == null) return;

			if (!string.IsNullOrEmpty(config["DefaultTemplatesDir"]))
				DefaultTemplatesDirInstance = config["DefaultTemplatesDir"];

			if (!string.IsNullOrEmpty(config["DefaultStyle"]))
				DefaultStyle = config["DefaultStyle"];

			if (!string.IsNullOrEmpty(config["DefaultLanguage"]))
				DefaultLanguageInstance = config["DefaultLanguage"];

			if (!string.IsNullOrEmpty(config["ExtensionDataDir"]))
				ExtensionsDataDirInstance = config["ExtensionDataDir"];
		}
	}
}
