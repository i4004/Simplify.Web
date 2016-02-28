using System.Threading.Tasks;
using Microsoft.Owin;
using Simplify.DI;

namespace Simplify.Web.Core
{
	/// <summary>
	/// Provides controllers HTTP request handler
	/// </summary>
	public class ControllersRequestHandler : IControllersRequestHandler
	{
		private readonly IControllersProcessor _controllersProcessor;
		private readonly IPageProcessor _pageProcessor;

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllersRequestHandler" /> class.
		/// </summary>
		/// <param name="controllersProcessor">The controllers request handler.</param>
		/// <param name="pageProcessor">The page processor.</param>
		public ControllersRequestHandler(IControllersProcessor controllersProcessor, IPageProcessor pageProcessor)
		{
			_controllersProcessor = controllersProcessor;
			_pageProcessor = pageProcessor;
		}

		/// <summary>
		/// Processes the HTTP request for controllers.
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public Task ProcessRequest(IDIContainerProvider containerProvider, IOwinContext context)
		{
			var result = _controllersProcessor.ProcessControllers(containerProvider, context);

			switch (result)
			{
				case ControllersProcessorResult.Ok:
					return _pageProcessor.ProcessPage(containerProvider, context);

				case ControllersProcessorResult.Http401:
					context.Response.StatusCode = 401;
					break;

				case ControllersProcessorResult.Http403:
					context.Response.StatusCode = 403;
					break;

				case ControllersProcessorResult.Http404:
					context.Response.StatusCode = 404;
					break;
			}

			return Task.Delay(0);
		}
	}
}