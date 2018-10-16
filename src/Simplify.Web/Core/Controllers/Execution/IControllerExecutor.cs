using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Simplify.DI;

namespace Simplify.Web.Core.Controllers.Execution
{
	/// <summary>
	/// Represent controller executor, handles creation and execution of controllers
	/// </summary>
	public interface IControllerExecutor
	{
		/// <summary>
		/// Creates and executes the specified controller.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <param name="containerProvider">The container provider.</param>
		/// <param name="context">The context.</param>
		/// <param name="routeParameters">The route parameters.</param>
		/// <returns></returns>
		ControllerResponseResult Execute(Type controllerType, IDIContainerProvider containerProvider, HttpContext context,
			dynamic routeParameters = null);

		/// <summary>
		/// Processes the asynchronous controllers responses.
		/// </summary>
		/// <param name="containerProvider">The container provider.</param>
		/// <returns></returns>
		IEnumerable<ControllerResponseResult> ProcessAsyncControllersResponses(IDIContainerProvider containerProvider);
	}
}