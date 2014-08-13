using Microsoft.Owin;

namespace AcspNet.Modules
{
	/// <summary>
	/// Provides language manager provider
	/// </summary>
	public class LanguageManagerProvider : ILanguageManagerProvider
	{
		private readonly string _defaultLanguage;
		private ILanguageManager _languageManager;

		/// <summary>
		/// Initializes a new instance of the <see cref="LanguageManagerProvider"/> class.
		/// </summary>
		/// <param name="defaultLanguage">The default language.</param>
		public LanguageManagerProvider(string defaultLanguage)
		{
			_defaultLanguage = defaultLanguage;
		}

		/// <summary>
		/// Creates the language manager instance.
		/// </summary>
		/// <param name="context">The context.</param>
		public void Setup(IOwinContext context)
		{
			if(_languageManager == null)
				_languageManager = new LanguageManager(_defaultLanguage, context.Request.Cookies, context.Response.Cookies);
		}

		/// <summary>
		/// Gets the language manager.
		/// </summary>
		/// <returns></returns>
		public ILanguageManager Get()
		{
			return _languageManager;
		}
	}
}