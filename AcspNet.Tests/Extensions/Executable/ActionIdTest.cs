using NUnit.Framework;

namespace AcspNet.Tests.Extensions.Executable
{
	[Action("foo2")]
	[Priority(1)]
	[Version("1.0")]
	public class FooTestPage2 : ExecExtension
	{
		public override void Invoke()
		{
			Assert.AreEqual("foo2", Manager.CurrentAction);
			Assert.AreEqual("2", Manager.CurrentID);

			Assert.AreEqual("?act=foo2&amp;id=2",  Manager.GetActionModeUrl());
		}
	}
}