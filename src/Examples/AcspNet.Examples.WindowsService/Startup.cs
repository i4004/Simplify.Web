using Simplify.Web.Meta;

namespace Simplify.Web.Examples.WindowsService
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// Exclude AcspNet from exclude assemblies to be able to load example controllers
			AcspTypesFinder.ExcludedAssembliesPrefixes.Remove("AcspNet");

			DIContainer.Current = new SimpleInjectorDIProvider();

			app.UseAcspNet();
		}
	}
}