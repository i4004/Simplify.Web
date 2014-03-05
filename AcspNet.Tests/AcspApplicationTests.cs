using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class AcspApplicationTests
	{
		[Test]
		public void Setup_ExtensionsLoading_LoadedCorrectly()
		{
			var app = new AcspApplication();
			var execExtensions = app.GetExecExtensionsMetaData();
			var libExtensions = app.GetLibExtensionsMetaData();

			Assert.AreEqual(0, execExtensions.Count);
			Assert.AreEqual(0, libExtensions.Count);
		}
	}
}