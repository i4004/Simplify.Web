using System;

namespace AcspNet.Modules
{
	/// <summary>
	/// Data collector specific data setter
	/// </summary>
	public class DataCollectorDataSetter
	{
		private readonly IDataCollector _dataCollector;

		/// <summary>
		/// Site generation time templates variable name
		/// </summary>
		public const string SiteVariableNameSiteExecutionTime = "SV:SiteExecutionTime";

		/// <summary>
		/// The site variable name templates dir
		/// </summary>
		public const string SiteVariableNameTemplatesPath = "SV:TemplatesDir";

		/// <summary>
		/// The site variable name current style
		/// </summary>
		public const string SiteVariableNameCurrentStyle = "SV:Style";

		/// <summary>
		/// The site variable name current language
		/// </summary>
		public const string SiteVariableNameCurrentLanguage = "SV:Language";
		/// <summary>
		/// The site variable name current language extension
		/// </summary>
		public const string SiteVariableNameCurrentLanguageExtension = "SV:LanguageExt";
		/// <summary>
		/// The site variable name site URL
		/// </summary>
		public const string SiteVariableNameSiteUrl = "SV:SiteUrl";
		/// <summary>
		/// The site variable name site virtual path (returns '/yoursite' if your site is placed in virtual directory like http://yourdomain.com/yoursite/)
		/// </summary>
		public const string SiteVariableNameSiteVirtualPath = "~";

		/// <summary>
		/// Initializes a new instance of the <see cref="DataCollectorDataSetter"/> class.
		/// </summary>
		/// <param name="dataCollector">The data collector.</param>
		public DataCollectorDataSetter(IDataCollector dataCollector)
		{
			_dataCollector = dataCollector;
		}

		/// <summary>
		/// Sets environment variables into data collector.
		/// </summary>
		/// <param name="environment">The environment.</param>
		public void SetEnvironmentVariables(IEnvironment environment)
		{
			_dataCollector.Add(SiteVariableNameTemplatesPath, environment.TemplatesPath);
			_dataCollector.Add(SiteVariableNameCurrentStyle, environment.SiteStyle);
		}

		/// <summary>
		/// Sets context variables into data collector.
		/// </summary>
		/// <param name="context">The context.</param>
		public void SetContextVariables(IAcspContext context)
		{
			_dataCollector.Add(SiteVariableNameSiteUrl, context.SiteUrl);
			_dataCollector.Add(SiteVariableNameSiteVirtualPath, context.SiteVirtualPath);
		}

		/// <summary>
		/// Sets language variables into data collector.
		/// </summary>
		/// <param name="language">The language.</param>
		public void SetLanguageVariables(string language)
		{
			_dataCollector.Add(SiteVariableNameCurrentLanguage, language);
			_dataCollector.Add(SiteVariableNameCurrentLanguageExtension, !string.IsNullOrEmpty(language) ? "." + language : "");
		}

		/// <summary>
		/// Sets environment variables into data collector.
		/// </summary>
		/// <param name="elapsedExecutionTime">The execution time.</param>
		public void SetExecutionTimeVariable(TimeSpan elapsedExecutionTime)
		{
			_dataCollector.Add(SiteVariableNameSiteExecutionTime, elapsedExecutionTime.ToString("mm\\:ss\\:fff"));
		}

		/// <summary>
		/// Sets the site title.
		/// </summary>
		/// <param name="action">The action.</param>
		/// <param name="mode">The mode.</param>
		public void SetSiteTitleFromStringTable(string action, string mode)
		{
			if (String.IsNullOrEmpty(action) && String.IsNullOrEmpty(mode)
				&& !_dataCollector.IsDataExist(_dataCollector.TitleVariableName))
				_dataCollector.Add(_dataCollector.TitleVariableName, _dataCollector.StringTable["SiteTitle"]);
			else
				_dataCollector.Add(_dataCollector.TitleVariableName, " - " + _dataCollector.StringTable["SiteTitle"]);
		}
	}
}