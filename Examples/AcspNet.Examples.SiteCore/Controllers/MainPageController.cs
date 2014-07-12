namespace AcspNet.Examples.SiteCore.Controllers
{
	[Route("/")]
	public class MainPageController : Controller
	{
		//private readonly ITestService _service;

		public MainPageController(/*ITestService service*/)
		{
			//_service = service;
		}

		public override void Invoke()
		{
			//DataCollector.Add(TemplateFactory.Load("MainPage.tpl").Get());
		}
	}
}