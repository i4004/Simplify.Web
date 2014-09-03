using Microsoft.Owin;

namespace AcspNet.Modules
{
	/// <summary>
	/// Provides language manager provider
	/// </summary>
	public class LanguageManagerProvider : ILanguageManagerProvider
	{
		private readonly IAcspNetSettings _settings;
		private ILanguageManager _languageManager;

		/// <summary>
		/// Initializes a new instance of the <see cref="LanguageManagerProvider" /> class.
		/// </summary>
		/// <param name="settings">The settings.</param>
		public LanguageManagerProvider(IAcspNetSettings settings)
		{
			_settings = settings;
		}

		/// <summary>
		/// Creates the language manager instance.
		/// </summary>
		/// <param name="context">The context.</param>
		public void Setup(IOwinContext context)
		{
			if(_languageManager == null)
				_languageManager = new LanguageManager(_settings, context);
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