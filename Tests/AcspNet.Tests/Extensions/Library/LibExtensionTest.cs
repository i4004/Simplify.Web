using NUnit.Framework;

namespace AcspNet.Tests.Extensions.Library
{
	[Priority(-1)]
	[Version("1.0")]
	public class LibExtensionTest : LibExtension
	{
		public override void Initialize()
		{
			Assert.IsNotNull(Manager);
			Assert.IsNotNull(DataCollector);
			Assert.IsNotNull(ExtensionsDataLoader);
			Assert.IsNotNull(Environment);
			Assert.IsNotNull(StringTable);
			Assert.IsNotNull(TemplateFactory);
			Assert.IsNotNull(Html);
			Assert.IsNotNull(Html.ListsGenerator);

			Assert.Throws<AcspNetException>(() => Manager.Get<HightPriortyLibExtensionTest>());
		}

		public int GetNumber()
		{
			return 256;
		}
	}
}