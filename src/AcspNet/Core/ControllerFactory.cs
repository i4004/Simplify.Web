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
		private readonly IViewFactory _viewFactory;
		private readonly IAcspNetContextProvider _contextProvider;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerFactory" /> class.
		/// </summary>
		/// <param name="viewFactory">The view factory.</param>
		/// <param name="contextProvider">The context factory.</param>
		public ControllerFactory(IViewFactory viewFactory, IAcspNetContextProvider contextProvider)
		{
			_viewFactory = viewFactory;
			_contextProvider = contextProvider;
		}

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
			BuildViewAccessorProperties(controller, containerProvider, _viewFactory);

			controller.RouteParameters = routeParameters;
			controller.Context = _contextProvider.Get();
			controller.LanguageManager = containerProvider.Resolve<ILanguageManagerProvider>().Get();

			return controller;
		}
	}
}