using ApplicationHelper;

namespace AcspNet
{
	/// <summary>
	/// Current request environment data.
	/// </summary>
	public interface IEnvironment : IHideObjectMembers
	{
		/// <summary>
		/// Site current templates directory relative path
		/// </summary>
		string TemplatesPath { get; set; }

		/// <summary>
		/// Site current templates physical directory
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