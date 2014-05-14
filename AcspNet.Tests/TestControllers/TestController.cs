namespace AcspNet.Tests.TestControllers
{
	[Priority(1)]
	[DefaultPage]
	[HttpGet]
	[ViewModel(typeof(LoginViewModel))]
	public class TestController : Controller
	{
	}

	public class LoginViewModel
	{
	}
}