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
			AjaxData = ajaxData;
		}

		/// <summary>
		/// Gets the ajax data.
		/// </summary>
		/// <value>
		/// The ajax data.
		/// </value>
		public string AjaxData { get; private set; }

		/// <summary>
		/// Processes this response
		/// </summary>
		public override ControllerResponseResult Process()
		{
			ResponseWriter.Write(AjaxData, Context.Response);

			return ControllerResponseResult.RawOutput;
		}
	}
}