using Simplify.Web.Responses;

namespace Simplify.Web.Examples.WindowsService.Controllers
{
	public class DefaultController : Controller
	{
		public override ControllerResponse Invoke()
		{
			return new MessageBox("Hello from OWIN self-hosted windows service application!");
		}
	}
}