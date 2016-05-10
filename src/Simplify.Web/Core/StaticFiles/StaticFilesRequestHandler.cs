using System.Threading.Tasks;
using Microsoft.Owin;
using Simplify.Web.Owin;

namespace Simplify.Web.Core.StaticFiles
{
	/// <summary>
	/// Provides static files request handler
	/// </summary>
	public class StaticFilesRequestHandler : IStaticFilesRequestHandler
	{
		private readonly IStaticFileHandler _fileHandler;
		private readonly IStaticFileResponseFactory _responseFactory;

		/// <summary>
		/// Initializes a new instance of the <see cref="StaticFilesRequestHandler" /> class.
		/// </summary>
		/// <param name="fileHandler">The file handler.</param>
		/// <param name="responseFactory">The response factory.</param>
		public StaticFilesRequestHandler(IStaticFileHandler fileHandler, IStaticFileResponseFactory responseFactory)
		{
			_fileHandler = fileHandler;
			_responseFactory = responseFactory;
		}

		/// <summary>
		/// Determines whether current route path is for static file.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public bool IsStaticFileRoutePath(IOwinContext context)
		{
			return _fileHandler.IsStaticFileRoutePath(GetRelativeFilePath(context.Request));
		}

		/// <summary>
		/// Processes the HTTP request for static files.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public Task ProcessRequest(IOwinContext context)
		{
			var ifModifiedSinceTime = OwinRequestHelper.GetIfModifiedSinceTime(context.Request.Headers);
			var relativeFilePath = GetRelativeFilePath(context.Request);
			var lastModificationTime = _fileHandler.GetFileLastModificationTime(relativeFilePath);
			var response = _responseFactory.Create(context.Response);

			response.SetMimeType(relativeFilePath);

			return _fileHandler.IsFileCanBeUsedFromCache(context.Request.CacheControl, ifModifiedSinceTime, lastModificationTime)
				? response.SendNotModified(lastModificationTime)
				: response.SendNew(_fileHandler.GetFileData(relativeFilePath), lastModificationTime);
		}

		private static string GetRelativeFilePath(IOwinRequest request)
		{
			return request.Path.ToString().Substring(1);
		}
	}
}