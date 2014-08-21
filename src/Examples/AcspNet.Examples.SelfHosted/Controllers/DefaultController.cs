using System.Threading.Tasks;
using AcspNet.Attributes;
using AcspNet.Responses;

namespace AcspNet.Examples.SelfHosted.Controllers
{
	[Get("/")]
	public class DefaultController : AsyncController
	{
		public override Task<ControllerResponse> Invoke()
		{
			return Task.FromResult((ControllerResponse)new Tpl(TemplateFactory.Load("Default")));
		}
	}
}
