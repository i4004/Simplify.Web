using AcspNet.Attributes;
using AcspNet.Examples.SelfHosted.Views;
using AcspNet.Responses;

namespace AcspNet.Examples.SelfHosted.Controllers
{
	[Get("/")]
	public class DefaultPageController : Controller
	{
		public override IControllerResponse Invoke()
		{
			var view = GetView<DefaultPageView>();
			var tpl = TemplateFactory.Load("Index");
			tpl.Set("MainContent", view.Get());

			return new Tpl(tpl);
		}
	}
}