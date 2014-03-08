using NUnit.Framework;

namespace AcspNet.Tests.TestExtensions.Extensions.Executable
{
	[Action("ActionIdTest")]
	[Priority(1)]
	[Version("1.0")]
	public class ActionIdTest : ExecExtension
	{
		public override void Invoke()
		{
			Assert.AreEqual("ActionIdTest", Context.CurrentAction);
			Assert.AreEqual("2", Context.CurrentID);
		}
	}
}