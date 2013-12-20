using System;
using System.Globalization;
using System.Threading;
using System.Web;

namespace AcspNet.CoreExtensions.Library
{
	/// <summary>
	/// Site environment variables, by default initialized from <see cref="EngineSettings" />
	/// </summary>
	[Priority(-9)]
	[Version("1.1")]
	public sealed class EnvironmentVariables : LibExtension
	{
		/// <summary>
		/// Language field name in user cookies
		/// </summary>
		public const string CookieLanguageFieldName = "language";

		private string _language = "";
	
		/// <summary>
		/// Initializes the library extension.
		/// </summary>
		public override void Initialize()
		{
			var cookieLanguage = Manager.Request.Cookies[CookieLanguageFieldName];

			SetCurrentLanguage(cookieLanguage != null && !string.IsNullOrEmpty(cookieLanguage.Value) ? cookieLanguage.Value : EngineSettings.DefaultLanguage);

			TemplatesPath = EngineSettings.DefaultTemplatesDir;
			SiteStyle = EngineSettings.DefaultStyle;
		}

		/// <summary>
		/// Site current templates relative directory
		/// </summary>
		public string TemplatesPath { get; set; }

		/// <summary>
		/// Site current templates physical directory
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
		/// Set site current and cookie language value
		/// </summary>
		/// <param name="language">language code</param>
		public void SetLanguage(string language)
		{
			SetCurrentLanguage(language);
			SetCookieLanguage(language);
		}

		/// <summary>
		/// Set site cookie language value
		/// </summary>
		/// <param name="language">Language code</param>
		public void SetCookieLanguage(string language)
		{
			if (string.IsNullOrEmpty(language))
				return;

			var cookie = new HttpCookie(CookieLanguageFieldName, language)
			{
				Expires = DateTime.Now.AddYears(5)
			};

			Manager.Response.Cookies.Add(cookie);
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
