using AcspNet.Owin;
using Owin;
using SimpleInjector;
using Simplify.Core;

namespace AcspNet.Examples
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			DependencyResolver.Current = new EventDependencyResolver(new Container().GetInstance);
			app.UseAcspNet();
		}
	}
}