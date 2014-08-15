using System.ServiceProcess;
using Simplify.AutomatedWindowsServices;
using Simplify.DI;

namespace AcspNet.Examples.WindowsService
{
	class Program
	{
		static void Main()
		{
#if DEBUG
			System.Diagnostics.Debugger.Launch();
#endif
			DIContainer.Current.Register<WebApplicationStartup>();

			ServiceBase.Run(new BasicServiceHandler<WebApplicationStartup>());
		}
	}
}
