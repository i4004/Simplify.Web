using System.Xml.Linq;

namespace AcspNet.Modules
{
	/// <summary>
	/// Represents localizable text and XML files reader
	/// </summary>
	public interface IFileReader : IHideObjectMembers
	{
		/// <summary>
		/// Gets the current language.
		/// </summary>
		/// <value>
		/// The current language.
		/// </value>
		string Language { get; }

		/// <summary>
		/// Gets the default language.
		/// </summary>
		/// <value>
		/// The default language.
		/// </value>
		string DefaultLanguage { get; }

		/// <summary>
		/// Load xml document from a extension data file
		/// </summary>
		/// <param name="extensionsDataFileName">Extension data file name</param>
		/// <returns>Xml document</returns>
		XDocument LoadXDocument(string extensionsDataFileName);

		/// <summary>
		/// Load xml document from a extension data file with specific language
		/// </summary>
		/// <param name="extensionsDataFileName">Extension data file name</param>
		/// <param name="language">Extension data file language</param>
		/// <returns>Xml document</returns>
		XDocument LoadXDocument(string extensionsDataFileName, string language);

		/// <summary>
		/// Load text from a extension data file
		/// </summary>
		/// <param name="extensionsDataFileName">Extension data file name</param>
		/// <returns>Text from a extension data file</returns>
		string LoadTextDocument(string extensionsDataFileName);

		/// <summary>
		/// Load text from a extension data file with specific language
		/// </summary>
		/// <param name="extensionsDataFileName">Extension data file name</param>
		/// <param name="language">Extension data file language</param>
		/// <returns>Text from a extension data file</returns>
		string LoadTextDocument(string extensionsDataFileName, string language);
	}
}