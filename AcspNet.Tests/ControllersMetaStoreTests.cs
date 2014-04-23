using AcspNet.Tests.TestControllers;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	[LoadIndividualControllers(typeof(TestController))]
	[LoadControllersFromAssemblyOf(typeof(TestController))]
	public class ControllersMetaStoreTests
	{
		[Test]
		public void ControllersMetaStore_Create_ControllersMetaDataLoadedCorrectly()
		{
			// Act

			ControllersMetaStore.Current = new ControllersMetaStore();

			var metaData = ControllersMetaStore.Current.GetControllersMetaData();

			// Assert

			Assert.AreEqual(3, metaData.Count);

			Assert.AreEqual("TestController2", metaData[0].ControllerType.Name);
			Assert.AreEqual(-1, metaData[0].RunPriority);
			Assert.AreEqual("Foo", metaData[0].Action);
			Assert.AreEqual("Bar", metaData[0].Mode);
			Assert.IsFalse(metaData[0].IsDefaultPageController);
			Assert.IsFalse(metaData[0].IsAjaxRequest);

			Assert.AreEqual("TestController3", metaData[1].ControllerType.Name);
			Assert.AreEqual(0, metaData[1].RunPriority);
			Assert.IsTrue(metaData[1].IsAjaxRequest);

			Assert.AreEqual("TestController", metaData[2].ControllerType.Name);
			Assert.AreEqual(1, metaData[2].RunPriority);
			Assert.AreEqual(null, metaData[2].Action);
			Assert.AreEqual(null, metaData[2].Mode);
			Assert.IsTrue(metaData[2].IsDefaultPageController);
			Assert.IsFalse(metaData[2].IsAjaxRequest);
		}
	}
}