namespace Simplify.Web.Responses
{
	/// <summary>
	/// Provides controller Ajax response (send only specified string to response)
	/// </summary>
	public class Ajax : ControllerResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Ajax" /> class.
		/// </summary>
		/// <param name="ajaxData">The ajax data.</param>
		/// <param name="statusCode">The HTTP response status code.</param>
		public Ajax(string ajaxData, int statusCode = 200)
		{
			AjaxData = ajaxData;
			StatusCode = statusCode;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Ajax" /> class.
		/// </summary>
		/// <param name="statusCode">The HTTP response status code.</param>
		public Ajax(int statusCode = 204)
		{
			StatusCode = statusCode;
		}

		/// <summary>
		/// Gets the ajax data.
		/// </summary>
		/// <value>
		/// The ajax data.
		/// </value>
		public string AjaxData { get; }

		/// <summary>
		/// Gets the HTTP response status code.
		/// </summary>
		/// <value>
		/// The HTTP response status code.
		/// </value>
		public int StatusCode { get; }

		/// <summary>
		/// Processes this response
		/// </summary>
		public override ControllerResponseResult Process()
		{
			Context.Response.StatusCode = StatusCode;

			if (AjaxData != null)
				ResponseWriter.Write(AjaxData, Context.Response);

			return ControllerResponseResult.RawOutput;
		}
	}
}