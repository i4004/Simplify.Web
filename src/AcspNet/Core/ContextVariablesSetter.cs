using AcspNet.Modules;
using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Provides context variables setter
	/// </summary>
	public class ContextVariablesSetter : IContextVariablesSetter
	{

		/// <summary>
		/// The site variable name templates dir
		/// </summary>
		public const string VariableNameTemplatesPath = "SV:TemplatesDir";

		/// <summary>
		/// The site variable name current style
		/// </summary>
		public const string VariableNameSiteStyle = "SV:Style";

		/// <summary>
		/// The site variable name current language
		/// </summary>
		public const string VariableNameCurrentLanguage = "SV:Language";

		/// <summary>
		/// The site variable name current language extension
		/// </summary>
		public const string VariableNameCurrentLanguageExtension = "SV:LanguageExt";

		/// <summary>
		/// The site variable name site URL
		/// </summary>
		public const string VariableNameSiteUrl = "SV:SiteUrl";

		/// <summary>
		/// The site variable name site virtual path (returns '/yoursite' if your site is placed in virtual directory like http://yourdomain.com/yoursite/)
		/// </summary>
		public const string VariableNameSiteVirtualPath = "~";

		/// <summary>
		/// Site generation time templates variable name
		/// </summary>
		public const string VariableNameExecutionTime = "SV:SiteExecutionTime";


		private readonly IDataCollector _dataCollector;

		/// <summary>
		/// Initializes a new instance of the <see cref="ContextVariablesSetter"/> class.
		/// </summary>
		/// <param name="dataCollector">The data collector.</param>
		public ContextVariablesSetter(IDataCollector dataCollector)
		{
			_dataCollector = dataCollector;
		}

		/// <summary>
		/// Sets the context variables to data collector
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		public void SetVariables(IDIContainerProvider containerProvider)
		{
			var environment = containerProvider.Resolve<IEnvironment>();
			var languageManager = containerProvider.Resolve<ILanguageManagerProvider>().Get();
			var context = containerProvider.Resolve<IAcspNetContextProvider>().Get();
			var stopWatchProvider = containerProvider.Resolve<IStopwatchProvider>();

			_dataCollector.Add(VariableNameTemplatesPath, environment.TemplatesPath);
			_dataCollector.Add(VariableNameSiteStyle, environment.SiteStyle);

			_dataCollector.Add(VariableNameCurrentLanguage, languageManager.Language);
			_dataCollector.Add(VariableNameCurrentLanguageExtension, !string.IsNullOrEmpty(languageManager.Language) ? "." + languageManager.Language : "");

			_dataCollector.Add(VariableNameSiteUrl, context.SiteUrl);
			_dataCollector.Add(VariableNameSiteVirtualPath, context.Request.PathBase.Value);

			_dataCollector.Add(VariableNameExecutionTime, stopWatchProvider.StopAndGetMeasurement().ToString("mm\\:ss\\:fff"));
		}
	}
}