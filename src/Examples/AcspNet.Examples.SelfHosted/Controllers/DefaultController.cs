using AcspNet.Attributes;
using AcspNet.Responses;

namespace AcspNet.Examples.SelfHosted.Controllers
{
	[Get("/")]
	public class DefaultController : Controller
	{
		public override ControllerResponse Invoke()
		{
			return new StaticTpl("Default");
		}
	}
}
