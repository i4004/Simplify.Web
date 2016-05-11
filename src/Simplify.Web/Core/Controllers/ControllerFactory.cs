using System;
using Microsoft.Owin;
using Simplify.DI;

namespace Simplify.Web.Core.Controllers
{
	/// <summary>
	/// Controller factory
	/// </summary>
	public class ControllerFactory : ActionModulesAccessorBuilder, IControllerFactory
	{
		/// <summary>
		/// Creates the controller.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="context">The context.</param>
		/// <param name="routeParameters">The route parameters.</param>
		/// <returns></returns>
		public ControllerBase CreateController(Type controllerType, IDIContainerProvider containerProvider, IOwinContext context, dynamic routeParameters = null)
		{
			var controller = (ControllerBase)containerProvider.Resolve(controllerType);

			BuildActionModulesAccessorProperties(controller, containerProvider);

			controller.RouteParameters = routeParameters;

			return controller;
		}
	}
}