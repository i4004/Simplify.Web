using AcspNet.Attributes;
using AcspNet.Responses;

namespace AcspNet.Examples.Nowin.Controllers
{
	[Get("/")]
	public class DefaultPageController : Controller
	{
		private readonly Foo _foo;

		public DefaultPageController(Foo foo)
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