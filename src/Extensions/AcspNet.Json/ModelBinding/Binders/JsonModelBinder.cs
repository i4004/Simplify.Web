using System.IO;
using Newtonsoft.Json;

namespace AcspNet.ModelBinding.Binders
{
	/// <summary>
	/// Provides HTTP request JSON data to object binding
	/// </summary>
	public class JsonModelBinder : IModelBinder
	{
		/// <summary>
		/// Binds the specified JSON data to object.
		/// </summary>
		/// <typeparam name="T">Model type</typeparam>
		/// <param name="args">The <see cref="AcspNet.ModelBinding.ModelBinderEventArgs{T}"/> instance containing the event data.</param>
		public void Bind<T>(ModelBinderEventArgs<T> args)
		{
			if (args.Context.Request.ContentType.Contains("application/json"))
				args.SetModel(JsonConvert.DeserializeObject<T>(new StreamReader(args.Context.Request.Body).ReadToEnd()));
		}
	}
}