using System;
using Microsoft.AspNetCore.Builder;
using Simplify.Web.Bootstrapper;
using Simplify.Web.Core;

namespace Simplify.Web.Owin
{
	/// <summary>
	/// IApplicationBuilder Simplify.Web extensions
	/// </summary>
	public static class ApplicationBuilderExtensions
	{
		/// <summary>
		/// Gets or sets a value indicating whether Simplify.Web is terminal middleware.
		/// </summary>
		public static bool TerminalMiddleware { get; set; } = true;

		/// <summary>
		/// Performs Simplify.Web bootstrapper registrations and adds Simplify.Web to the OWIN pipeline as a terminal middleware
		/// </summary>
		/// <param name="builder">The OWIN builder.</param>
		/// <returns></returns>
		public static IApplicationBuilder UseSimplifyWeb(this IApplicationBuilder builder)
		{
			try
			{
				TerminalMiddleware = true;
				BootstrapperFactory.CreateBootstrapper().Register();

				builder.Run(async (context) => await SimplifyWebOwinMiddleware.Invoke(context).Task);

				return builder;
			}
			catch (Exception e)
			{
				SimplifyWebOwinMiddleware.ProcessOnException(e);

				throw;
			}
		}

		/// <summary>
		/// Adds Simplify.Web to the OWIN pipeline as a terminal middleware without bootstrapper registrations (useful when you want to control bootstrapper registrations manually)
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <returns></returns>
		public static IApplicationBuilder UseSimplifyWebWithoutRegistrations(this IApplicationBuilder builder)
		{
			try
			{
				TerminalMiddleware = false;

				builder.Run(async (context) => await SimplifyWebOwinMiddleware.Invoke(context).Task);

				return builder;
			}
			catch (Exception e)
			{
				SimplifyWebOwinMiddleware.ProcessOnException(e);

				throw;
			}
		}

		/// <summary>
		/// Performs Simplify.Web bootstrapper registrations and adds Simplify.Web to the OWIN pipeline as a non-terminal middleware
		/// </summary>
		/// <param name="builder">The OWIN builder.</param>
		/// <returns></returns>
		public static IApplicationBuilder UseSimplifyWebNonTerminal(this IApplicationBuilder builder)
		{
			try
			{
				TerminalMiddleware = false;
				BootstrapperFactory.CreateBootstrapper().Register();

				builder.Use(async (context, next) =>
				{
					var result = SimplifyWebOwinMiddleware.Invoke(context);

					await result.Task;

					if (result.Status == RequestHandlingStatus.RequestWasUnhandled)
						await next.Invoke();
				});

				return builder;
			}
			catch (Exception e)
			{
				SimplifyWebOwinMiddleware.ProcessOnException(e);

				throw;
			}
		}
	}
}