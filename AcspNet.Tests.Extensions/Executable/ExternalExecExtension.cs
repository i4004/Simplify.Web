using NUnit.Framework;

namespace AcspNet.Tests.Extensions.Executable
{
	[Action("foo")]
	public class ExternalExecExtension : ExecExtension
	{
		public override void Invoke()
		{
			Assert.AreEqual("foo", Manager.CurrentAction);
			Assert.AreEqual("", Manager.CurrentMode);
			Assert.AreEqual("", Manager.CurrentID);

			Assert.AreEqual("?act=foo", Manager.GetActionModeUrl());
		}
	}
}