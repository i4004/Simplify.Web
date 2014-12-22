using AcspNet.Attributes;
using AcspNet.Modules;
using AcspNet.Responses;

namespace AcspNet.Examples.SelfHosted.Controllers.Accounts
{
	[Get("logout")]
	public class LogoutController : Controller
	{
		public override ControllerResponse Invoke()
		{
			Context.Context.Authentication.SignOut();

			return new Redirect();
		}
	}
}