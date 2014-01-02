using AcspNet.Tests.Extensions.Library;

using NUnit.Framework;

namespace AcspNet.Tests.Extensions.Executable
{
	[Action("")]
	[Mode("")]
	[Priority(-1)]
	[Version("1.0")]
	public class AllPagesControl : ExecExtension
	{
		public override void Invoke()
		{
			Assert.IsNotNull(Manager);

			var testLibExt = Manager.Get<TestLibExtension>();

			Assert.IsNotNull(testLibExt);
			Assert.AreEqual(256, testLibExt.GetNumber());
		}
	}
}