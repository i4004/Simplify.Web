using System.Threading.Tasks;
using Microsoft.Owin;

namespace AcspNet.Core
{
	/// <summary>
	/// Providers response writer
	/// </summary>
	public class ResponseWriter : IResponseWriter
	{
		/// <summary>
		/// Writes the specified data.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="response">The response.</param>
		/// <returns></returns>
		public Task Write(string data, IOwinResponse response)
		{
			return response.WriteAsync(data);
		}

		/// <summary>
		/// Writes the specified data.
		/// </summary>
		/// <param name="data">The data.</param>
		/// <param name="response">The response.</param>
		/// <returns></returns>
		public Task Write(byte[] data, IOwinResponse response)
		{
			return response.WriteAsync(data);
		}
	}
}