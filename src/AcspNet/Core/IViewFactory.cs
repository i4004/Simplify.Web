using System;
using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Represent view factory
	/// </summary>
	public interface IViewFactory
	{
		/// <summary>
		/// Creates the view.
		/// </summary>
		/// <param name="viewType">Type of the view.</param>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <returns></returns>
		View CreateView(Type viewType, IDIContainerProvider containerProvider);
	}
}