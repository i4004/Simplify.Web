using System;
using System.Threading.Tasks;
using AcspNet.Bootstrapper;
using AcspNet.Core;
using AcspNet.DI;
using AcspNet.Diagnostics;
using Microsoft.Owin;

namespace AcspNet.Owin
{
	/// <summary>
	/// AcspNet engine root
	/// </summary>
	public class AcspNetOwinMiddleware : OwinMiddleware
	{
		private readonly BaseAcspNetBootstrapper _bootstrapper = BootstrapperFactory.CreateBootstrapper();

		/// <summary>
		/// Initializes a new instance of the <see cref="AcspNetOwinMiddleware"/> class.
		/// </summary>
		/// <param name="next"></param>
		public AcspNetOwinMiddleware(OwinMiddleware next)
			: base(next)
		{
			_bootstrapper.Register();
		}

		/// <summary>
		/// Process an individual request.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override Task Invoke(IOwinContext context)
		{
			using (var scope = DIContainer.Current.BeginLifetimeScope())
			{
				try
				{
					var requestHandler = scope.Container.Resolve<IRequestHandler>();
					return requestHandler.ProcessRequest(scope.Container, context);
				}
				catch (Exception e)
				{
					return
						context.Response.WriteAsync(ExceptionInfoPageGenerator.Generate(e,
							scope.Container.Resolve<IAcspNetSettings>().HideExceptionDetails));
				}
			}
		}
	}
}