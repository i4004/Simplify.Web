using AcspNet.Meta;
using AcspNet.Owin;
using Owin;

namespace AcspNet.Examples.WindowsService
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// Exclude AcspNet from exclude assemblies to be able to load example controllers
			AcspTypesFinder.ExcludedAssembliesPrefixes.Remove("AcspNet");

			app.UseAcspNet();
		}
	}
}