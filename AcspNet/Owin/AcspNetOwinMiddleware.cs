using System;
using System.Threading.Tasks;
using DryIoc;
using Microsoft.Owin;
using Simplify.Core;

namespace AcspNet.Owin
{
	/// <summary>
	/// AcspNet OWIN request handler
	/// </summary>
	public class AcspNetOwinMiddleware : OwinMiddleware
	{
		readonly Container _container = new Container();

		/// <summary>
		/// Initializes a new instance of the <see cref="AcspNetOwinMiddleware"/> class.
		/// </summary>
		/// <param name="next"></param>
		public AcspNetOwinMiddleware(OwinMiddleware next)
			: base(next)
		{
			_container.Register<IRequestHandler, RequestHandler>(Reuse.InResolutionScope);
			_container.RegisterAll<IRequestHandler>();
		}

		/// <summary>
		/// Process an individual request.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override Task Invoke(IOwinContext context)
		{
			try
			{
				var request = _container.Resolve<RequestHandler>();
				request.ProcessRequest(context);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return context.Response.WriteAsync(e.ToString());
			}

			return Task.Delay(0);
		}
	}
}