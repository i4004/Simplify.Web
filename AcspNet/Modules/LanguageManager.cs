using System;
using System.Globalization;
using System.Threading;
using System.Web;

namespace AcspNet.Modules
{
	/// <summary>
	/// Current language controller and information container
	/// </summary>
	public class LanguageManager : ILanguageManager
	{
		/// <summary>
		/// Language field name in user cookies
		/// </summary>
		public const string CookieLanguageFieldName = "language";

		private readonly HttpCookieCollection _responseCookies;
	
		internal LanguageManager(string defaultLanguage, HttpCookieCollection requestCookies, HttpCookieCollection responseCookies)
		{
			_responseCookies = responseCookies;

			var cookieLanguage = requestCookies[CookieLanguageFieldName];
			SetCurrentLanguage(cookieLanguage != null && !string.IsNullOrEmpty(cookieLanguage.Value) ? cookieLanguage.Value : defaultLanguage);
		}

		/// <summary>
		/// Site current language, for example: "en", "ru", "de" etc.
		/// </summary>
		public string Language { get; private set; }

		/// <summary>
		/// Set site cookie language value
		/// </summary>
		/// <param name="language">Language code</param>
		public void SetCookieLanguage(string language)
		{
			if (string.IsNullOrEmpty(language))
				throw new ArgumentNullException("language");

			var cookie = new HttpCookie(CookieLanguageFieldName, language)
			{
				Expires = DateTime.Now.AddYears(5)
			};

			_responseCookies.Add(cookie);
		}

		/// <summary>
		/// Set language for current request
		/// </summary>
		/// <param name="language">Language code</param>
		public void SetCurrentLanguage(string language)
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
			Thread.CurrentThread.CurrentCulture = new CultureInfo(language);

			Language = language;
		}		 
	}
}