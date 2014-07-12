using System;
using System.Threading.Tasks;
using AcspNet.Bootstrapper;
using DryIoc;
using Microsoft.Owin;

namespace AcspNet.Owin
{
	/// <summary>
	/// AcspNet engine root
	/// </summary>
	public class AcspNetOwinMiddleware : OwinMiddleware
	{
		private readonly Container _container = new Container();
		private readonly BootstrapperFactory _bootstrapperFactory = new BootstrapperFactory();

		/// <summary>
		/// Initializes a new instance of the <see cref="AcspNetOwinMiddleware"/> class.
		/// </summary>
		/// <param name="next"></param>
		public AcspNetOwinMiddleware(OwinMiddleware next)
			: base(next)
		{
			var bs = _bootstrapperFactory.CreateBootstrapper();

			// Registering all AcspNet pipeline types

			_container.Register(typeof (IRequestHandler), bs.RequestHandlerType, Reuse.InResolutionScope);
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
				var request = _container.Resolve<IRequestHandler>();
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