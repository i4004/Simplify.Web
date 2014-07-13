using System;
using Simplify.Core;

namespace AcspNet
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
		public Controller CreateController(Type controllerType)
		{
			var controller = (Controller)DependencyResolver.Current.Resolve(controllerType);

			return controller;
		}
	}
}