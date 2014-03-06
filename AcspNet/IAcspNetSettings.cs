namespace AcspNet
{
	public interface IAcspNetSettings : IHideObjectMembers
	{
		/// <summary>
		/// Default templates directory path, for example: Templates, default value is "Templates"
		/// </summary>
		string DefaultTemplatesPath { get; }

		/// <summary>
		/// Default site style
		/// </summary>
		string DefaultStyle { get; }

		/// <summary>
		/// Default language, for example: "en", "ru", "de" etc., default value is "en"
		/// </summary>
		string DefaultLanguage { get; }

		/// <summary>
		/// Extension data directory path, for example: ExtensionsData, default value is "ExtensionsData"
		/// </summary>
		string DefaultExtensionDataPath { get; }

		/// <summary>
		/// Site default page
		/// </summary>
		/// <value>
		/// Site default page
		/// </value>
		string IndexPage { get; }

		/// <summary>
		/// Gets or sets the master page template file name
		/// </summary>
		/// <value>
		/// The name of the master page template file
		/// </value>
		string DefaultMasterTemplateFileName { get; }

		/// <summary>
		/// Gets or sets the master template main content variable name.
		/// </summary>
		/// <value>
		/// The  master template main content variable name.
		/// </value>
		string DefaultMainContentVariableName { get; }

		/// <summary>
		/// Gets or sets the master template title variable name.
		/// </summary>
		/// <value>
		/// The title variable name.
		/// </value>
		string DefaultTitleVariableName { get; }

		/// <summary>
		/// Gets a value indicating whether templates memory cache enabled or disabled.
		/// </summary>
		/// <value>
		/// <c>true</c> if templates memory cache enabled; otherwise, <c>false</c>.
		/// </value>
		bool TemplatesMemoryCache { get; }

		/// <summary>
		/// Gets or sets a value indicating whether site title postfix should be set automatically
		/// </summary>
		/// <value>
		/// <c>true</c> if [disable automatic site title set]; otherwise, <c>false</c>.
		/// </value>
		bool DisableAutomaticSiteTitleSet { get; }

		/// <summary>
		/// Gets or sets a value indicating whether internal AcspNet extensions from AcspNet.Extensions.Executable should be disabled
		/// </summary>
		bool DisableAcspInternalExtensions { get; }
	}
}