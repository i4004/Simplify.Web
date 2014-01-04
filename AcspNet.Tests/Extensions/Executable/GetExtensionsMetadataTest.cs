using NUnit.Framework;

namespace AcspNet.Tests.Extensions.Executable
{
	[Action("getExtensionsMetadataTest")]
	public class GetExtensionsMetadataTest : ExecExtension
	{
		public override void Invoke()
		{
			var execItems = Manager.GetExecExtensionsMetaData();
			Assert.AreEqual(16, execItems.Count);

			var libsitems = Manager.GetLibExtensionsMetaData();
			Assert.AreEqual(4, libsitems.Count);
		}
	}
}