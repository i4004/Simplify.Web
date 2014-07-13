using AcspNet.Meta;
using AcspNet.Owin;
using Owin;
using SimpleInjector;
using Simplify.Core;

namespace AcspNet.Examples.SelfHosted
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// Exclude AcspNet from exclude assemblies to be able to load example controllers
			AcspTypesFinder.ExcludedAssembliesPrefixes.Remove("AcspNet");

			// Example of external DI framework for controllers creation

			var container = new Container();
			container.RegisterLifetimeScope<IRequestHandler, RequestHandler>();
			DependencyResolver.Current = new EventDependencyResolver(container.GetInstance, container.BeginLifetimeScope);

			app.UseAcspNet();
		}
	}
}