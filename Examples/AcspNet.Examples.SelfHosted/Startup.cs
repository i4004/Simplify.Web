using AcspNet.Meta;
using AcspNet.Owin;
using DryIoc;
using Owin;

namespace AcspNet.Examples.SelfHosted
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// Exclude AcspNet from exclude assemblies to be able to load example controllers
			AcspTypesFinder.ExcludedAssembliesPrefixes.Remove("AcspNet");

			DependencyResolver.Container.Register<Foo>();

			app.UseAcspNet();
		}
	}
}