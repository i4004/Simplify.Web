using System;
using Microsoft.AspNetCore.Http;
using Simplify.DI;

namespace Simplify.Web.Core.Controllers.Execution.Building
{
	/// <summary>
	/// Represent controller factory
	/// </summary>
	public interface IControllerFactory
	{
		/// <summary>
		/// Creates the controller.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="context">The context.</param>
		/// <param name="routeParameters">The route parameters.</param>
		/// <returns></returns>
		ControllerBase CreateController(Type controllerType, IDIContainerProvider containerProvider, HttpContext context, dynamic routeParameters = null);
	}
}