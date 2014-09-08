using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;

namespace AcspNet.ModelBinding.Binders
{
	/// <summary>
	/// Provides HTTP query to model binding
	/// </summary>
	public class QueryModelBinder
	{
		/// <summary>
		/// Binds the specified query data to model.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query">The query.</param>
		/// <returns></returns>
		public static T Bind<T>(IReadableStringCollection query)
		{
			return ListModelBinder.Bind<T>(query.Select(x => new KeyValuePair<string, string>(x.Key, x.Value[0])).ToList());
		}	 
	}
}