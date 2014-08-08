using System;

namespace AcspNet.Core
{
	/// <summary>
	/// Represent controller factory
	/// </summary>
	public interface IControllerFactory
	{
		/// <summary>
		/// Creates the controller.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <returns></returns>
		IController CreateController(Type controllerType);
	}
}