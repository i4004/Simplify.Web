using NUnit.Framework;

namespace AcspNet.Tests.TestExtensions.Extensions.Library
{
	public class NotInitializedExtensionCallTest : LibExtension
	{
		public override void Initialize()
		{
			Assert.Throws<AcspException>(() => Get<HighPriortyLibExtension>());
		}
	}
}