using AcspNet.Attributes;
using AcspNet.Responses;

namespace AcspNet.Examples.SelfHosted.Controllers.Static
{
	[Get("about")]
	[Authorize]
	public class AboutController : Controller
	{
		public override ControllerResponse Invoke()
		{
			return new Tpl(TemplateFactory.Load("Static/About"), StringTable.PageTitleAbout);
		}
	}
}