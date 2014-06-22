using AcspNet.Examples.Views;

namespace AcspNet.Examples.Controllers
{
	[Action("about")]
	//[HttpGet]
	public class AboutPageController : Controller
	{
		public override void Invoke()
		{
			DataCollector.Add(GetView<AboutPageView>().Get());
			DataCollector.AddTitleSt("PageTitleAbout");
		}
	}
}
