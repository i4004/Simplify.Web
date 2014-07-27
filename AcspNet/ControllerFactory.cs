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
		public IController CreateController(Type controllerType)
		{
			var controller = (IController)DependencyResolver.Current.Resolve(controllerType);

			return controller;
		}
	}
}