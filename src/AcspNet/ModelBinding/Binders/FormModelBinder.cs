using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;

namespace AcspNet.ModelBinding.Binders
{
	/// <summary>
	/// Provides form data to object (model) binding
	/// </summary>
	public static class FormModelBinder
	{
		/// <summary>
		/// Binds specifed form data to model.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T Bind<T>(IFormCollection form)
		{
			return ListModelBinder.Bind<T>(form.Select(x => new KeyValuePair<string, string>(x.Key, x.Value[0])).ToList());
		}
	}
}