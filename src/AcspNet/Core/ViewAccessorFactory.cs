using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Provides factory for ViewAccessor objects construction
	/// </summary>
	public abstract class ViewAccessorFactory
	{
		/// <summary>
		/// Constructs the view accessor.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="viewFactory">The view factory.</param>
		/// <param name="viewAccessor">The view accessor.</param>
		protected void ConstructViewAccessor(IDIContainerProvider containerProvider, IViewFactory viewFactory, ViewAccessor viewAccessor)
		{
			viewAccessor.ContainerProvider = containerProvider;
			viewAccessor.ViewFactory = viewFactory;		
		}
	}
}