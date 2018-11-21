using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Simplify.DI;
using Simplify.DI.Provider.SimpleInjector;
using Simplify.Web.Meta;
using Simplify.Web.Owin;

namespace Simplify.Web.Examples.SelfHosted
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			// Exclude Simplify.Web from exclude assemblies to be able to load example controllers
			SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Remove("Simplify");

			var provider = new SimpleInjectorDIProvider();
			DIContainer.Current = provider;

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseAuthentication();

			app.UseSimplifyWeb(env);

			provider.Container.Verify();
		}
	}
}