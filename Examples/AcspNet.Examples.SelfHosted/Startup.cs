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
			var container = new Container();
			container.RegisterLifetimeScope<IRequestHandler, RequestHandler>();
			DependencyResolver.Current = new EventDependencyResolver(container.GetInstance, container.BeginLifetimeScope);

			app.UseAcspNet();
		}
	}
}