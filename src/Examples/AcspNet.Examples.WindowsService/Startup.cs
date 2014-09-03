using AcspNet.Meta;
using AcspNet.Owin;
using Owin;
using Simplify.DI;
using Simplify.DI.Provider.SimpleInjector;

namespace AcspNet.Examples.WindowsService
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