using Owin;
using Simplify.DI;
using Simplify.DI.Provider.SimpleInjector;
using Simplify.Web.Meta;
using Simplify.Web.Owin;

namespace Simplify.Web.Examples.WindowsService
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// Exclude AcspNet from exclude assemblies to be able to load example controllers
			SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Remove("AcspNet");

			DIContainer.Current = new SimpleInjectorDIProvider();

			app.UseAcspNet();
		}
	}
}