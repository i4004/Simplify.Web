using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;

namespace AcspNet.ModelBinding
{
	/// <summary>
	/// Provides form data to object (model) deserialization
	/// </summary>
	public static class FormModelDeserializer
	{
		/// <summary>
		/// Deserializes specifed form to model.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T Deserialize<T>(IFormCollection form)
		{
			return ListModelDeserializer.Deserialize<T>(form.Select(x => new KeyValuePair<string, string>(x.Key, x.Value[0])).ToList());
		}
	}
}