using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Simplify.Web.Core;
using Simplify.Web.Diagnostics;
using Simplify.Web.Modules;

namespace Simplify.Web.Owin
{
	/// <summary>
	/// AcspNet catched exceptions delegate
	/// </summary>
	/// <param name="e">The e.</param>
	public delegate void ExceptionEventHandler(Exception e);

	/// <summary>
	/// AcspNet trace delegate
	/// </summary>
	public delegate void TraceEventHandler(IOwinContext context);

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
		/// Occurs when exception occured and catched by framework.
		/// </summary>
		public static event ExceptionEventHandler OnException;

		/// <summary>
		/// Occurs on each request.
		/// </summary>
		public static event TraceEventHandler OnTrace;

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

					// Tracing

					var settings = scope.Container.Resolve<IAcspNetSettings>();

					if (settings.ConsoleTracing)
						TraceToConsole(context);

					if (OnTrace != null)
						OnTrace(context);

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
					try
					{
						ProcessOnException(e);
					}
					catch (Exception exception)
					{
						return
							context.Response.WriteAsync(ExceptionInfoPageGenerator.Generate(exception,
								scope.Container.Resolve<IAcspNetSettings>().HideExceptionDetails));
					}

					return
						context.Response.WriteAsync(ExceptionInfoPageGenerator.Generate(e,
							scope.Container.Resolve<IAcspNetSettings>().HideExceptionDetails));
				}
			}
		}

		internal static bool ProcessOnException(Exception e)
		{
			if (OnException == null) return false;

			OnException(e);
			return true;
		}

		private static void TraceToConsole(IOwinContext context)
		{
			Trace.WriteLine(string.Format("[{0}] [{1}] {2}", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:fff", CultureInfo.InvariantCulture), context.Request.Method, context.Request.Uri.AbsoluteUri));
		}
	}
}