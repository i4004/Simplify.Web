using System;
using System.Threading.Tasks;

namespace Simplify.Web.Core.StaticFiles
{
	/// <summary>
	/// Represent static file response
	/// </summary>
	public interface IStaticFileResponse
	{
		/// <summary>
		/// Sends the not modified static file response.
		/// </summary>
		/// <param name="lastModifiedTime">The last modified time.</param>
		/// <returns></returns>
		Task SendNotModified(DateTime lastModifiedTime);

		/// <summary>
		/// Sends the fresh static file.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="lastModifiedTime">The last modified time.</param>
		/// <returns></returns>
		Task SendNew(byte[] data, DateTime lastModifiedTime);

		/// <summary>
		/// Sets the MIME type of response.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		void SetMimeType(string fileName);
	}
}