using System;
using AcspNet.Modules;
using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// View factory
	/// </summary>
	public class ViewFactory : ModulesAccessorBuilder, IViewFactory
	{
		/// <summary>
		/// Creates the view.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="viewType">Type of the view.</param>
		/// <returns></returns>
		public IView CreateView(IDIContainerProvider containerProvider, Type viewType)
		{
			var view = (View)containerProvider.Resolve(viewType);

			BuildViewAccessorProperties(containerProvider, this, view);
			BuildModulesAccessorProperties(containerProvider, view);

			view.Language = containerProvider.Resolve<LanguageManagerProvider>().Get().Language;

			return view;
		}
	}
}