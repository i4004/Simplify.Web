using AcspNet.DI;
using AcspNet.DIContainerProvider.SimpleInjector;
using AcspNet.Meta;
using AcspNet.Owin;
using Owin;

namespace AcspNet.Examples.Nowin
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// Exclude AcspNet from exclude assemblies to be able to load example controllers
			AcspTypesFinder.ExcludedAssembliesPrefixes.Remove("AcspNet");
			
			DIContainer.Current = new SimpleInjectorDIProvider();
			DIContainer.Current.Register<Foo>();

			app.UseAcspNet();
		}
	}
}