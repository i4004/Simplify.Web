namespace AcspNet.Examples.Controllers
{
	[Get("/")]
	public class MainPageController : Controller
	{
		public override IControllerResponse Invoke()
		{
			return new Tpl("Hello world!!!");
		}
	}
}