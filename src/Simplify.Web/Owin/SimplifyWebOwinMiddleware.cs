using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Simplify.DI;
using Simplify.Web.Core;
using Simplify.Web.Diagnostics;
using Simplify.Web.Settings;

namespace Simplify.Web.Owin
{
	/// <summary>
	/// Catched exceptions delegate
	/// </summary>
	/// <param name="e">The e.</param>
	public delegate void ExceptionEventHandler(Exception e);

	/// <summary>
	/// HTTP requests trace delegate
	/// </summary>
	public delegate void TraceEventHandler(HttpContext context);

	/// <summary>
	/// Simplify.Web engine root
	/// </summary>
	public class SimplifyWebOwinMiddleware
	{
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
		public static Task Invoke(HttpContext context)
		{
			using (var scope = DIContainer.Current.BeginLifetimeScope())
			{
				try
				{
					// Starts execution measurement
					scope.Container.Resolve<IStopwatchProvider>().StartMeasurement();

					// Tracing

					var settings = scope.Container.Resolve<ISimplifyWebSettings>();

					if (settings.ConsoleTracing)
						TraceToConsole(context);

					OnTrace?.Invoke(context);

					//// Setup providers

					//var webContextProvider = scope.Container.Resolve<IWebContextProvider>();
					//var languageManagerProvider = scope.Container.Resolve<ILanguageManagerProvider>();
					//var templateFactory = scope.Container.Resolve<ITemplateFactory>();
					//var fileReader = scope.Container.Resolve<IFileReader>();
					//var stringTable = scope.Container.Resolve<IStringTable>();

					//webContextProvider.Setup(context);
					//languageManagerProvider.Setup(context);
					//templateFactory.Setup();
					//fileReader.Setup();
					//stringTable.Setup();

					//// Run request process pipeline

					//var requestHandler = scope.Container.Resolve<IRequestHandler>();
					//return requestHandler.ProcessRequest(scope.Container, context);
					return Task.Delay(10);
				}
				catch (Exception e)
				{
					try
					{
						context.Response.StatusCode = 500;

						ProcessOnException(e);
					}
					catch (Exception exception)
					{
						return
							context.Response.WriteAsync(ExceptionInfoPageGenerator.Generate(exception,
								scope.Container.Resolve<ISimplifyWebSettings>().HideExceptionDetails));
					}

					return
						context.Response.WriteAsync(ExceptionInfoPageGenerator.Generate(e,
							scope.Container.Resolve<ISimplifyWebSettings>().HideExceptionDetails));
				}
			}
		}

		internal static bool ProcessOnException(Exception e)
		{
			if (OnException == null) return false;

			OnException(e);
			return true;
		}

		private static void TraceToConsole(HttpContext context)
		{
			Trace.WriteLine(
				$"[{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss:fff", CultureInfo.InvariantCulture)}] [{context.Request.Method}] {context.Request.GetDisplayUrl()}");
		}
	}
}