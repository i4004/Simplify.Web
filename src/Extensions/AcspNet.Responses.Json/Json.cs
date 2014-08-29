namespace AcspNet.Responses
{
	/// <summary>
	/// Provides controller JSON response (send only JSON string to response)
	/// </summary>
	public class Json : ControllerResponse
	{
		private readonly object _objectToConvert;

		/// <summary>
		/// Initializes a new instance of the <see cref="Json"/> class.
		/// </summary>
		/// <param name="objectToConvert">The object to convert to JSON.</param>
		public Json(object objectToConvert)
		{
			_objectToConvert = objectToConvert;
		}

		/// <summary>
		/// Processes this response
		/// </summary>
		public override ControllerResponseResult Process()
		{
			//ResponseWriter.Write(_objectToConvert, Context.Response);

			return ControllerResponseResult.RawOutput;
		}

		//private static string ToJson(object input)
		//{
		//	var serializer = new JavaScriptSerializer();
		//	return serializer.Serialize(input);
		//}
	}
}