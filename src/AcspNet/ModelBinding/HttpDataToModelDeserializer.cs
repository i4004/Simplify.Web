using AcspNet.Modules;

namespace AcspNet.ModelBinding
{
	/// <summary>
	/// Provides view model from POST/GET request data or JSON to object serialization
	/// </summary>
	public class HttpDataToModelDeserializer : IModelDeserializer
	{
		private readonly IAcspNetContext _context;

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpDataToModelDeserializer"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public HttpDataToModelDeserializer(IAcspNetContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Deserializes view model from POST/GET request data or JSON to object
		/// </summary>
		/// <typeparam name="T">View model type</typeparam>
		/// <returns></returns>
		public T Deserialize<T>()
		{
			if (_context.Request.ContentType == "application/x-www-form-urlencoded")
				return FormModelDeserializer.Deserialize<T>(_context.Form);

			if (_context.Request.Method == "GET")
				return QueryModelDeserializer.Deserialize<T>(_context.Query);

			throw new ModelBindingException(string.Format("Unrecognized request content type for binding: {0}", _context.Request.ContentType));
		}
	}
}