using System.Xml.Linq;

namespace AcspNet.Modules
{
	/// <summary>
	/// Represents localizable files reader
	/// </summary>
	public interface IFileReader : IHideObjectMembers
	{
		/// <summary>
		/// Load xml document from a file
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <returns>Xml document</returns>
		XDocument LoadXDocument(string fileName);
		
		/// <summary>
		/// Load xml document from a file with specific language
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <param name="language">File language</param>
		/// <returns>Xml document</returns>
		XDocument LoadXDocument(string fileName, string language);

		/// <summary>
		/// Load text from a extension data file
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <returns>Text from a file</returns>
		string LoadTextDocument(string fileName);
		
		/// <summary>
		/// Load text from a file with specific language
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <param name="language">File language</param>
		/// <returns>Text from a file</returns>
		string LoadTextDocument(string fileName, string language);
	}
}