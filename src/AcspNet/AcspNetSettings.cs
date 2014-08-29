using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace AcspNet
{
	/// <summary>
	/// AcspNet settings
	/// </summary>
	public sealed class AcspNetSettings : IAcspNetSettings
	{
		/// <summary>
		/// Default language, for example: "en", "ru", "de" etc., default value is "en"
		/// </summary>
		public string DefaultLanguage { get; private set; }

		/// <summary>
		/// Gets a value indicating whether browser language should be accepted
		/// </summary>
		/// <value>
		/// <c>true</c> if  browser language should be accepted; otherwise, <c>false</c>.
		/// </value>
		public bool AcceptBrowserLanguage { get; private set; }

		/// <summary>
		/// Default templates directory path, for example: Templates, default value is "Templates"
		/// </summary>
		public string DefaultTemplatesPath { get; private set; }

		/// <summary>
		/// Gets a value indicating whether templates memory cache enabled or disabled.
		/// </summary>
		/// <value>
		/// <c>true</c> if templates memory cache enabled; otherwise, <c>false</c>.
		/// </value>
		public bool TemplatesMemoryCache { get; private set; }

		/// <summary>
		/// Gets a value indicating whether all templates should be loaded from assembly
		/// </summary>
		/// <value>
		/// <c>true</c> if all templates should be loaded from assembly; otherwise, <c>false</c>.
		/// </value>
		public bool LoadTemplatesFromAssembly { get; private set; }

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
		/// Default site style
		/// </summary>
		public string DefaultStyle { get; private set; }
		/// <summary>
		/// Gets a value indicating whether string table memory cache enabled or disabled.
		/// </summary>
		/// <value>
		/// <c>true</c> if string table memory cache enabled; otherwise, <c>false</c>.
		/// </value>
		public bool StringTableMemoryCache { get; private set; }

		/// <summary>
		/// Data directory path, default value is "App_Data"
		/// </summary>
		public string DataPath { get; private set; }

		/// <summary>
		/// Gets the static files paths.
		/// </summary>
		/// <value>
		/// The static files paths.
		/// </value>
		public IList<string> StaticFilesPaths { get; private set; }

		/// <summary>
		/// Gets or sets a value indicating whether site title postfix should be set automatically
		/// </summary>
		public bool DisableAutomaticSiteTitleSet { get; private set; }

		/// <summary>
		/// Gets a value indicating whether exception details should be hide when some unhandled exception occured.
		/// </summary>
		public bool HideExceptionDetails { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="AcspNetSettings"/> class.
		/// </summary>
		public AcspNetSettings()
		{
			DefaultLanguage = "en";

			DefaultTemplatesPath = "Templates";
			DefaultMasterTemplateFileName = "Master.tpl";
			DefaultMainContentVariableName = "MainContent";
			DefaultTitleVariableName = "Title";

			DefaultStyle = "Main";

			DataPath = "App_Data";

			StaticFilesPaths = new List<string> {"Styles", "Scripts", "Images", "Content"};

			HideExceptionDetails = false;

			var config = ConfigurationManager.GetSection("AcspNetSettings") as NameValueCollection;

			if (config != null)
			{
				LoadLanguageManagerSettings(config);
				LoadTemplatesSettings(config);
				LoadOtherSettings(config);
			}
		}

		private void LoadLanguageManagerSettings(NameValueCollection config)
		{
			if (!string.IsNullOrEmpty(config["DefaultLanguage"]))
				DefaultLanguage = config["DefaultLanguage"];

			if (!string.IsNullOrEmpty(config["AcceptBrowserLanguage"]))
			{
				bool buffer;

				if (bool.TryParse(config["AcceptBrowserLanguage"], out buffer))
					AcceptBrowserLanguage = buffer;
			}
		}

		private void LoadTemplatesSettings(NameValueCollection config)
		{
			if (!string.IsNullOrEmpty(config["DefaultTemplatesPath"]))
				DefaultTemplatesPath = config["DefaultTemplatesPath"];

			if (!string.IsNullOrEmpty(config["TemplatesMemoryCache"]))
			{
				bool buffer;

				if (bool.TryParse(config["TemplatesMemoryCache"], out buffer))
					TemplatesMemoryCache = buffer;
			}

			if (!string.IsNullOrEmpty(config["DefaultMasterTemplateFileName"]))
				DefaultMasterTemplateFileName = config["DefaultMasterTemplateFileName"];

			if (!string.IsNullOrEmpty(config["DefaultMainContentVariableName"]))
				DefaultMainContentVariableName = config["DefaultMainContentVariableName"];

			if (!string.IsNullOrEmpty(config["DefaultTitleVariableName"]))
				DefaultTitleVariableName = config["DefaultTitleVariableName"];
		}

		private void LoadOtherSettings(NameValueCollection config)
		{
			if (!string.IsNullOrEmpty(config["DefaultStyle"]))
				DefaultStyle = config["DefaultStyle"];

			if (!string.IsNullOrEmpty(config["StringTableMemoryCache"]))
			{
				bool buffer;

				if (bool.TryParse(config["StringTableMemoryCache"], out buffer))
					StringTableMemoryCache = buffer;
			}
			
			if (!string.IsNullOrEmpty(config["DefaultDataPath"]))
				DataPath = config["DefaultDataPath"];

			if (!string.IsNullOrEmpty(config["StaticFilesPaths"]))
			{
				StaticFilesPaths.Clear();
				var items = config["StaticFilesPaths"].Replace(" ", "").Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

				foreach (var item in items)
					StaticFilesPaths.Add(item);
			}

			if (!string.IsNullOrEmpty(config["DisableAutomaticSiteTitleSet"]))
			{
				bool buffer;

				if (bool.TryParse(config["DisableAutomaticSiteTitleSet"], out buffer))
					DisableAutomaticSiteTitleSet = buffer;
			}

			if (!string.IsNullOrEmpty(config["HideExceptionDetails"]))
			{
				bool buffer;

				if (bool.TryParse(config["HideExceptionDetails"], out buffer))
					HideExceptionDetails = buffer;
			}
		}
	}
}