using AcspNet.Meta;
using AcspNet.Modules.Controllers;
using AcspNet.Tests.TestControllers;
using NUnit.Framework;

namespace AcspNet.Tests.Meta
{
	[TestFixture]
	[IgnoreControllers(typeof(TestIgnoredController), typeof(MessagePageDisplay))]
	public class ControllersMetaStoreTests
	{
		[Test]
		public void ControllersMetaStore_Create_ControllersMetaDataLoadedCorrectly()
		{
			// Arrange

			ControllersMetaStore.ExcludedAssembliesPrefixes.Remove("AcspNet");
			ControllersMetaStore.ExcludedAssembliesPrefixes.Add("nunit");
			ControllersMetaStore.ExcludedAssembliesPrefixes.Add("JetBrains");
			ControllersMetaStore.ExcludedAssembliesPrefixes.Add("DynamicProxy");
			ControllersMetaStore.ExcludedAssembliesPrefixes.Add("Moq");
			ControllersMetaStore.ExcludedAssembliesPrefixes.Add("Anonymously");			

			// Act

			var metaStore = new ControllersMetaStore();
			var metaData = metaStore.GetControllersMetaData();

			// Assert

			Assert.AreEqual(3, metaData.Count);

			Assert.AreEqual("TestController2", metaData[0].ControllerType.Name);
			Assert.AreEqual(-1, metaData[0].ExecParameters.RunPriority);
			Assert.AreEqual("Foo", metaData[0].ExecParameters.Action);
			Assert.AreEqual("Bar", metaData[0].ExecParameters.Mode);
			Assert.IsFalse(metaData[0].ExecParameters.IsDefaultPageController);
			Assert.IsFalse(metaData[0].ExecParameters.IsAjax);
			Assert.IsTrue(metaData[0].Security.IsHttpPost);
			Assert.IsFalse(metaData[0].Security.IsAuthenticationRequired);
			Assert.IsNull(metaData[0].Role);
			Assert.IsNull(metaData[0].Data);

			Assert.AreEqual("TestController3", metaData[1].ControllerType.Name);
			Assert.AreEqual(0, metaData[1].ExecParameters.RunPriority);
			Assert.IsTrue(metaData[1].ExecParameters.IsAjax);
			Assert.IsTrue(metaData[1].Role.Is400Handler);
			Assert.IsTrue(metaData[1].Role.Is403Handler);
			Assert.IsTrue(metaData[1].Role.Is404Handler);
			Assert.IsTrue(metaData[1].Security.IsAuthenticationRequired);
			Assert.IsNull(metaData[1].Data);

			Assert.AreEqual("TestController", metaData[2].ControllerType.Name);
			Assert.AreEqual(1, metaData[2].ExecParameters.RunPriority);
			Assert.AreEqual(null, metaData[2].ExecParameters.Action);
			Assert.AreEqual(null, metaData[2].ExecParameters.Mode);
			Assert.IsTrue(metaData[2].ExecParameters.IsDefaultPageController);
			Assert.IsFalse(metaData[2].ExecParameters.IsAjax);
			Assert.IsTrue(metaData[2].Security.IsHttpGet);
			Assert.IsNull(metaData[2].Role);
			Assert.AreEqual("LoginViewModel", metaData[2].Data.ViewModel.Name);
		}
	}
}