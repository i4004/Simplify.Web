using AcspNet.Attributes;

namespace AcspNet.Tests.TestEntities
{
	[Get("/testaction")]
	[Post("/testaction1")]
	[Put("/testaction2")]
	[Delete("/testaction3")]
	[Http400]
	[Http403]
	[Http404]
	[Priority(1)]
	public class TestController1 : Controller
	{
		public override ControllerResponse Invoke()
		{
			throw new System.NotImplementedException();
		}
	}
}