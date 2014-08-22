using Microsoft.Owin;
using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Represents controllers request handler
	/// </summary>
	public interface IControllersRequestHandler
	{
		/// <summary>
		/// Creates and invokes controllers instances.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		ControllersHandlerResult Execute(IDIContainerProvider containerProvider, IOwinContext context);
	}
}