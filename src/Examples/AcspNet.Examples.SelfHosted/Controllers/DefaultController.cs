using AcspNet.Attributes;
using AcspNet.Responses;

namespace AcspNet.Examples.SelfHosted.Controllers
{
	[Get("/")]
	public class DefaultController : Controller
	{
		public override IControllerResponse Invoke()
		{
			return new Tpl(TemplateFactory.Load("Default"));
		}
	}
}