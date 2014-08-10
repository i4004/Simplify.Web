using System;
using AcspNet.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Controller factory
	/// </summary>
	public class ControllerFactory : ModulesAccessorFactory, IControllerFactory
	{
		private readonly IViewFactory _viewFactory;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerFactory"/> class.
		/// </summary>
		/// <param name="viewFactory">The view factory.</param>
		public ControllerFactory(IViewFactory viewFactory)
		{
			_viewFactory = viewFactory;
		}

		/// <summary>
		/// Creates the controller.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="controllerType">Type of the controller.</param>
		/// <returns></returns>
		public IController CreateController(IDIContainerProvider containerProvider, Type controllerType)
		{
			var controller = (Controller)containerProvider.Resolve(controllerType);

			ConstructViewAccessor(containerProvider, _viewFactory, controller);
			ConstructModulesAccessor(containerProvider, controller);

			return controller;
		}
	}
}