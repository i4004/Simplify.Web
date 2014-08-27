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
		/// Process HTTP request to controllers
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		ControllersRequestHandlerResult ProcessRequest(IDIContainerProvider containerProvider, IOwinContext context);
	}
}