using System;
using Simplify.Web;
using Simplify.Web.Attributes;

namespace SampleApp.SelfHosted.Controllers
{
	[Get("exception")]
	public class ExceptionExampleController : Controller
	{
		public override ControllerResponse Invoke()
		{
			throw new NotImplementedException();
		}
	}
}