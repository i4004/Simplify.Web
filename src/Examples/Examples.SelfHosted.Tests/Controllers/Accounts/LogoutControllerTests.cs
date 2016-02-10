using Simplify.Web.Examples.SelfHosted.Controllers.Accounts;
using Simplify.Web.Modules;
using Simplify.Web.Responses;

namespace Simplify.Web.Examples.SelfHosted.Tests.Controllers.Accounts
{
	[TestFixture]
	public class LogoutControllerTests
	{
		[Test]
		public void Invoke_SignOutCalledRedirectedToDefaultPage()
		{
			// Assign

			var c = new Mock<LogoutController> {CallBase = true};
			var context = new Mock<IAcspNetContext>();
			context.Setup(x => x.Context.Authentication.SignOut());
			c.SetupGet(x => x.Context).Returns(context.Object);

			// Act
			var result = c.Object.Invoke();

			// Assert

			Assert.AreEqual(RedirectionType.DefaultPage, ((Redirect)result).RedirectionType);
			context.Verify(x => x.Context.Authentication.SignOut());
		}
	}
}