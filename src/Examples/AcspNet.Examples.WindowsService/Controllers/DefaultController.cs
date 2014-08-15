using AcspNet.Responses;

namespace AcspNet.Examples.WindowsService.Controllers
{
	public class DefaultController : Controller
	{
		public override IControllerResponse Invoke()
		{
			return new MessageBox("Hello from OWIN self-hosted windows service application!");
		}
	}
}