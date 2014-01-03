using NUnit.Framework;

namespace AcspNet.Tests.Extensions.Executable
{
	[Action("foo")]
	[Mode("bar")]
	[Priority(1)]
	[Version("1.0")]
	public class ActionModeIdTest : ExecExtension
	{
		public override void Invoke()
		{
			Assert.AreEqual("foo", Manager.CurrentAction);
			Assert.AreEqual("bar", Manager.CurrentMode);
			Assert.AreEqual("15", Manager.CurrentID);

			Assert.AreEqual("?act=foo&amp;mode=bar&amp;id=15",  Manager.GetActionModeUrl());
		}
	}
}