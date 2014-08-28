using System;

namespace AcspNet.Responses
{
	/// <summary>
	/// Provides Http file response (send file to response)
	/// </summary>
	public class File : ControllerResponse
	{
		private readonly string _outputFileName;
		private readonly string _contentType;
		private readonly byte[] _data;

		/// <summary>
		/// Initializes a new instance of the <see cref="File"/> class.
		/// </summary>
		/// <param name="outputFileName">The name of the file.</param>
		/// <param name="contentType">Type of the content.</param>
		/// <param name="data">The data of the file.</param>
		public File(string outputFileName, string contentType, byte[] data)
		{
			if (outputFileName == null) throw new ArgumentNullException("outputFileName");
			if (contentType == null) throw new ArgumentNullException("contentType");
			if (data == null) throw new ArgumentNullException("data");

			_outputFileName = outputFileName;
			_contentType = contentType;
			_data = data;
		}

		/// <summary>
		/// Processes this response
		/// </summary>
		public override ControllerResponseResult Process()
		{
			Context.Response.Headers.Append("Content-Disposition", "attachment; filename=" + _outputFileName);
			Context.Response.ContentType = _contentType;
			Context.Response.Write(_data);

			return ControllerResponseResult.RawOutput;
		}
	}
}