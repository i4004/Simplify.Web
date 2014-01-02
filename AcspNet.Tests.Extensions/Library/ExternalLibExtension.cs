using NUnit.Framework;

namespace AcspNet.Tests.Extensions.Library
{
	public class ExternalLibExtension : LibExtension
	{
		public override void Initialize()
		{
			Assert.IsNotNull(Manager);

			Assert.Throws<AcspNetException>(() => Manager.Get<ExternalLibExtension2>());
		}
	}
}