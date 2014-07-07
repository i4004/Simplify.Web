using System.Threading.Tasks;
using Microsoft.Owin;

namespace AcspNet
{
	/// <summary>
	/// HTTP request handler
	/// </summary>
	public class RequestHandler : IRequestHandler
	{
		public Task ProcessRequest(IOwinContext context)
		{
			if (context.Request.Path == new PathString("/"))
			{
				context.Response.ContentType = "text/plain";
				return context.Response.WriteAsync("Hello World from AcspNet!");
			}

			context.Response.StatusCode = 404;

			return Task.Delay(0);
		}
	}
}
