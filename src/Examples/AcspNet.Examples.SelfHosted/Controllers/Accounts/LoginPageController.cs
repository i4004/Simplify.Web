using AcspNet.Attributes;
using AcspNet.Responses;

namespace AcspNet.Examples.SelfHosted.Controllers.Accounts
{
	[Get("login")]
	public class LoginPageController : Controller
	{
		public override ControllerResponse Invoke()
		{
			return new Tpl(TemplateFactory.Load("Accounts/LoginPage"));
		}
	}
}