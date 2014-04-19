using System;

namespace AcspNet
{
	/// <summary>
	/// Represents controlles and views creation factory
	/// </summary>
	public interface IContainerFactory : IHideObjectMembers
	{
		/// <summary>
		/// Creates the controller or views derived from container class.
		/// </summary>
		/// <param name="controllerType">Type of the controller.</param>
		/// <returns></returns>
		Container CreateContainer(Type controllerType);
	}
}