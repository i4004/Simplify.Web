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
		/// Default site style
		/// </summary>
		string DefaultStyle { get; }

		/// <summary>
		/// Default templates directory path, for example: Templates, default value is "Templates"
		/// </summary>
		string DefaultTemplatesPath { get; }

		/// <summary>
		/// Gets or sets the master page template file name
		/// </summary>
		/// <value>
		/// The name of the master page template file
		/// </value>
		string DefaultMasterTemplateFileName { get; }

		/// <summary>
		/// Gets a value indicating whether templates memory cache enabled or disabled.
		/// </summary>
		/// <value>
		/// <c>true</c> if templates memory cache enabled; otherwise, <c>false</c>.
		/// </value>
		bool TemplatesMemoryCache { get; }

		/// <summary>
		/// Data directory path, for example: default value is "App_Data"
		/// </summary>
		string DefaultDataPath { get; }

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
		/// Gets or sets a value indicating whether site title postfix should be set automatically
		/// </summary>
		bool DisableAutomaticSiteTitleSet { get; }

		/// <summary>
		/// Gets a value indicating whether exception details should be hide when some unhandler exception occured.
		/// </summary>
		bool HideExceptionDetails { get; }
	}
}