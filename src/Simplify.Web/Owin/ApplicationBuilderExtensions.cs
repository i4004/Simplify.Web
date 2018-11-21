using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Simplify.Web.Bootstrapper;

namespace Simplify.Web.Owin
{
	/// <summary>
	/// IApplicationBuilder Simplify.Web extensions
	/// </summary>
	public static class ApplicationBuilderExtensions
	{
		/// <summary>
		/// Performs Simplify.Web bootstrapper registrations and adds Simplify.Web to the OWIN pipeline as a terminal middleware
		/// </summary>
		/// <param name="builder">The OWIN builder.</param>
		/// <param name="env">The hosting environment.</param>
		/// <returns></returns>
		public static IApplicationBuilder UseSimplifyWeb(this IApplicationBuilder builder, IHostingEnvironment env)
		{
			try
			{
				BootstrapperFactory.CreateBootstrapper().Register(env);

				builder.Run(async (context) => await SimplifyWebOwinMiddleware.Invoke(context));

				return builder;
			}
			catch (Exception e)
			{
				SimplifyWebOwinMiddleware.ProcessOnException(e);

				throw;
			}
		}

		/// <summary>
		/// Adds Simplify.Web to the OWIN pipeline as a terminal middleware without boostrapper registrations (useful when you want to control boostrapper registrations manually)
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <returns></returns>
		public static IApplicationBuilder UseSimplifyWebWithoutRegistrations(this IApplicationBuilder builder)
		{
			try
			{
				builder.Run(async (context) => await SimplifyWebOwinMiddleware.Invoke(context));

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