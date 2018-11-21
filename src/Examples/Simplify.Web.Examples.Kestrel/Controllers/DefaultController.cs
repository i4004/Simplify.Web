using Simplify.Web.Attributes;

namespace Simplify.Web.Examples.Kestrel.Controllers
{
	[Get("/")]
	public class DefaultController : Controller
	{
		public override ControllerResponse Invoke()
		{
			return Tpl("Hello world!!!");
		}
	}
}