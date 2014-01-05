using System;

using AcspNet.Html;

using NUnit.Framework;

namespace AcspNet.Tests.Extensions.Executable
{
	[Action("messageBoxTests")]
	[Priority(2)]
	public class MessageBoxTests : ExecExtension
	{
		public override void Invoke()
		{
			Assert.Throws<ArgumentNullException>(() => Html.MessageBox.Show(null));
			Assert.Throws<ArgumentNullException>(() => Html.MessageBox.Show("test", MessageBoxStatus.Information, null));

			Html.MessageBox.Show("1", MessageBoxStatus.Ok, "2");

			Assert.IsTrue(DataCollector.IsDataExist(Environment.MainContentVariableName));
			Assert.AreEqual("21", DataCollector.Items[Environment.MainContentVariableName]);
			Assert.AreEqual("2", DataCollector.Items[Environment.TitleVariableName]);

			Html.MessageBox.Show("3", MessageBoxStatus.Information, "4");

			Assert.IsTrue(DataCollector.IsDataExist(Environment.MainContentVariableName));
			Assert.AreEqual("2143", DataCollector.Items[Environment.MainContentVariableName]);
			Assert.AreEqual("24", DataCollector.Items[Environment.TitleVariableName]);

			Html.MessageBox.ShowSt("SiteTitle", MessageBoxStatus.Error, "6");

			Assert.IsTrue(DataCollector.IsDataExist(Environment.MainContentVariableName));
			Assert.AreEqual("21436Your site title!", DataCollector.Items[Environment.MainContentVariableName]);
			Assert.AreEqual("246", DataCollector.Items[Environment.TitleVariableName]);

			Assert.Throws<ArgumentNullException>(() => Html.MessageBox.GetInline(null));
			Assert.AreEqual("test", Html.MessageBox.GetInline("test"));
			Assert.AreEqual("test", Html.MessageBox.GetInline("test", MessageBoxStatus.Ok));
			Assert.AreEqual("Your site title!", Html.MessageBox.GetInlineSt("SiteTitle", MessageBoxStatus.Error));
		}
	}
}