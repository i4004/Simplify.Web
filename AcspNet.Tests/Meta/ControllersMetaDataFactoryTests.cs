using AcspNet.Meta;
using AcspNet.Tests.TestEntities;
using NUnit.Framework;

namespace AcspNet.Tests.Meta
{
	[TestFixture]
	public class ControllersMetaDataFactoryTests
	{
		[Test]
		public void CreateControllerMetaData_TestController_PropertiesSetCorrectly()
		{
			// Arrange
			var factory = new ControllerMetaDataFactory();

			// Act
			var metaData = factory.CreateControllerMetaData(typeof (TestController1));

			// Assert

			Assert.AreEqual("TestController1", metaData.ControllerType.Name);
			Assert.AreEqual("/testaction", metaData.ExecParameters.RouteInfo.GetRoute);
			Assert.AreEqual("/testaction1", metaData.ExecParameters.RouteInfo.PostRoute);
			Assert.AreEqual("/testaction2", metaData.ExecParameters.RouteInfo.PutRoute);
			Assert.AreEqual("/testaction3", metaData.ExecParameters.RouteInfo.DeleteRoute);
			Assert.AreEqual(1, metaData.ExecParameters.RunPriority);
		}
	}
}