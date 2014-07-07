using System.Threading.Tasks;
using Microsoft.Owin;

namespace AcspNet
{
	/// <summary>
	/// AcspNet OWIN request handler
	/// </summary>
	public class RequestHandler : OwinMiddleware
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RequestHandler"/> class.
		/// </summary>
		/// <param name="next"></param>
		public RequestHandler(OwinMiddleware next)
			: base(next)
		{
		}

		/// <summary>
		/// Process an individual request.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override Task Invoke(IOwinContext context)
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