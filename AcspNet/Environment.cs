using System;
using System.Globalization;
using System.Threading;
using System.Web;

namespace AcspNet
{
	/// <summary>
	/// Site environment variables, by default initialized from <see cref="AcspNetSettings" />
	/// </summary>
	public sealed class Environment : IEnvironment
	{
		private string _language = "";

		private readonly Manager _manager;

		internal Environment(Manager manager)
		{
			_manager = manager;

			var cookieLanguage = _manager.Request.Cookies[Manager.CookieLanguageFieldName];

			SetCurrentLanguage(cookieLanguage != null && !string.IsNullOrEmpty(cookieLanguage.Value) ? cookieLanguage.Value : Manager.Settings.DefaultLanguage);

			TemplatesPath = Manager.Settings.DefaultTemplatesPath;
			SiteStyle = Manager.Settings.DefaultStyle;
			ExtensionsDataPath = Manager.Settings.DefaultExtensionDataPath;
			TemplatesMemoryCache = Manager.Settings.TemplatesMemoryCache;
			MasterTemplateFileName = Manager.Settings.DefaultMasterTemplateFileName;
			MainContentVariableName = Manager.Settings.DefaultMainContentVariableName;
			TitleVariableName = Manager.Settings.DefaultTitleVariableName;
		}

		/// <summary>
		/// Site current templates directory relative path
		/// </summary>
		public string TemplatesPath { get; set; }

		/// <summary>
		/// Site current templates directory physical path
		/// </summary>
		public string TemplatesPhysicalPath
		{
			get
			{
				return Manager.SitePhysicalPath + TemplatesPath;
			}
		}

		/// <summary>
		/// Site current style
		/// </summary>
		public string SiteStyle { get; set; }

		/// <summary>
		/// Site current language, for example: "en", "ru", "de" etc.
		/// </summary>
		public string Language
		{
			get { return _language; }
		}

		/// <summary>
		/// Site current extensions data directory relative path
		/// </summary>
		public string ExtensionsDataPath { get; set; }

		/// <summary>
		/// Site templates memory cache status
		/// </summary>
		/// <value>
		/// <c>true</c> if templates memory cache enabled; otherwise, <c>false</c>.
		/// </value>
		public bool TemplatesMemoryCache { get; set; }

		/// <summary>
		/// Gets or sets the current master page template file name
		/// </summary>
		/// <value>
		/// The name of the master page template file
		/// </value>
		public string MasterTemplateFileName { get; set; }

		/// <summary>
		/// Gets or sets the current master template main content variable name.
		/// </summary>
		/// <value>
		/// The  master template main content variable name.
		/// </value>
		public string MainContentVariableName { get; set; }

		/// <summary>
		/// Gets or sets the current master template title variable name.
		/// </summary>
		/// <value>
		/// The title variable name.
		/// </value>
		public string TitleVariableName { get; set; }

		/// <summary>
		/// Set site cookie language value
		/// </summary>
		/// <param name="language">Language code</param>
		public void SetCookieLanguage(string language)
		{
			if (string.IsNullOrEmpty(language))
				return;

			var cookie = new HttpCookie(Manager.CookieLanguageFieldName, language)
			{
				Expires = DateTime.Now.AddYears(5)
			};

			_manager.Response.Cookies.Add(cookie);
		}

		/// <summary>
		/// Set language for current request
		/// </summary>
		/// <param name="language">Language code</param>
		public void SetCurrentLanguage(string language)
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
			Thread.CurrentThread.CurrentCulture = new CultureInfo(language);

			_language = language;
		}
	}
}
