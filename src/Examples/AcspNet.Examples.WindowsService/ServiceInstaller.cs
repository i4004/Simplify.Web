using System.ComponentModel;
using System.Reflection;
using Simplify.WindowsServices;

namespace AcspNet.Examples.WindowsService
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
