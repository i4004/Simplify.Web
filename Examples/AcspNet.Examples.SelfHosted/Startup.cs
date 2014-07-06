using AcspNet.Owin;
using Owin;

namespace AcspNet.Examples.SelfHosted
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.UseAcspNet();
		}
	}
}