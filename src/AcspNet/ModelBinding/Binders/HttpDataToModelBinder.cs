using AcspNet.Modules;

namespace AcspNet.ModelBinding.Binders
{
	/// <summary>
	/// Provides POST/GET request data or JSON to model binding
	/// </summary>
	public class HttpDataToModelBinder : IModelBinder
	{
		private readonly IAcspNetContext _context;

		/// <summary>
		/// Initializes a new instance of the <see cref="HttpDataToModelBinder"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public HttpDataToModelBinder(IAcspNetContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Binds HTTP data to model
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <returns></returns>
		/// <exception cref="AcspNet.ModelBinding.ModelBindingException"></exception>
		public T Bind<T>()
		{
			if (_context.Request.ContentType == "application/x-www-form-urlencoded")
				return FormModelBinder.Bind<T>(_context.Form);

			if (_context.Request.Method == "GET")
				return QueryModelBinder.Bind<T>(_context.Query);

			throw new ModelBindingException(string.Format("Unrecognized request content type for binding: {0}", _context.Request.ContentType));
		}
	}
}