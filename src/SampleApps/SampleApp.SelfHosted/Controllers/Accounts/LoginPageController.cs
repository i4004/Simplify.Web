using SampleApp.SelfHosted.Views.Accounts;
using Simplify.Web;
using Simplify.Web.Attributes;
using Simplify.Web.Responses;

namespace SampleApp.SelfHosted.Controllers.Accounts
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