using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.DependencyInjection;
using Simplify.Web.Meta;
using Simplify.Web.Owin;

namespace Simplify.Web.Examples.Angular
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			// In production, the Angular files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/dist";
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			// Exclude Simplify.Web from exclude assemblies to be able to load example controllers
			SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Remove("Simplify");

			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			app.UseSimplifyWebNonTerminal();

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
					spa.UseAngularCliServer("start");
			});
		}
	}
}