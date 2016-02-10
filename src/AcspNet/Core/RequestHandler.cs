using System.Threading.Tasks;

namespace Simplify.Web.Core
{
	/// <summary>
	/// Provides OWIN HTTP request handler
	/// </summary>
	public class RequestHandler : IRequestHandler
	{
		private readonly IControllersRequestHandler _controllersRequestHandler;
		private readonly IStaticFilesRequestHandler _staticFilesRequestHandler;

		/// <summary>
		/// Initializes a new instance of the <see cref="RequestHandler" /> class.
		/// </summary>
		/// <param name="controllersRequestHandler">The controllers request handler.</param>
		/// <param name="staticFilesRequestHandler">The static files request handler.</param>
		public RequestHandler(IControllersRequestHandler controllersRequestHandler, IStaticFilesRequestHandler staticFilesRequestHandler)
		{
			_controllersRequestHandler = controllersRequestHandler;
			_staticFilesRequestHandler = staticFilesRequestHandler;
		}

		/// <summary>
		/// Processes the OWIN HTTP request.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public Task ProcessRequest(IDIContainerProvider containerProvider, IOwinContext context)
		{
			return _staticFilesRequestHandler.IsStaticFileRoutePath(context)
				? _staticFilesRequestHandler.ProcessRequest(context)
				: _controllersRequestHandler.ProcessRequest(containerProvider, context);
		}
	}
}