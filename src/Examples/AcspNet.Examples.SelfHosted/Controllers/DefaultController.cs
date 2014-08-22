using AcspNet.Attributes;
using AcspNet.Responses;

namespace AcspNet.Examples.SelfHosted.Controllers
{
	[Get("/")]
	public class DefaultController : Controller
	{
		public override ControllerResponse Invoke()
		{
			var user = Context.Context.Authentication.User;
			return new Tpl(TemplateFactory.Load("Default"));
		}
	}
}
