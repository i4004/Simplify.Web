using System;
using System.Security.Cryptography.X509Certificates;

namespace AcspNet
{
	public interface IControllerFactory : IHideObjectMembers
	{
		Controller CreateController(Type controllerType);
	}
}