using AcspNet.Modules;
using Microsoft.Owin;
using Simplify.DI;

namespace AcspNet.Core
{
	/// <summary>
	/// Provides page processor
	/// </summary>
	public class PageProcessor : IPageProcessor
	{
		private readonly IPageBuilder _pageBuilder;
		private readonly IResponseWriter _responseWriter;
		private readonly IRedirector _redirector;

		/// <summary>
		/// Initializes a new instance of the <see cref="PageProcessor"/> class.
		/// </summary>
		/// <param name="pageBuilder">The page builder.</param>
		/// <param name="responseWriter">The response writer.</param>
		/// <param name="redirector">The redirector.</param>
		public PageProcessor(IPageBuilder pageBuilder, IResponseWriter responseWriter, IRedirector redirector)
		{
			_pageBuilder = pageBuilder;
			_responseWriter = responseWriter;
			_redirector = redirector;
		}

		/// <summary>
		/// Processes (build web-page and send to client, process current page state) the current web-page
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <param name="context">The context.</param>
		public void ProcessPage(IDIContainerProvider containerProvider, IOwinContext context)
		{
			_responseWriter.Write(_pageBuilder.Build(containerProvider), context.Response);

			_redirector.PreviousPageUrl = context.Request.Uri.AbsoluteUri;
		}
	}
}