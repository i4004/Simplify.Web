using AcspNet.ModelBinding.Binders;
using Simplify.DI;

namespace AcspNet
{
	/// <summary>
	/// AcspNet synchronous model controllers base class
	/// </summary>
	public abstract class Controller<T> : SyncControllerBase
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
			get { return _model ?? (_model = ContainerProvider.Resolve<IModelBinder>().Bind<T>()); }
		}
	}
}