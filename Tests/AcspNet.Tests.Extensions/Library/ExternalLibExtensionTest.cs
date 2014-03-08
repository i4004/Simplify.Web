using NUnit.Framework;

namespace AcspNet.Tests.Extensions.Library
{
	public class ExternalLibExtensionTest : LibExtension
	{
		public override void Initialize()
		{
			Assert.IsNotNull(Manager);

			Assert.Throws<AcspNetException>(() => Manager.Get<ExternalLibExtension2Test>());
		}
	}
}