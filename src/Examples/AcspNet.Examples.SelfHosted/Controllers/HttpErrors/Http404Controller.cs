using AcspNet.Attributes;
using AcspNet.Responses;

namespace AcspNet.Examples.SelfHosted.Controllers.HttpErrors
{
	[Http404]
	public class Http404Controller : Controller
	{
		public override IControllerResponse Invoke()
		{
			return new Tpl(TemplateFactory.Load("HttpErrors/Http404"));
		}
	}
}