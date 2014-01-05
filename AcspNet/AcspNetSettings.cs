using System.Collections.Specialized;
using System.Configuration;

namespace AcspNet
{
	/// <summary>
	/// ACSP.NET settings
	/// </summary>
	public sealed class AcspNetSettings
	{
		/// <summary>
		/// Default templates directory path, for example: Templates, default value is "Templates"
		/// </summary>
		public string DefaultTemplatesPath { get; private set; }

		/// <summary>
		/// Default site style
		/// </summary>
		public string DefaultStyle { get; private set; }

		/// <summary>
		/// Default language, for example: "en", "ru", "de" etc., default value is "en"
		/// </summary>
		public string DefaultLanguage { get; private set; }

		/// <summary>
		/// Extension data directory path, for example: ExtensionsData, default value is "ExtensionsData"
		/// </summary>
		public string DefaultExtensionDataPath { get; private set; }

		/// <summary>
		/// Site default page
		/// </summary>
		/// <value>
		/// Site default page
		/// </value>
		public string IndexPage { get; private set; }

		/// <summary>
		/// Gets or sets the master page template file name
		/// </summary>
		/// <value>
		/// The name of the master page template file
		/// </value>
		public string DefaultMasterTemplateFileName { get; private set; }

		/// <summary>
		/// Gets or sets the master template main content variable name.
		/// </summary>
		/// <value>
		/// The  master template main content variable name.
		/// </value>
		public string DefaultMainContentVariableName { get; private set; }

		/// <summary>
		/// Gets or sets the master template title variable name.
		/// </summary>
		/// <value>
		/// The title variable name.
		/// </value>
		public string DefaultTitleVariableName { get; private set; }

		/// <summary>
		/// Gets a value indicating whether templates memory cache enabled or disabled.
		/// </summary>
		/// <value>
		/// <c>true</c> if templates memory cache enabled; otherwise, <c>false</c>.
		/// </value>
		public bool TemplatesMemoryCache { get; private set; }

		/// <summary>
		/// Gets or sets a value indicating whether site title postfix should be set automatically
		/// </summary>
		/// <value>
		/// <c>true</c> if [disable automatic site title set]; otherwise, <c>false</c>.
		/// </value>
		public bool DisableAutomaticSiteTitleSet { get; private set; }

		/// <summary>
		/// Gets or sets a value indicating whether internal AcspNet extensions from AcspNet.Extensions.Executable should be disabled
		/// </summary>
		public bool DisableAcspInternalExtensions { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="AcspNetSettings"/> class.
		/// </summary>
		internal AcspNetSettings()
		{
			DefaultTitleVariableName = "Title";
			DefaultMainContentVariableName = "MainContent";
			DefaultMasterTemplateFileName = "Index.tpl";
			IndexPage = "Index.aspx";
			DefaultExtensionDataPath = "ExtensionsData";
			DefaultLanguage = "en";
			DefaultStyle = "Main";
			DefaultTemplatesPath = "Templates";

			var config = ConfigurationManager.GetSection("AcspNetSettings") as NameValueCollection;

			if (config != null)
			{
				if (!string.IsNullOrEmpty(config["DefaultTemplatesPath"]))
					DefaultTemplatesPath = config["DefaultTemplatesPath"];

				if (!string.IsNullOrEmpty(config["DefaultStyle"]))
					DefaultStyle = config["DefaultStyle"];

				if (!string.IsNullOrEmpty(config["DefaultLanguage"]))
					DefaultLanguage = config["DefaultLanguage"];

				if (!string.IsNullOrEmpty(config["DefaultExtensionDataPath"]))
					DefaultExtensionDataPath = config["DefaultExtensionDataPath"];

				if (!string.IsNullOrEmpty(config["IndexPage"]))
					IndexPage = config["IndexPage"];

				if (!string.IsNullOrEmpty(config["DefaultMasterTemplateFileName"]))
					DefaultMasterTemplateFileName = config["DefaultMasterTemplateFileName"];

				if (!string.IsNullOrEmpty(config["DefaultMainContentVariableName"]))
					DefaultMainContentVariableName = config["DefaultMainContentVariableName"];

				if (!string.IsNullOrEmpty(config["DefaultTitleVariableName"]))
					DefaultTitleVariableName = config["DefaultTitleVariableName"];

				if (!string.IsNullOrEmpty(config["TemplatesMemoryCache"]))
				{
					bool buffer;

					if (bool.TryParse(config["TemplatesMemoryCache"], out buffer))
						TemplatesMemoryCache = buffer;
				}

				if (!string.IsNullOrEmpty(config["DisableAutomaticSiteTitleSet"]))
				{
					bool buffer;

					if (bool.TryParse(config["DisableAutomaticSiteTitleSet"], out buffer))
						DisableAutomaticSiteTitleSet = buffer;
				}

				if (!string.IsNullOrEmpty(config["DisableAcspInternalExtensions"]))
				{
					bool buffer;

					if (bool.TryParse(config["DisableAcspInternalExtensions"], out buffer))
						DisableAcspInternalExtensions = buffer;
				}			
			}
		}
	}
}
