using System.Threading.Tasks;
using Microsoft.Owin;

namespace AcspNet.Core
{
	/// <summary>
	/// Represent response writer
	/// </summary>
	public interface IResponseWriter
	{
		/// <summary>
		/// Writes the specified data.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="response">The response.</param>
		/// <returns></returns>
		Task Write(string data, IOwinResponse response);

		/// <summary>
		/// Writes the specified data.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="response">The response.</param>
		/// <returns></returns>
		Task Write(byte[] data, IOwinResponse response);
	}
}