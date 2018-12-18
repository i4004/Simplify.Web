using Simplify.DI;
using Simplify.Web.Bootstrapper;
using Simplify.Web.Meta;

namespace Simplify.Web.Examples.WindowsService.Setup
{
	public class IocRegistrations
	{
		public static void Register()
		{
			// Exclude Simplify.Web from exclude assemblies to be able to load example controllers
			SimplifyWebTypesFinder.ExcludedAssembliesPrefixes.Remove("Simplify");

			DIContainer.Current.Register<WebApplicationStartup>();

			// Manual Simplify.Web bootstrapper registration
			BootstrapperFactory.CreateBootstrapper().Register();
		}
	}
}