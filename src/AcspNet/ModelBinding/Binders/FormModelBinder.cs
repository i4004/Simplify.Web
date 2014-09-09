using System.Collections.Generic;
using System.Linq;

namespace AcspNet.ModelBinding.Binders
{
	/// <summary>
	/// Provides form data to object (model) binding
	/// </summary>
	public class FormModelBinder : IModelBinder
	{
		/// <summary>
		/// Binds specifed form data to model.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public void Bind<T>(ModelBinderEventArgs<T> args)
		{
			if (args.Context.Request.ContentType.Contains("application/x-www-form-urlencoded"))
				args.SetModel(
					ListModelParser.Parse<T>(args.Context.Form.Select(x => new KeyValuePair<string, string>(x.Key, x.Value[0])).ToList()));
		}
	}
}