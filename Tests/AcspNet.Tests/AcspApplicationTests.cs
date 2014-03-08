using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class AcspApplicationTests
	{
		//[Test]
		//public void Setup_NoAttributesInAssembly_AcspNetExceptionThrown()
		//{
		//	var app = new AcspApplication();
		//	Assert.Throws<AcspException>(app.Setup);
		//}

		//[Test]
		//public void Setup_TestExtensions_ExtensionsLoadedCorrectly()
		//{
		//	var app = new AcspApplication();
		//	app.MainAssembly = Assembly.GetAssembly(typeof(TestExecExtension1));

		//	app.Setup();

		//	var execExtensions = app.GetExecExtensionsMetaData();
		//	var libExtensions = app.GetLibExtensionsMetaData();

		//	Assert.AreEqual(2, execExtensions.Count);
		//	Assert.AreEqual(2, libExtensions.Count);

		//	Assert.AreEqual("0.4", execExtensions[0].Version);
		//	Assert.AreEqual(-1, execExtensions[0].Priority);
		//	Assert.AreEqual(RunType.OnAction, execExtensions[0].RunType);
		//	Assert.AreEqual("Foo", execExtensions[0].Action);
		//	Assert.AreEqual("Bar", execExtensions[0].Mode);

		//	Assert.AreEqual("1.5.3", execExtensions[1].Version);
		//	Assert.AreEqual(1, execExtensions[1].Priority);
		//	Assert.AreEqual(RunType.DefaultPage, execExtensions[1].RunType);

		//	Assert.AreEqual("1.0", libExtensions[0].Version);
		//	Assert.AreEqual(-2, libExtensions[0].Priority);

		//	Assert.AreEqual("2.0", libExtensions[1].Version);
		//}

		//[Test]
		//public void CreateProcessor_AcspApplicationNotSetup_AcspNetExceptionThrown()
		//{
		//	var app = new AcspApplication();
		//	Assert.Throws<AcspException>(() => app.CreateProcessor(null));
		//}
	}
}