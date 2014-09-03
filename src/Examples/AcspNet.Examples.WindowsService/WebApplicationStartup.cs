using Microsoft.Owin.Hosting;

namespace AcspNet.Examples.WindowsService
{
	public class WebApplicationStartup
	{
		public void Run()
		{
			WebApp.Start<Startup>("http://localhost:8080");
		}
	}
}