using AcspNet.Tests.TestExtensions.Extensions.Library;
using NUnit.Framework;

namespace AcspNet.Tests.TestExtensions.Extensions.Executable
{
	public class LibExtensionCallTest : ExecExtension
	{
		public override void Invoke()
		{
			var extensions = Get<LibExtensionWithMethod>();

			Assert.AreEqual(256, extensions.GetNumber());
		}
	}
}