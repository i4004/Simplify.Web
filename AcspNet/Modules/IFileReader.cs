using System.Xml.Linq;

namespace AcspNet.Modules
{
	/// <summary>
	/// Represents localizable files reader
	/// </summary>
	public interface IFileReader : IHideObjectMembers
	{
		/// <summary>
		/// Load xml document from a extension data file
		/// </summary>
		/// <param name="extensionsDataFileName">Extension data file name</param>
		/// <returns>Xml document</returns>
		XDocument LoadXDocument(string extensionsDataFileName);

		/// <summary>
		/// Load text from a extension data file
		/// </summary>
		/// <param name="extensionsDataFileName">Extension data file name</param>
		/// <returns>Text from a extension data file</returns>
		string LoadTextDocument(string extensionsDataFileName);
	}
}