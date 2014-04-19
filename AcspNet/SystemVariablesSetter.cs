using System;

namespace AcspNet
{
	public class SystemVariablesSetter
	{
		/// <summary>
		/// Site generation time templates variable name
		/// </summary>
		public const string SiteVariableNameSiteGenerationTime = "SV:SiteGenerationTime";

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
		public const string SiteVariableNameSiteVirtualPath = "SV:SiteVirtualPath";

		/// <summary>
		/// Sets system variables into data collector.
		/// </summary>
		/// <param name="dataCollector">The data collector.</param>
		/// <param name="environment">The environment.</param>
		/// <param name="context">The context.</param>
		/// <param name="elapsedExecutionTime">The execution time.</param>
		/// <param name="language">The language.</param>
		public static void Set(IDataCollector dataCollector, IEnvironment environment, IAcspContext context, TimeSpan elapsedExecutionTime, string language)
		{
			dataCollector.Add(SiteVariableNameSiteGenerationTime, elapsedExecutionTime.ToString("mm\\:ss\\:fff"));
			dataCollector.Add(SiteVariableNameTemplatesPath, environment.TemplatesPath);
			dataCollector.Add(SiteVariableNameCurrentStyle, environment.SiteStyle);
			dataCollector.Add(SiteVariableNameCurrentLanguage, language);
			dataCollector.Add(SiteVariableNameCurrentLanguageExtension, language != "" ? "." + language : "");
			//dataCollector.Add(SiteVariableNameSiteUrl, context.SiteUrl);
			//dataCollector.Add(SiteVariableNameSiteVirtualPath, context.SiteVirtualPath);
		}		 
	}
}