using System.Diagnostics;
using System.ServiceProcess;
using Simplify.WindowsServices;

namespace Simplify.Web.Examples.WindowsService
{
	internal class Program
	{
		private static void Main()
		{
#if DEBUG
			Debugger.Launch();
#endif

			ServiceBase.Run(new BasicServiceHandler<WebApplicationStartup>(true));
		}
	}
}