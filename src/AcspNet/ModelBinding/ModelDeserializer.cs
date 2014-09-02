using System;
using AcspNet.Modules;
using Microsoft.Owin;

namespace AcspNet.ModelBinding
{
	/// <summary>
	/// Provides view model from POST/GET request data or JSON to object serializer
	/// </summary>
	public class ModelDeserializer : IModelDeserializer
	{
		private readonly IAcspNetContext _context;

		/// <summary>
		/// Initializes a new instance of the <see cref="ModelDeserializer"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public ModelDeserializer(IAcspNetContext context)
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
				return FormDeserializer.Deserialize<T>(_context.Form);

			return default(T);
		}
	}
}