using AcspNet.Extensions.Library;

namespace AcspNet.Extensions.Executable
{
	/// <summary>
	/// Setting site environment variables and settings values to <see cref="DataCollector"/> templates
	/// </summary>
	[Priority(9)]
	[Version("1.2")]
	public sealed class SiteVariablesSetter : IExecExtension
	{
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
		/// Invokes the executable extension.
		/// </summary>
		/// <param name="manager">The manager.</param>
		public void Invoke(Manager manager)
		{
			var dc = manager.Get<DataCollector>();
			var ev = manager.Get<EnvironmentVariables>();

			dc.Set(SiteVariableNameTemplatesPath, ev.TemplatesPath);
			dc.Set(SiteVariableNameCurrentStyle, ev.SiteStyle);
			dc.Set(SiteVariableNameCurrentLanguage, ev.Language);
			dc.Set(SiteVariableNameCurrentLanguageExtension, ev.Language != "" ? "." + ev.Language : "");
			dc.Set(SiteVariableNameSiteUrl, Manager.SiteUrl);
		}
	}
}
