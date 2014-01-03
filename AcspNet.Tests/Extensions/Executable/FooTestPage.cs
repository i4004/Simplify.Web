using NUnit.Framework;

namespace AcspNet.Tests.Extensions.Executable
{
	[Action("foo")]
	[Mode("bar")]
	[Priority(1)]
	[Version("1.0")]
	public class FooTestPage : ExecExtension
	{
		public override void Invoke()
		{
			Assert.IsNotNull(Manager);
			Assert.AreEqual("foo", Manager.CurrentAction);
			Assert.AreEqual("bar", Manager.CurrentMode);
			Assert.AreEqual("15", Manager.CurrentID);

			var execItems = Manager.GetExecExtensionsMetaData();
			Assert.AreEqual(11, execItems.Count);

			var libsitems = Manager.GetLibExtensionsMetaData();
			Assert.AreEqual(3, libsitems.Count);

			Assert.AreEqual("?act=foo&amp;mode=bar&amp;id=15",  Manager.GetActionModeUrl());
		}
	}
}