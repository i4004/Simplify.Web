using Simplify.Web;
using Simplify.Web.Attributes;

namespace SampleApp.Kestrel.Controllers
{
	[Get("/")]
	public class DefaultController : Controller
	{
		public override ControllerResponse Invoke()
		{
			return Tpl("Hello from Simplify.Web Kestrel Application!");
		}
	}
}