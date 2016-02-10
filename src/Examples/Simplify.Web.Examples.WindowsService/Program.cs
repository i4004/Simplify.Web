using System.ServiceProcess;
using Simplify.WindowsServices;

namespace Simplify.Web.Examples.WindowsService
{
	class Program
	{
		static void Main()
		{
#if DEBUG
			global::System.Diagnostics.Debugger.Launch();
#endif

			ServiceBase.Run(new BasicServiceHandler<WebApplicationStartup>(true));
		}
	}
}
