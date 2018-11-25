using Moq;
using NUnit.Framework;
using Simplify.Templates;
using Simplify.Web.Examples.SelfHosted.Controllers.Shared;
using Simplify.Web.Responses;

namespace Simplify.Web.Examples.SelfHosted.Tests.Controllers.Shared
{
	[TestFixture]
	public class NavbarControllerTests
	{
		[Test]
		public void Invoke_InlineTplNavbarDataDataReturned()
		{
			// Assign

			var c = new Mock<NavbarController> { CallBase = true };

			c.Setup(x => x.TemplateFactory.Load(It.Is<string>(name => name == "Navbar"))).Returns(Template.FromString("Inline Data"));

			// Act
			var result = c.Object.Invoke() as InlineTpl;

			// Assert

			Assert.IsNotNull(result);
			Assert.AreEqual("Navbar", result.DataCollectorVariableName);
			Assert.AreEqual("Inline Data", result.Data);
		}
	}
}