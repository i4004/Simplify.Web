using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Simplify.Web.Meta;
using Simplify.Web.Owin;

namespace Simplify.Web.Examples.Angular
{
	public class Startup
	{
		//public Startup(IConfiguration configuration)
		//{
		//	Configuration = configuration;
		//}

		//public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		//public void ConfigureServices(IServiceCollection services)
		//{
		//	//services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

		//	// In production, the Angular files will be served from this directory
		//	services.AddSpaStaticFiles(configuration =>
		//	{
		//		configuration.RootPath = "ClientApp/dist";
		//	});
		//}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			// Exclude Simplify.Web from exclude assemblies to be able to load example controllers
			SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Remove("Simplify");

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				//app.UseExceptionHandler("/Error");
				//app.UseHsts();
			}

			//app.UseHttpsRedirection();
			//app.UseStaticFiles();
			//app.UseSpaStaticFiles();

			//app.UseMvc(routes =>
			//{
			//    routes.MapRoute(
			//        name: "default",
			//        template: "{controller}/{action=Index}/{id?}");
			//});

			app.UseSimplifyWeb(env);

			//app.UseSpa(spa =>

			//{
			//	// To learn more about options for serving an Angular SPA from ASP.NET Core,
			//	// see https://go.microsoft.com/fwlink/?linkid=864501

			//	spa.Options.SourcePath = "ClientApp";

			//	if (env.IsDevelopment())
			//	{
			//		spa.UseAngularCliServer(npmScript: "start");
			//	}
			//});
		}
	}
}