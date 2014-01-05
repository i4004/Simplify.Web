using System.Xml.Linq;

using ApplicationHelper;

namespace AcspNet
{
	/// <summary>
	/// Text and XML files loader
	/// </summary>
	public interface IExtensionsDataLoader : IHideObjectMembers
	{
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