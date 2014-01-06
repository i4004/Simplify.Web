using NUnit.Framework;

namespace AcspNet.Tests.Extensions.Executable
{
	[Action("getExtensionsMetadataTest")]
	public class GetExtensionsMetadataTest : ExecExtension
	{
		public override void Invoke()
		{
			var execItems = AcspNet.Manager.GetExecExtensionsMetaData();
			Assert.AreEqual(16, execItems.Count);

			var libsitems = AcspNet.Manager.GetLibExtensionsMetaData();
			Assert.AreEqual(4, libsitems.Count);
		}
	}
}