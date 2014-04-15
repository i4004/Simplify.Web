using Moq;
using NUnit.Framework;

namespace AcspNet.Tests
{
	public class TestController : Controller {}

	[TestFixture]
	public class ContainerBuilderTests
	{
		[Test]
		public void FillContainer_EmptyContainer_DataFilledCorrectly()
		{
			var env = new Mock<IEnvironment>();

			var builder = new ContainerBuilder(env.Object);
			var controller = new TestController();

			builder.FillContainer(controller);
			Assert.IsNotNull(controller.FileReader);
		}
	}
}