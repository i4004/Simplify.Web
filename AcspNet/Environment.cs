using System;
using System.Globalization;
using System.Threading;
using System.Web;

namespace AcspNet
{
	/// <summary>
	/// Site environment variables, by default initialized from <see cref="AcspNetSettings" />
	/// </summary>
	public sealed class Environment
	{
		/// <summary>
		/// Language field name in user cookies
		/// </summary>
		public const string CookieLanguageFieldName = "language";

		private string _language = "";

		private readonly Manager _manager;

		internal Environment(Manager manager)
		{
			_manager = manager;

			var cookieLanguage = _manager.Request.Cookies[CookieLanguageFieldName];

			SetCurrentLanguage(cookieLanguage != null && !string.IsNullOrEmpty(cookieLanguage.Value) ? cookieLanguage.Value : Manager.Settings.DefaultLanguage);

			TemplatesPath = Manager.Settings.DefaultTemplatesDir;
			SiteStyle = Manager.Settings.DefaultStyle;
		}

		/// <summary>
		/// Site current templates directory relative path
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

			_manager.Response.Cookies.Add(cookie);
		}

		/// <summary>
		/// Set language for current request
		/// </summary>
		/// <param name="language">Language code</param>
		private void SetCurrentLanguage(string language)
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
			Thread.CurrentThread.CurrentCulture = new CultureInfo(language);

			_language = language;
		}
	}
}
