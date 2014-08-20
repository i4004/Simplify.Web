using System;
using AcspNet.Modules;
using Microsoft.Owin;
using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Controller factory
	/// </summary>
	public class ControllerFactory : ViewAccessorBuilder, IControllerFactory
	{
		/// <summary>
		/// Creates the controller.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="context">The context.</param>
		/// <param name="routeParameters">The route parameters.</param>
		/// <returns></returns>
		public Controller CreateController(Type controllerType, IDIContainerProvider containerProvider, IOwinContext context, dynamic routeParameters = null)
		{
			var controller = (Controller)containerProvider.Resolve(controllerType);

			BuildModulesAccessorProperties(controller, containerProvider);
			BuildViewAccessorProperties(controller, containerProvider, containerProvider.Resolve<IViewFactory>());

			controller.RouteParameters = routeParameters;
			controller.Context = containerProvider.Resolve<IAcspNetContextProvider>().Get();
			controller.LanguageManager = containerProvider.Resolve<ILanguageManagerProvider>().Get();
			controller.DataCollector = containerProvider.Resolve<IDataCollector>();
			controller.FileReader = containerProvider.Resolve<IFileReader>();
			controller.Redirector = containerProvider.Resolve<IRedirector>();

			return controller;
		}
	}
}