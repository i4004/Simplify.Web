using AcspNet.Tests.Extensions.Library;

using NUnit.Framework;

namespace AcspNet.Tests.Extensions.Executable
{
	public class LibraryExtensionCallTest : ExecExtension
	{
		public override void Invoke()
		{
			var testLibExt = Manager.Get<LibExtensionTest>();

			Assert.IsNotNull(testLibExt);
			Assert.AreEqual(256, testLibExt.GetNumber());
		}		 
	}
}