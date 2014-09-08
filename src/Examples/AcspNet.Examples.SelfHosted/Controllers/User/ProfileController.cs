using AcspNet.Attributes;
using AcspNet.Responses;

namespace AcspNet.Examples.SelfHosted.Controllers.User
{
	[Authorize]
	[Get("profile")]
	public class ProfileController : Controller
	{
		public override ControllerResponse Invoke()
		{
			return new StaticTpl("User/Profile", StringTable.PageTitleProfile);
		}
	}
}