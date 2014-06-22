namespace AcspNet.Examples.Controllers
{
	[DefaultPage]
	public class MainPageController : Controller
	{
		private readonly ITestService _service;

		public MainPageController(ITestService service)
		{
			_service = service;
		}

		public override void Invoke()
		{
			DataCollector.Add(TemplateFactory.Load("MainPage.tpl").Get());
		}
	}
}