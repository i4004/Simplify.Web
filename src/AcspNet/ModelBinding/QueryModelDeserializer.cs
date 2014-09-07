using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;

namespace AcspNet.ModelBinding
{
	/// <summary>
	/// Provides http query to object (model) deserializer
	/// </summary>
	public class QueryModelDeserializer
	{
		/// <summary>
		/// Deserializes the specified query to model.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		public static T Deserialize<T>(IReadableStringCollection query)
		{
			return ListModelDeserializer.Deserialize<T>(query.Select(x => new KeyValuePair<string, string>(x.Key, x.Value[0])).ToList());
		}	 
	}
}