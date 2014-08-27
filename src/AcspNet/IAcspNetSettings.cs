using System.Collections.Generic;

namespace AcspNet
{
	/// <summary>
	/// Represent AcspNet settings
	/// </summary>
	public interface IAcspNetSettings : IHideObjectMembers
	{
		/// <summary>
		/// Default language, for example: "en", "ru", "de" etc., default value is "en"
		/// </summary>
		string DefaultLanguage { get; }

		/// <summary>
		/// Gets a value indicating whether browser language should be accepted
		/// </summary>
		/// <value>
		/// <c>true</c> if  browser language should be accepted; otherwise, <c>false</c>.
		/// </value>
		bool AcceptBrowserLanguage { get; }

		/// <summary>
		/// Default templates directory path, for example: Templates, default value is "Templates"
		/// </summary>
		string DefaultTemplatesPath { get; }

		/// <summary>
		/// Gets a value indicating whether templates memory cache enabled or disabled.
		/// </summary>
		/// <value>
		/// <c>true</c> if templates memory cache enabled; otherwise, <c>false</c>.
		/// </value>
		bool TemplatesMemoryCache { get; }

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
		/// Default site style
		/// </summary>
		string DefaultStyle { get; }

		/// <summary>
		/// Gets a value indicating whether string table memory cache enabled or disabled.
		/// </summary>
		/// <value>
		/// <c>true</c> if string table memory cache enabled; otherwise, <c>false</c>.
		/// </value>
		bool StringTableMemoryCache { get; }

		/// <summary>
		/// Data directory path, for example: default value is "App_Data"
		/// </summary>
		string DataPath { get; }

		/// <summary>
		/// Gets the static files paths.
		/// </summary>
		/// <value>
		/// The static files paths.
		/// </value>
		IList<string> StaticFilesPaths { get; }

		/// <summary>
		/// Gets or sets a value indicating whether site title postfix should be set automatically
		/// </summary>
		bool DisableAutomaticSiteTitleSet { get; }

		/// <summary>
		/// Gets a value indicating whether exception details should be hide when some unhandled exception occured.
		/// </summary>
		bool HideExceptionDetails { get; }
	}
}