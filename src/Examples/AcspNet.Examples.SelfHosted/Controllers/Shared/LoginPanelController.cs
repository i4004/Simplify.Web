using System.Threading.Tasks;
using AcspNet.Attributes;
using AcspNet.Examples.SelfHosted.Views;
using AcspNet.Responses;

namespace AcspNet.Examples.SelfHosted.Controllers.Shared
{
	[Priority(-1)]
	public class LoginPanelController : AsyncController
	{
		public override async Task<ControllerResponse> Invoke()
		{
			return Context.Context.Authentication.User == null
				? new InlineTpl("LoginPanel", "")
				: new InlineTpl("LoginPanel", await GetView<LoginPanelView>().Get(Context.Context.Authentication.User.Identity.Name));
		}
	}
}