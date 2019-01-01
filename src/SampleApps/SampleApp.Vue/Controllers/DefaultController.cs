using Simplify.Web;
using Simplify.Web.Attributes;

namespace SampleApp.Vue.Controllers
{
	[Get("/")]
	public class DefaultController : Controller
	{
		public override ControllerResponse Invoke()
		{
			return Tpl("");
		}
	}
}