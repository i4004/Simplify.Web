using AcspNet.Attributes;
using AcspNet.Responses;

namespace AcspNet.Examples.SelfHosted.Controllers.HttpErrors
{
	[Http404]
	public class Http404Controller : Controller
	{
		public override ControllerResponse Invoke()
		{
			return new MessageBoxSt("Http404Message");
		}
	}
}