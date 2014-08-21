using System;
using System.Collections.Generic;
using Microsoft.Owin;
using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Represent controllers processor, handles creation and execution of controllers
	/// </summary>
	public interface IControllersProcessor
	{
		/// <summary>
		/// Processes the specified controller (creates and executes controller).
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <param name="containerProvider">The container provider.</param>
		/// <param name="context">The context.</param>
		/// <param name="routeParameters">The route parameters.</param>
		/// <returns></returns>
		ControllerResponseResult Process(Type controllerType, IDIContainerProvider containerProvider, IOwinContext context,
			dynamic routeParameters = null);

		/// <summary>
		/// Processes the asynchronous controllers responses.
		/// </summary>
		/// <param name="containerProvider">The container provider.</param>
		/// <returns></returns>
		IEnumerable<ControllerResponseResult> ProcessAsyncControllersResponses(IDIContainerProvider containerProvider);
	}
}