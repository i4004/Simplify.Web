using System;
using AcspNet.DI;

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
			var controller = (Controller)DIContainer.Current.Resolve(controllerType);

			return controller;
		}
	}
}