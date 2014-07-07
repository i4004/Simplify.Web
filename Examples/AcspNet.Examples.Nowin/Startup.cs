using AcspNet.Owin;
using Owin;

namespace AcspNet.Examples.Nowin
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.UseAcspNet();
		}
	}
}