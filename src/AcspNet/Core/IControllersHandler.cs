using AcspNet.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Represents controllers handler
	/// </summary>
	public interface IControllersHandler
	{
		/// <summary>
		/// Creates and invokes controllers instances.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="route">The route path.</param>
		/// <param name="method">The HTTP method.</param>
		/// <returns></returns>
		ControllersHandlerResult Execute(IDIContainerProvider containerProvider, string route, string method);
	}
}