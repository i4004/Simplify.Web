using NUnit.Framework;

namespace AcspNet.Tests.TestExtensions.Extensions.Executable
{
	[RunType(RunType.DefaultPage)]
	[Priority(-1)]
	public class ExecExtensionsBasePropertiesTest : ExecExtension
	{
		public override void Invoke()
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
			////Assert.IsNotNull(Extensions.MessagePage);
		}
	}
}