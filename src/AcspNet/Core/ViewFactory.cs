using System;
using AcspNet.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// View factory
	/// </summary>
	public class ViewFactory : IViewFactory
	{
		/// <summary>
		/// Creates the view.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="viewType">Type of the view.</param>
		/// <returns></returns>
		public IView CreateView(IDIContainerProvider containerProvider, Type viewType)
		{
			var view = (IView)containerProvider.Resolve(viewType);

			return view;
		}
	}
}