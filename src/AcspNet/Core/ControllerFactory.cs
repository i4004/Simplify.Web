using System;
using AcspNet.DI;
using AcspNet.Modules;

namespace AcspNet.Core
{
	/// <summary>
	/// Controller factory
	/// </summary>
	public class ControllerFactory : IControllerFactory
	{
		/// <summary>
		/// Creates the controller.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="controllerType">Type of the controller.</param>
		/// <returns></returns>
		public IController CreateController(IDIContainerProvider containerProvider, Type controllerType)
		{
			var controller = (Controller)containerProvider.Resolve(controllerType);

			controller.Environment = containerProvider.Resolve<IEnvironment>();

			return controller;
		}
	}
}