using System.Web;
using AcspNet.Meta;
using SimpleInjector;

namespace AcspNet.Examples
{
	public class Global : HttpApplication
	{
		protected void Application_Start()
		{
			//ControllersMetaStore.ExcludedAssembliesPrefixes.Remove("AcspNet");

			//var container = new Container();
			//container.RegisterPerWebRequest<ITestService, TestService>();
			//DependencyResolver.Current = new EventDependencyResolver(container.GetInstance);
		}
	}
}
