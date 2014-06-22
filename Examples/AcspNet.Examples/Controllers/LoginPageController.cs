using AcspNet.Examples.Views;

namespace AcspNet.Examples.Controllers
{
	[Action("login")]
	public class LoginPageController : Controller
	{
		public override void Invoke()
		{
			DataCollector.Add(GetView<LoginPageView>().Get());
		}
	}
}