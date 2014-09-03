using AcspNet.Examples.SelfHosted.Controllers;
using AcspNet.Responses;
using Moq;
using NUnit.Framework;
using Simplify.Templates;

namespace AcspNet.Examples.SelfHosted.Tests.Controllers
{
	[TestFixture]
	public class DefaultPageControllerTests
    {
		[Test]
		public void Invoke_Default_MainContentSet()
		{
			// Assign

			var c = new Mock<DefaultController> {CallBase = true};
			c.Setup(x => x.TemplateFactory.Load(It.IsAny<string>())).Returns(Template.FromString("Hello World!!!"));

			// Act
			var result = c.Object.Invoke();

			// Assert
			Assert.AreEqual("Hello World!!!", ((Tpl)result).Data);
		}
    }
}
