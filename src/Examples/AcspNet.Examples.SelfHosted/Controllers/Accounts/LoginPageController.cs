using AcspNet.Attributes;
using AcspNet.Examples.SelfHosted.Views.Accounts;
using AcspNet.Responses;

namespace AcspNet.Examples.SelfHosted.Controllers.Accounts
{
	[Get("login")]
	public class LoginPageController : Controller
	{
		public override ControllerResponse Invoke()
		{
			return new Tpl(GetView<LoginView>().Get(), StringTable.PageTitleLogin);
		}
	}
}