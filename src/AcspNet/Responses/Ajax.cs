namespace AcspNet.Responses
{
	/// <summary>
	/// Provides controller Ajax response (send only specified string to response)
	/// </summary>
	public class Ajax : ControllerResponse
	{
		private readonly string _ajaxData;

		/// <summary>
		/// Initializes a new instance of the <see cref="Ajax"/> class.
		/// </summary>
		/// <param name="ajaxData">The ajax data.</param>
		public Ajax(string ajaxData)
		{
			_ajaxData = ajaxData;
		}

		/// <summary>
		/// Processes this response
		/// </summary>
		public override ControllerResponseResult Process()
		{
			ResponseWriter.Write(_ajaxData, Context.Response);

			return ControllerResponseResult.RawOutput;
		}
	}
}