using System;
using System.Globalization;
using System.Threading;
using Microsoft.Owin;

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

		private readonly ResponseCookieCollection _responseCookies;

		/// <summary>
		/// Initializes a new instance of the <see cref="LanguageManager" /> class.
		/// </summary>
		/// <param name="settings">The settings.</param>
		/// <param name="context">The OWIN context.</param>
		public LanguageManager(IAcspNetSettings settings, IOwinContext context)
		{
			_responseCookies = context.Response.Cookies;

			var cookieLanguage = context.Request.Cookies[CookieLanguageFieldName];
			SetCurrentLanguage(cookieLanguage != null && !string.IsNullOrEmpty(cookieLanguage) ? cookieLanguage : settings.DefaultLanguage);
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

			_responseCookies.Append(CookieLanguageFieldName, language, new CookieOptions(){Expires = DateTime.Now.AddYears(5)});
		}

		/// <summary>
		/// Set language only for current request
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