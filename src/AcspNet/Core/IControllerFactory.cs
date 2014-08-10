using System;
using AcspNet.DI;
using Microsoft.Owin;

namespace AcspNet.Core
{
	/// <summary>
	/// Represent controller factory
	/// </summary>
	public interface IControllerFactory
	{
		/// <summary>
		/// Creates the controller.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="controllerType">Type of the controller.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		IController CreateController(IDIContainerProvider containerProvider, Type controllerType, IOwinContext context);
	}
}