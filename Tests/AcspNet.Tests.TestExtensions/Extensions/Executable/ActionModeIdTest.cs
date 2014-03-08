using NUnit.Framework;

namespace AcspNet.Tests.TestExtensions.Extensions.Executable
{
	[Action("ActionModeIdTest")]
	[Mode("ActionModeIdTestMode")]
	[Priority(1)]
	[Version("1.0")]
	public class ActionModeIdTest : ExecExtension
	{
		public override void Invoke()
		{
			Assert.AreEqual("ActionModeIdTest", Context.CurrentAction);
			Assert.AreEqual("ActionModeIdTestMode", Context.CurrentMode);
			Assert.AreEqual("15", Context.CurrentID);
		}
	}
}