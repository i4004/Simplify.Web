using Microsoft.Owin;

namespace Simplify.Web.Core.StaticFiles
{
	public class StaticFileResponseFactory : IStaticFileResponseFactory
	{
		private readonly IResponseWriter _responseWriter;

		public StaticFileResponseFactory(IResponseWriter responseWriter)
		{
			_responseWriter = responseWriter;
		}

		public IStaticFileResponse Create(IOwinResponse response)
		{
			return new StaticFileResponse(response, _responseWriter);
		}
	}
}