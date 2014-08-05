using System;

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
		/// <returns></returns>
		IView CreateView(Type viewType);
	}
}