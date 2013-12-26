using System.Collections.Specialized;
using System.Configuration;

namespace AcspNet
{
	/// <summary>
	/// ACSP.NET settings
	/// </summary>
	public sealed class AcspNetSettings
	{
		private readonly string _defaultTemplatesDir = "Templates";
		private readonly string _defaultLanguage = "en";
		private readonly string _extensionsDataDir = "ExtensionsData";
		private readonly string _indexPage = "Index.aspx";
		private readonly string _masterTemplateFileName = "Index.tpl";
		private readonly string _mainContentVariableName = "MainContent";
		private readonly string _titleVariableName = "Title";
		private readonly bool _templatesMemoryCache;
	
		/// <summary>
		/// Default templates directory, for example: Templates, default value is "Templates"
		/// </summary>
		public string DefaultTemplatesDir
		{
			get
			{
				return _defaultTemplatesDir;
			}
		}

		/// <summary>
		/// Default site style
		/// </summary>
		public string DefaultStyle { get; private set; }
		
		/// <summary>
		/// Default language, for example: "en", "ru", "de" etc., default value is "en"
		/// </summary>
		public string DefaultLanguage 
		{
			get
			{
				return _defaultLanguage;
			}
		}

		/// <summary>
		/// Extension data path, for example: ExtensionsData, default value is "ExtensionsData"
		/// </summary>
		public string ExtensionDataDir
		{
			get
			{
				return _extensionsDataDir;
			}
		}

		/// <summary>
		/// Site default page
		/// </summary>
		/// <value>
		/// Site default page
		/// </value>
		public string IndexPage
		{
			get
			{
				return _indexPage;
			}
		}

		/// <summary>
		/// Gets or sets the master page template file name
		/// </summary>
		/// <value>
		/// The name of the master page template file
		/// </value>
		public string MasterTemplateFileName
		{
			get { return _masterTemplateFileName; }
		}

		/// <summary>
		/// Gets or sets the master template main content variable name.
		/// </summary>
		/// <value>
		/// The  master template main content variable name.
		/// </value>
		public string MainContentVariableName
		{
			get { return _mainContentVariableName; }
		}

		/// <summary>
		/// Gets or sets the master template title variable name.
		/// </summary>
		/// <value>
		/// The title variable name.
		/// </value>
		public string TitleVariableName
		{
			get { return _titleVariableName; }
		}

		/// <summary>
		/// Gets a value indicating whether templates memory cache enabled or disabled.
		/// </summary>
		/// <value>
		/// <c>true</c> if templates memory cache enabled; otherwise, <c>false</c>.
		/// </value>
		public bool TemplatesMemoryCache
		{
			get
			{
				return _templatesMemoryCache;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AcspNetSettings"/> class.
		/// </summary>
		internal AcspNetSettings()
		{
			var config = ConfigurationManager.GetSection("AcspNetSettings") as NameValueCollection;

			if (config == null) return;

			if (!string.IsNullOrEmpty(config["DefaultTemplatesDir"]))
				_defaultTemplatesDir = config["DefaultTemplatesDir"];

			if (!string.IsNullOrEmpty(config["DefaultStyle"]))
				DefaultStyle = config["DefaultStyle"];

			if (!string.IsNullOrEmpty(config["DefaultLanguage"]))
				_defaultLanguage = config["DefaultLanguage"];

			if (!string.IsNullOrEmpty(config["ExtensionDataDir"]))
				_extensionsDataDir = config["ExtensionDataDir"];

			if (!string.IsNullOrEmpty(config["IndexPage"]))
				_indexPage = config["IndexPage"];

			if (!string.IsNullOrEmpty(config["MasterTemplateFileName"]))
				_masterTemplateFileName = config["MasterTemplateFileName"];

			if (!string.IsNullOrEmpty(config["MainContentVariableName"]))
				_mainContentVariableName = config["MainContentVariableName"];

			if (!string.IsNullOrEmpty(config["TitleVariableName"]))
				_titleVariableName = config["TitleVariableName"];
		
			if (!string.IsNullOrEmpty(config["TemplatesMemoryCache"]))
				_templatesMemoryCache = bool.Parse(config["TemplatesMemoryCache"]);	
		}
	}
}
