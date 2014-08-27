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
		private readonly IPageProcessor _pageProcessor;

		/// <summary>
		/// Initializes a new instance of the <see cref="RequestHandler" /> class.
		/// </summary>
		/// <param name="controllersRequestHandler">The controllers request handler.</param>
		/// <param name="pageProcessor">The page processor.</param>
		public RequestHandler(IControllersRequestHandler controllersRequestHandler, IPageProcessor pageProcessor)
		{
			_controllersRequestHandler = controllersRequestHandler;
			_pageProcessor = pageProcessor;
		}

		/// <summary>
		/// Processes the OWIN HTTP request.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public Task ProcessRequest(IDIContainerProvider containerProvider, IOwinContext context)
		{
			var result = _controllersRequestHandler.ProcessRequest(containerProvider, context);

			switch (result)
			{
				case ControllersRequestHandlerResult.Ok:
					return _pageProcessor.ProcessPage(containerProvider, context);

				case ControllersRequestHandlerResult.Http401:
					context.Response.StatusCode = 401;
					break;

				case ControllersRequestHandlerResult.Http403:
					context.Response.StatusCode = 403;
					break;

				case ControllersRequestHandlerResult.Http404:
					context.Response.StatusCode = 404;
					break;
			}

			return Task.Delay(0);
		}
	}
}
