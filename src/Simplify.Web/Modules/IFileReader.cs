using System.Xml.Linq;

namespace Simplify.Web.Modules
{
	/// <summary>
	/// Represents localizable files reader
	/// </summary>
	public interface IFileReader : IHideObjectMembers
	{
		/// <summary>
		/// Setups the file reader.
		/// </summary>
		void Setup();

		/// <summary>
		/// Load xml document from a file
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <param name="memoryCache">Load file from memory cache if possible.</param>
		/// <returns>Xml document</returns>
		XDocument LoadXDocument(string fileName, bool memoryCache = false);

		/// <summary>
		/// Load xml document from a file with specific language
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <param name="language">File language</param>
		/// <param name="memoryCache">Load file from memory cache if possible.</param>
		/// <returns>Xml document</returns>
		XDocument LoadXDocument(string fileName, string language, bool memoryCache = false);

		/// <summary>
		/// Load text from a extension data file
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <param name="memoryCache">Load file from memory cache if possible.</param>
		/// <returns>Text from a file</returns>
		string LoadTextDocument(string fileName, bool memoryCache = false);

		/// <summary>
		/// Load text from a file with specific language
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <param name="language">File language</param>
		/// <param name="memoryCache">Load file from memory cache if possible.</param>
		/// <returns>Text from a file</returns>
		string LoadTextDocument(string fileName, string language, bool memoryCache = false);
	}
}