using System;
using AcspNet.Html;
using Moq;
using NUnit.Framework;
using Simplify.Templates;

namespace AcspNet.Tests
{
	[TestFixture]
	public class MessageBoxTests
	{
		[Test]
		public void Show_NullParameters_ExceptionsThrown()
		{
			var mb = new MessageBox(null, null, null);
			Assert.Throws<ArgumentNullException>(() => mb.Show(null));
			Assert.Throws<ArgumentNullException>(() => mb.Show("test", MessageBoxStatus.Information, null));
		}

		[Test]
		public void Show_OkMessageBox_GeneratedCorrectly()
		{
			var tf = new Mock<ITemplateFactory>();
			tf.Setup(x => x.Load(It.Is<string>(c => c.EndsWith("OkMessageBox.tpl")))).Returns(new Template("{Title}{Message}", false));

			var st = new Mock<IStringTable>();
			st.Setup(x => x[It.Is<string>(c => c == "FormTitleMessageBox")]).Returns("Foo title");

			var dc = new Mock<IDataCollector>();
			dc.Setup(x => x.Add(It.IsAny<string>())).Callback((string str) => Assert.AreEqual("1", str));
			dc.Setup(x => x.AddTitle(It.IsAny<string>())).Callback((string str) => Assert.AreEqual("2", str));

			var mb = new MessageBox(tf.Object, st.Object, dc.Object);
			mb.Show("1", MessageBoxStatus.Ok, "2");

			dc.Verify(x => x.Add(It.IsAny<string>()), Times.Once);
			//Assert.IsTrue(DataCollector.IsDataExist(Environment.MainContentVariableName));
			//Assert.AreEqual("21", DataCollector.Items[Environment.MainContentVariableName]);
			//Assert.AreEqual("2", DataCollector.Items[Environment.TitleVariableName]);

			//Html.MessageBox.Show("3", MessageBoxStatus.Information, "4");

			//Assert.IsTrue(DataCollector.IsDataExist(Environment.MainContentVariableName));
			//Assert.AreEqual("2143", DataCollector.Items[Environment.MainContentVariableName]);
			//Assert.AreEqual("24", DataCollector.Items[Environment.TitleVariableName]);

			//Html.MessageBox.ShowSt("SiteTitle", MessageBoxStatus.Error, "6");

			//Assert.IsTrue(DataCollector.IsDataExist(Environment.MainContentVariableName));
			//Assert.AreEqual("21436Your site title!", DataCollector.Items[Environment.MainContentVariableName]);
			//Assert.AreEqual("246", DataCollector.Items[Environment.TitleVariableName]);

			//Assert.Throws<ArgumentNullException>(() => Html.MessageBox.GetInline(null));
			//Assert.AreEqual("test", Html.MessageBox.GetInline("test"));
			//Assert.AreEqual("test", Html.MessageBox.GetInline("test", MessageBoxStatus.Ok));
			//Assert.AreEqual("Your site title!", Html.MessageBox.GetInlineSt("SiteTitle", MessageBoxStatus.Error));			
		}
	}
}