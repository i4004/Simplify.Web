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
		/// Site generation time templates variable name
		/// </summary>
		public const string VariableNameSiteExecutionTime = "SV:SiteExecutionTime";

		/// <summary>
		/// The site variable name templates dir
		/// </summary>
		public const string VariableNameTemplatesPath = "SV:TemplatesDir";

		/// <summary>
		/// The site variable name current style
		/// </summary>
		public const string VariableNameCurrentStyle = "SV:Style";

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

		private readonly IDataCollector _dataCollector;

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
			throw new System.NotImplementedException();
		}
	}
}