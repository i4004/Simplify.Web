using System;

namespace AcspNet
{
	/// <summary>
	/// Represent view factory
	/// </summary>
	public interface IViewFactory
	{
		/// <summary>
		/// Creates the view.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <returns></returns>
		View CreateView(Type controllerType);
	}
}