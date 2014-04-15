using System;

namespace AcspNet
{
	public class ControllerFactory : IControllerFactory
	{
		public Controller CreateController(Type controllerType)
		{
			return (Controller)DependencyResolver.Current.Resolve(controllerType);
		}
	}
}