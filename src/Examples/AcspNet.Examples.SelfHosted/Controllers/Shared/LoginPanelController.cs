using System.Threading.Tasks;
using AcspNet.Attributes;
using AcspNet.Examples.SelfHosted.Views.Shared;
using AcspNet.Responses;

namespace AcspNet.Examples.SelfHosted.Controllers.Shared
{
	[Priority(-1)]
	public class LoginPanelController : AsyncController
	{
		public override async Task<ControllerResponse> Invoke()
		{
			return Context.Context.Authentication.User == null
				? new InlineTpl("LoginPanel", await TemplateFactory.LoadAsync("Shared/LoginPanel/GuestPanel"))
				: new InlineTpl("LoginPanel", await GetView<LoggedUserPanelView>().Get(Context.Context.Authentication.User.Identity.Name));
		}
	}
}