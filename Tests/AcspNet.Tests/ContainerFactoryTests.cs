using Moq;
using NUnit.Framework;

namespace AcspNet.Tests
{
	public class TestController : Controller {}

	[TestFixture]
	public class ContainerFactoryTests
	{
		[Test]
		public void CreateController_EmptyContainer_DataFilledCorrectly()
		{
			//var env = new Mock<IEnvironment>();
			var modulesContainer = new Mock<SourceContainer>();
			modulesContainer.SetupProperty(x => x.FileReader, new Mock<IFileReader>().Object);

			var factory = new ContainerFactory(modulesContainer.Object);
			//var controller = new TestController();

			var controller = factory.CreateContainer(typeof(TestController));
			Assert.IsNotNull(controller.FileReader);
		}
	}
}