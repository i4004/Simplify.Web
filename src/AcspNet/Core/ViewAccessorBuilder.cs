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
		/// <param name="viewAccessor">The view accessor.</param>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="viewFactory">The view factory.</param>
		protected void BuildViewAccessorProperties(ViewAccessor viewAccessor, IDIContainerProvider containerProvider, IViewFactory viewFactory)
		{
			viewAccessor.ContainerProvider = containerProvider;
			viewAccessor.ViewFactory = viewFactory;		
		}
	}
}