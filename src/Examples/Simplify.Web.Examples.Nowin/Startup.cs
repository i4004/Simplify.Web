using Owin;
using Simplify.DI;
using Simplify.DI.Provider.SimpleInjector;
using Simplify.Web.Meta;
using Simplify.Web.Owin;

namespace Simplify.Web.Examples.Nowin
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// Exclude Simplify.Web from exclude assemblies to be able to load example controllers
			SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Remove("Simplify.Web");

			DIContainer.Current = new SimpleInjectorDIProvider();

			app.UseSimplifyWeb();
		}
	}
}