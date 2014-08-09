using System;
using AcspNet.DI;
using AcspNet.DryIoc;

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
		/// <param name="controllerType">Type of the controller.</param>
		/// <returns></returns>
		public IController CreateController(Type controllerType)
		{
			var controller = (Controller)DependencyResolver.Container.Resolve(controllerType);

			controller.Context = null;

			return controller;
		}
	}
}