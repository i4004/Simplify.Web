using NUnit.Framework;

namespace AcspNet.Tests.TestExtensions.Extensions.Executable
{
	[RunType(RunType.DefaultPage)]
	public class DefaultPageTest : ExecExtension
	{
		public override void Invoke()
		{
			Assert.AreEqual("", Context.CurrentAction);
			Assert.AreEqual("", Context.CurrentMode);
			Assert.AreEqual("", Context.CurrentID);

			Assert.AreEqual("", Context.GetActionModeUrl());
		}
	}
}