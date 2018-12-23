using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Simplify.Web.Examples.WindowsService
{
	public class WebApplicationStartup
	{
		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();

		public void Run()
		{
			CreateWebHostBuilder(Program.Args).Build().Start();
		}
	}
}