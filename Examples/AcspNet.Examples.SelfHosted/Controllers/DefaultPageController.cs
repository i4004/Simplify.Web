using AcspNet.Attributes;
using AcspNet.Responses;

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
			var tpl = TemplateFactory.Load("Index");
			tpl.Set("MainContent", _foo.Bar());

			return new Tpl(tpl);
		}
	}
}