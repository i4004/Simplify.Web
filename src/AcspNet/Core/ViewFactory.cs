using System;
using AcspNet.Modules;
using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// View factory
	/// </summary>
	public class ViewFactory : ViewAccessorBuilder, IViewFactory
	{
		/// <summary>
		/// Creates the view.
		/// </summary>
		/// <param name="viewType">Type of the view.</param>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <returns></returns>
		public View CreateView(Type viewType, IDIContainerProvider containerProvider)
		{
			var view = (View)containerProvider.Resolve(viewType);

			BuildModulesAccessorProperties(view, containerProvider);
			BuildViewAccessorProperties(view, containerProvider, this);

			view.Language = containerProvider.Resolve<ILanguageManagerProvider>().Get().Language;

			return view;
		}
	}
}