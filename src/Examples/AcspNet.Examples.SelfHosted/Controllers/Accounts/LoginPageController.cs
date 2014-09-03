using AcspNet.Attributes;
using AcspNet.Examples.SelfHosted.Models.Accounts;
using AcspNet.Responses;

namespace AcspNet.Examples.SelfHosted.Controllers.Accounts
{
	[Get("login")]
	public class LoginPageController : Controller
	{
		public override ControllerResponse Invoke()
		{
			return new ViewModel<LoginViewModel>("Accounts/LoginPage");
		}
	}
}