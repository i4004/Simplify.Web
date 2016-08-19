using System.Diagnostics;
using Simplify.WindowsServices;

namespace Simplify.Web.Examples.WindowsService
{
	internal class Program
	{
		private static void Main(string[] args)
		{
#if DEBUG
			Debugger.Launch();
#endif

			new BasicServiceHandler<WebApplicationStartup>(true).Start(args);
		}
	}
}