using NUnit.Framework;

namespace AcspNet.Tests.Extensions.Executable
{
	[RunType(RunType.MainPage)]
	public class MainPage : ExecExtension
	{
		public override void Invoke()
		{
			Assert.IsNotNull(Manager);
			Assert.IsNotNull(DataCollector);
			Assert.IsNotNull(ExtensionsDataLoader);
			Assert.IsNotNull(Environment);
			Assert.IsNotNull(StringTable);
			Assert.IsNotNull(TemplateFactory);
			Assert.IsNotNull(Html);
			Assert.IsNotNull(Html.ListsGenerator);
			Assert.IsNotNull(Html.MessageBox); 

			Assert.AreEqual("", Manager.CurrentAction);
			Assert.AreEqual("", Manager.CurrentMode);
			Assert.AreEqual("", Manager.CurrentID);

			Assert.AreEqual("", Manager.GetActionModeUrl());
		}
	}
}