using AcspNet.Attributes;
using AcspNet.Responses;

namespace AcspNet.Examples.SelfHosted.Controllers.Accounts
{
	[Get("login")]
	public class LoginController : Controller
	{
		public override IControllerResponse Invoke()
		{
			return new Tpl(TemplateFactory.Load("Accounts/LoginPage"));
		}
	}
}