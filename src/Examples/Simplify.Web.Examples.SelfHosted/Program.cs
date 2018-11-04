using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Simplify.Web.Examples.SelfHosted
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}