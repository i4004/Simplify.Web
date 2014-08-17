using System;

namespace AcspNet.Responses
{
	/// <summary>
	/// Provides controller Ajax response (send only specified string to response)
	/// </summary>
	public class Ajax : ControllerResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Ajax"/> class.
		/// </summary>
		/// <param name="ajaxData">The ajax data.</param>
		public Ajax(string ajaxData)
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