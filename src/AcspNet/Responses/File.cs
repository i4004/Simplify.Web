using System;

namespace AcspNet.Responses
{
	/// <summary>
	/// Provides Http file response (send file to response)
	/// </summary>
	public class File : ControllerResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="File"/> class.
		/// </summary>
		/// <param name="contentType">Type of the content.</param>
		/// <param name="name">The name of the file.</param>
		/// <param name="data">The data of the file.</param>
		public File(string contentType, string name, byte[] data)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Processes this response
		/// </summary>
		/// <exception cref="System.NotImplementedException"></exception>
		public override ControllerResponseResult Process()
		{
			throw new NotImplementedException();
		}
	}
}