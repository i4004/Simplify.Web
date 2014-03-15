using NUnit.Framework;

namespace AcspNet.Tests.TestExtensions.Extensions.Library
{
	[Priority(-1)]
	[Version("1.0")]
	public class LibExtensionTest : LibExtension
	{
		public override void Initialize()
		{
			Assert.IsNotNull(ProcessorContoller);
			Assert.IsNotNull(Context);
			Assert.IsNotNull(DataCollector);
			Assert.IsNotNull(ExtensionsDataLoader);
			Assert.IsNotNull(Environment);
			Assert.IsNotNull(StringTable);
			Assert.IsNotNull(TemplateFactory);
			Assert.IsNotNull(Html);
			Assert.IsNotNull(Html.ListsGenerator);
			Assert.IsNotNull(Html.MessageBox);
			Assert.IsNotNull(AuthenticationModule);
			Assert.IsNotNull(Extensions);
			Assert.IsNotNull(Extensions.IdVerifier);
			Assert.IsNotNull(Extensions.Navigator);
		}
	}
}