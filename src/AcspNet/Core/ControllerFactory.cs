using System;
using AcspNet.DI;
using AcspNet.Modules;
using Microsoft.Owin;

namespace AcspNet.Core
{
	/// <summary>
	/// Controller factory
	/// </summary>
	public class ControllerFactory : ModulesAccessorFactory, IControllerFactory
	{
		private readonly IViewFactory _viewFactory;
		private readonly IAcspNetContextFactory _contextFactory;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerFactory" /> class.
		/// </summary>
		/// <param name="viewFactory">The view factory.</param>
		/// <param name="contextFactory">The context factory.</param>
		public ControllerFactory(IViewFactory viewFactory, IAcspNetContextFactory contextFactory)
		{
			_viewFactory = viewFactory;
			_contextFactory = contextFactory;
		}

		/// <summary>
		/// Creates the controller.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="controllerType">Type of the controller.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public IController CreateController(IDIContainerProvider containerProvider, Type controllerType, IOwinContext context)
		{
			var controller = (Controller)containerProvider.Resolve(controllerType);

			ConstructViewAccessor(containerProvider, _viewFactory, controller);
			ConstructModulesAccessor(containerProvider, controller);

			controller.Context = _contextFactory.Create(context);

			return controller;
		}
	}
}