using System;
using System.Threading.Tasks;
using AcspNet.Core;
using AcspNet.Diagnostics;
using AcspNet.Modules;
using Microsoft.Owin;
using Simplify.DI;

namespace AcspNet.Owin
{
	/// <summary>
	/// AcspNet engine root
	/// </summary>
	public class AcspNetOwinMiddleware : OwinMiddleware
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AcspNetOwinMiddleware"/> class.
		/// </summary>
		/// <param name="next">The next middleware.</param>
		public AcspNetOwinMiddleware(OwinMiddleware next)
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
			using (var scope = DIContainer.Current.BeginLifetimeScope())
			{
				try
				{
					// Starts execution measurement
					scope.Container.Resolve<IStopwatchProvider>().StartMeasurement();

					// Setup providers

					var acspNetContextProvider = scope.Container.Resolve<IAcspNetContextProvider>();
					var languageManagerProvider = scope.Container.Resolve<ILanguageManagerProvider>();
					var templateFactory = scope.Container.Resolve<ITemplateFactory>();
					var fileReader = scope.Container.Resolve<IFileReader>();
					var stringTable = scope.Container.Resolve<IStringTable>();

					acspNetContextProvider.Setup(context);
					languageManagerProvider.Setup(context);
					templateFactory.Setup();
					fileReader.Setup();
					stringTable.Setup();

					// Run request process pipeline

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