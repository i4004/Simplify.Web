using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Simplify.DI;
using Simplify.DI.Provider.SimpleInjector;
using Simplify.Web.Owin;

namespace SampleApp.SelfHosted
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
			var provider = new SimpleInjectorDIProvider();
			DIContainer.Current = provider;

			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			app.UseAuthentication();

			app.UseSimplifyWeb();

			provider.Container.Verify();
		}
	}
}