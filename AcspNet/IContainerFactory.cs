using System;

namespace AcspNet
{
	public interface IContainerFactory : IHideObjectMembers
	{
		Controller CreateController(Type controllerType);
	}
}