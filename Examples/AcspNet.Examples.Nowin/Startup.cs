using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

namespace AcspNet.Examples.Nowin
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.Use((context, task) =>
			{
				if (context.Request.Path == new PathString("/"))
				{
					context.Response.ContentType = "text/plain";
					return context.Response.WriteAsync("Hello World Nowin!");
				}

				context.Response.StatusCode = 404;
				return Task.Delay(0);
			});
		}
	}
}