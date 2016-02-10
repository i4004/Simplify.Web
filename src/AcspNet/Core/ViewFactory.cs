using System;
using Simplify.Web.Modules;

namespace Simplify.Web.Core
{
	/// <summary>
	/// View factory
	/// </summary>
	public class ViewFactory : ModulesAccessorBuilder, IViewFactory
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

			view.Language = containerProvider.Resolve<ILanguageManagerProvider>().Get().Language;
			view.SiteUrl = containerProvider.Resolve<IAcspNetContextProvider>().Get().SiteUrl;

			return view;
		}
	}
}