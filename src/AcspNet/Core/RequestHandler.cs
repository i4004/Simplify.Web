using System.Threading.Tasks;
using Microsoft.Owin;
using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Provides OWIN HTTP request handler
	/// </summary>
	public class RequestHandler : IRequestHandler
	{
		private readonly IControllersRequestHandler _controllersRequestHandler;

		public RequestHandler(IControllersRequestHandler controllersRequestHandler)
		{
			_controllersRequestHandler = controllersRequestHandler;
		}

		/// <summary>
		/// Processes the OWIN HTTP request.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public Task ProcessRequest(IDIContainerProvider containerProvider, IOwinContext context)
		{
			return _controllersRequestHandler.ProcessRequest(containerProvider, context);
		}
	}
}