using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Provides builder for ViewAccessor objects construction
	/// </summary>
	public abstract class ViewAccessorBuilder : ModulesAccessorBuilder
	{
		/// <summary>
		/// Builds the view accessor properties.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="viewFactory">The view factory.</param>
		/// <param name="viewAccessor">The view accessor.</param>
		protected void BuildViewAccessorProperties(IDIContainerProvider containerProvider, IViewFactory viewFactory, ViewAccessor viewAccessor)
		{
			viewAccessor.ContainerProvider = containerProvider;
			viewAccessor.ViewFactory = viewFactory;		
		}
	}
}