using AcspNet.Attributes;

namespace AcspNet.Examples.SelfHosted.Controllers
{
	[Get("/foo/{name}")]
	public class TestController : Controller
	{
		public override IControllerResponse Invoke()
		{
			var a = RouteParameters.name;
			return base.Invoke();
		}
	}
}