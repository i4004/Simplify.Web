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
		/// <param name="viewType">Type of the view.</param>
		/// <returns></returns>
		public IView CreateView(Type viewType)
		{
			var view = (IView)DIContainer.Current.Resolve(viewType);

			return view;
		}
	}
}