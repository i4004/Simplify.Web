namespace AcspNet.Tests.TestEntities
{
	[Get("/testaction")]
	[Post("/testaction1")]
	[Put("/testaction2")]
	[Delete("/testaction3")]
	[Priority(1)]
	public class TestController1 : Controller
	{
	}
}