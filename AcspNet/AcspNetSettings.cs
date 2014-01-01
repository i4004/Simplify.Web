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
		/// Default templates directory, for example: Templates, default value is "Templates"
		/// </summary>
		public string DefaultTemplatesDir { get; private set; }

		/// <summary>
		/// Default site style
		/// </summary>
		public string DefaultStyle { get; set; }

		/// <summary>
		/// Default language, for example: "en", "ru", "de" etc., default value is "en"
		/// </summary>
		public string DefaultLanguage { get; private set; }

		/// <summary>
		/// Extension data path, for example: ExtensionsData, default value is "ExtensionsData"
		/// </summary>
		public string ExtensionDataDir { get; private set; }

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
		public string MasterTemplateFileName { get; private set; }

		/// <summary>
		/// Gets or sets the master template main content variable name.
		/// </summary>
		/// <value>
		/// The  master template main content variable name.
		/// </value>
		public string MainContentVariableName { get; private set; }

		/// <summary>
		/// Gets or sets the master template title variable name.
		/// </summary>
		/// <value>
		/// The title variable name.
		/// </value>
		public string TitleVariableName { get; private set; }

		/// <summary>
		/// Gets a value indicating whether templates memory cache enabled or disabled.
		/// </summary>
		/// <value>
		/// <c>true</c> if templates memory cache enabled; otherwise, <c>false</c>.
		/// </value>
		public bool TemplatesMemoryCache { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="AcspNetSettings"/> class.
		/// </summary>
		internal AcspNetSettings()
		{
			TitleVariableName = "Title";
			MainContentVariableName = "MainContent";
			MasterTemplateFileName = "Index.tpl";
			IndexPage = "Index.aspx";
			ExtensionDataDir = "ExtensionsData";
			DefaultLanguage = "en";
			DefaultStyle = "Main";
			DefaultTemplatesDir = "Templates";

			var config = ConfigurationManager.GetSection("AcspNetSettings") as NameValueCollection;

			if (config != null)
			{
				if (!string.IsNullOrEmpty(config["DefaultTemplatesDir"]))
					DefaultTemplatesDir = config["DefaultTemplatesDir"];

				if (!string.IsNullOrEmpty(config["DefaultStyle"]))
					DefaultStyle = config["DefaultStyle"];

				if (!string.IsNullOrEmpty(config["DefaultLanguage"]))
					DefaultLanguage = config["DefaultLanguage"];

				if (!string.IsNullOrEmpty(config["ExtensionDataDir"]))
					ExtensionDataDir = config["ExtensionDataDir"];

				if (!string.IsNullOrEmpty(config["IndexPage"]))
					IndexPage = config["IndexPage"];

				if (!string.IsNullOrEmpty(config["MasterTemplateFileName"]))
					MasterTemplateFileName = config["MasterTemplateFileName"];

				if (!string.IsNullOrEmpty(config["MainContentVariableName"]))
					MainContentVariableName = config["MainContentVariableName"];

				if (!string.IsNullOrEmpty(config["TitleVariableName"]))
					TitleVariableName = config["TitleVariableName"];

				if (!string.IsNullOrEmpty(config["TemplatesMemoryCache"]))
				{
					bool buffer;

					if (bool.TryParse(config["TemplatesMemoryCache"], out buffer))
						TemplatesMemoryCache = buffer;
				}
			}
		}
	}
}
