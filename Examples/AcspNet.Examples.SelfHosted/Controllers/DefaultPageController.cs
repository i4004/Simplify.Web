namespace AcspNet.Examples.SelfHosted.Controllers
{
	[Get("/")]
	public class MainPageController : Controller
	{
		private readonly Foo _foo;

		public MainPageController(Foo foo)
		{
			_foo = foo;
		}

		public override IControllerResponse Invoke()
		{
			return new Tpl(_foo.Bar());
		}
	}
}