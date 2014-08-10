using AcspNet.Core;
using AcspNet.DI;

namespace AcspNet
{
	/// <summary>
	/// View accessor base class
	/// </summary>
	public abstract class ViewAccessor : IViewAccessor
	{
		internal virtual IViewFactory ViewFactory { get; set; }
		internal virtual IDIContainerProvider ContainerProvider { get; set; }
		
		/// <summary>
		/// Gets view instance
		/// </summary>
		/// <typeparam name="T">View instance to get</typeparam>
		/// <returns>View instance</returns>
		public T GetView<T>()
			where T : IView
		{
			var type = typeof(T);

			return (T)ViewFactory.CreateView(ContainerProvider, type);
		}
	}
}
