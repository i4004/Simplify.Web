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
		private readonly IControllersHandler _controllersHandler;
		private readonly IPageBuilder _pageBuilder;
		private readonly IResponseWriter _responseWriter;

		/// <summary>
		/// Initializes a new instance of the <see cref="RequestHandler" /> class.
		/// </summary>
		/// <param name="controllersHandler">The controllers handler.</param>
		/// <param name="pageBuilder">The page builder.</param>
		/// <param name="responseWriter">The response writer.</param>
		public RequestHandler(IControllersHandler controllersHandler, IPageBuilder pageBuilder, IResponseWriter responseWriter)
		{
			_controllersHandler = controllersHandler;
			_pageBuilder = pageBuilder;
			_responseWriter = responseWriter;
		}

		/// <summary>
		/// Processes the OWIN HTTP request.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public Task ProcessRequest(IDIContainerProvider containerProvider, IOwinContext context)
		{
			var result = _controllersHandler.Execute(containerProvider, context);

			switch (result)
			{
				case ControllersHandlerResult.Ok:
					_responseWriter.Write(_pageBuilder.Build(containerProvider), context.Response);
					break;

				case ControllersHandlerResult.Http404:
					context.Response.StatusCode = 404;
					break;
			}

			return Task.Delay(0);
		}
	}
}
