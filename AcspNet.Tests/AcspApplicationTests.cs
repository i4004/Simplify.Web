using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class AcspApplicationTests
	{
		[Test]
		[ExpectedException(typeof(AcspNetException))]
		public void Constructor_Init_LoadExtensionsFromAssemblyOfNotFound()
		{
			AcspApplication.Setup();
		}

		//[Test]
		//public void Constructor_Init_LoadExtensionsFromAssemblyOfNotFound()
		//{
		//	var execExtensions = AcspApplication.Current.GetExecExtensionsMetaData();
		//	var libExtensions = AcspApplication.Current.GetLibExtensionsMetaData();

		//	Assert.AreEqual(0, execExtensions.Count);
		//	Assert.AreEqual(0, libExtensions.Count);

		//	AcspApplication.Setup();

		//	execExtensions = AcspApplication.Current.GetExecExtensionsMetaData();
		//	libExtensions = AcspApplication.Current.GetLibExtensionsMetaData();

		//	Assert.AreEqual(0, execExtensions.Count);
		//	Assert.AreEqual(0, libExtensions.Count);
		//}

		//[Test]
		//public void Constructor_Init_ExtensionsLoadedCorrectly()
		//{
		//	var execExtensions = AcspApplication.Current.GetExecExtensionsMetaData();
		//	var libExtensions = AcspApplication.Current.GetLibExtensionsMetaData();

		//	Assert.AreEqual(0, execExtensions.Count);
		//	Assert.AreEqual(0, libExtensions.Count);

		//	AcspApplication.Setup();

		//	execExtensions = AcspApplication.Current.GetExecExtensionsMetaData();
		//	libExtensions = AcspApplication.Current.GetLibExtensionsMetaData();

		//	Assert.AreEqual(0, execExtensions.Count);
		//	Assert.AreEqual(0, libExtensions.Count);
		//}

		//[Test]
		//public void Setup_Init_ExternalsCreated()
		//{
		//	Assert.IsNull(AcspApplication.MainAssembly);

		//	AcspApplication.Setup();

		//	Assert.IsNotNull(AcspApplication.MainAssembly);
		//}
	}
}