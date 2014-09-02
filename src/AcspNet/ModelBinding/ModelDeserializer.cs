using System;
using Microsoft.Owin;

namespace AcspNet.ModelBinding
{
	/// <summary>
	/// Provides view model from POST/GET request data or JSON to object serializer
	/// </summary>
	public class ModelDeserializer : IModelDeserializer
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ModelDeserializer"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public ModelDeserializer(IOwinContext context)
		{
			
		}

		/// <summary>
		/// Deserializes view model from POST/GET request data or JSON to object
		/// </summary>
		/// <typeparam name="T">View model type</typeparam>
		/// <returns></returns>
		public T Deserialize<T>()
		{
			throw new NotImplementedException();
		}
	}
}