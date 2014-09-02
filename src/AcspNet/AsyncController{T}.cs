using AcspNet.ModelBinding;
using Simplify.DI;

namespace AcspNet
{
	/// <summary>
	/// AcspNet asynchronous model controllers base class
	/// </summary>
	public abstract class AsyncController<T> : AsyncControllerBase
		where T : class
	{
		private T _model;

		/// <summary>
		/// Gets the model of current request.
		/// </summary>
		/// <value>
		/// The current request model.
		/// </value>
		public virtual T Model
		{
			get { return _model ?? (_model = ContainerProvider.Resolve<IModelDeserializer>().Deserialize<T>()); }
		}
	}
}