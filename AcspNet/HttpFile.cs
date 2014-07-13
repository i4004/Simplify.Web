using System;

namespace AcspNet
{
	/// <summary>
	/// Provides Http file response
	/// </summary>
	public class HttpFile : IControllerResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="HttpFile"/> class.
		/// </summary>
		/// <param name="contentType">Type of the content.</param>
		/// <param name="name">The name of the file.</param>
		/// <param name="data">The data of the file.</param>
		public HttpFile(string contentType, string name, byte[] data)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Processes this response
		/// </summary>
		/// <exception cref="System.NotImplementedException"></exception>
		public void Process()
		{
			throw new NotImplementedException();
		}
	}
}