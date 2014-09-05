using AcspNet.Examples.SelfHosted.Controllers.Accounts;
using AcspNet.Modules;
using AcspNet.Responses;
using Moq;
using NUnit.Framework;

namespace AcspNet.Examples.SelfHosted.Tests.Controllers.Accounts
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