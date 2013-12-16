using System.Collections.Specialized;
using System.Configuration;

namespace AcspNet.Extensions.Library
{
	/// <summary>
	/// ACSP.NET base settings class
	/// </summary>
	[Priority(-11)]
	[Version("1.1.1")]
	public sealed class EngineSettings : ILibExtension
	{
		/// <summary>
		/// Default templates path, for example: templates
		/// </summary>
		public static string DefaultTemplatesPath { get; private set; }
		/// <summary>
		/// Default site style
		/// </summary>
		public static string DefaultSiteStyle { get; private set; }
		/// <summary>
		/// Default language, for example: "en", "ru", "de" etc.
		/// </summary>
		public static string DefaultLanguage { get; private set; }
		/// <summary>
		/// Extension data path, for example: ExtensionsData
		/// </summary>
		public static string ExtensionDataPath { get; private set; }

		/// <summary>
		/// Initializes the library extension.
		/// </summary>
		/// <param name="manager">The manager.</param>
		public void Initialize(Manager manager)
		{
			var siteConfigurationSection = ConfigurationManager.GetSection("EngineSettings") as NameValueCollection;

			if (siteConfigurationSection == null) return;

			DefaultTemplatesPath = siteConfigurationSection["DefaultTemplatesPath"];
			DefaultSiteStyle = siteConfigurationSection["DefaultSiteStyle"];
			DefaultLanguage = siteConfigurationSection["DefaultLanguage"];
			ExtensionDataPath = siteConfigurationSection["ExtensionDataPath"];
		}
	}
}
