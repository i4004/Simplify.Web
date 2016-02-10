using System.ComponentModel;
using System.Reflection;

namespace Simplify.Web.Examples.WindowsService
{
	[RunInstaller(true)]
	public class ServiceInstaller : ServiceInstallerBase
	{
		public ServiceInstaller()
			: base(Assembly.GetExecutingAssembly())
		{
		}
	}
}
