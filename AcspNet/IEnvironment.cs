namespace AcspNet
{
	/// <summary>
	/// Represent web-site enviroment properties
	/// </summary>
	public interface IEnvironment : IHideObjectMembers
	{
		/// <summary>
		/// Site current templates directory relative path
		/// </summary>
		string TemplatesPath { get; set; }

		/// <summary>
		/// Site current templates directory physical path
		/// </summary>
		string TemplatesPhysicalPath { get; }

		/// <summary>
		/// Site current style
		/// </summary>
		string SiteStyle { get; set; }

		/// <summary>
		/// Site current language, for example: "en", "ru", "de" etc.
		/// </summary>
		string Language { get; }

		/// <summary>
		/// Site current extensions data directory relative path
		/// </summary>
		string ExtensionsDataPath { get; set; }

		/// <summary>
		/// Site templates memory cache status
		/// </summary>
		/// <value>
		/// <c>true</c> if templates memory cache enabled; otherwise, <c>false</c>.
		/// </value>
		bool TemplatesMemoryCache { get; set; }

		/// <summary>
		/// Gets or sets the current master page template file name
		/// </summary>
		/// <value>
		/// The name of the master page template file
		/// </value>
		string MasterTemplateFileName { get; set; }

		/// <summary>
		/// Gets or sets the current master template main content variable name.
		/// </summary>
		/// <value>
		/// The  master template main content variable name.
		/// </value>
		string MainContentVariableName { get; set; }

		/// <summary>
		/// Gets or sets the current master template title variable name.
		/// </summary>
		/// <value>
		/// The title variable name.
		/// </value>
		string TitleVariableName { get; set; }

		/// <summary>
		/// Set site cookie language value
		/// </summary>
		/// <param name="language">Language code</param>
		void SetCookieLanguage(string language);

		/// <summary>
		/// Set language for current request
		/// </summary>
		/// <param name="language">Language code</param>
		void SetCurrentLanguage(string language);
	}
}