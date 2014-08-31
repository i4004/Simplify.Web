using System;
using AcspNet.Modules;
using AcspNet.Modules.Html;
using Moq;
using NUnit.Framework;
using Simplify.Templates;

namespace AcspNet.Tests.Modules.Html
{
	[TestFixture]
	public class MessageBoxTests
	{
		[Test]
		public void Show_NullParameters_ExceptionsThrown()
		{
			var mb = new MessageBox(null, null, null);
			Assert.Throws<ArgumentNullException>(() => mb.Show(null));
		}

		[Test]
		public void GetInline_NullParameters_ExceptionsThrown()
		{
			var mb = new MessageBox(null, null, null);
			Assert.Throws<ArgumentNullException>(() => mb.GetInline(null));
		}

		[Test]
		public void Show_OkMessageBox_GeneratedCorrectly()
		{
			var tf = new Mock<ITemplateFactory>();
			tf.Setup(x => x.Load(It.Is<string>(c => c.EndsWith("OkMessageBox.tpl")))).Returns(new Template("{Title}{Message}", false));

			var st = new Mock<IStringTable>();
			st.Setup(x => x.GetItem(It.Is<string>(c => c == "FormTitleMessageBox"))).Returns("Foo title");

			var dc = new Mock<IDataCollector>();
			dc.Setup(x => x.Add(It.IsAny<string>())).Callback((string str) => Assert.AreEqual("21", str));
			dc.Setup(x => x.AddTitle(It.IsAny<string>())).Callback((string str) => Assert.AreEqual("2", str));

			var mb = new MessageBox(tf.Object, st.Object, dc.Object);
			mb.Show("1", MessageBoxStatus.Ok, "2");

			dc.Verify(x => x.Add(It.IsAny<string>()), Times.Once);
		}

		[Test]
		public void Show_InfoMessageBox_GeneratedCorrectly()
		{
			var tf = new Mock<ITemplateFactory>();
			tf.Setup(x => x.Load(It.Is<string>(c => c.EndsWith("InfoMessageBox.tpl")))).Returns(new Template("{Title}{Message}", false));

			var st = new Mock<IStringTable>();
			st.Setup(x => x.GetItem(It.Is<string>(c => c == "FormTitleMessageBox"))).Returns("Foo title");

			var dc = new Mock<IDataCollector>();
			dc.Setup(x => x.Add(It.IsAny<string>())).Callback((string str) => Assert.AreEqual("21", str));
			dc.Setup(x => x.AddTitle(It.IsAny<string>())).Callback((string str) => Assert.AreEqual("2", str));

			var mb = new MessageBox(tf.Object, st.Object, dc.Object);
			mb.Show("1", MessageBoxStatus.Information, "2");

			dc.Verify(x => x.Add(It.IsAny<string>()), Times.Once);
		}

		[Test]
		public void ShowSt_ErrorMessageBox_GeneratedCorrectly()
		{
			var tf = new Mock<ITemplateFactory>();

			tf.Setup(x => x.Load(It.Is<string>(c => c.EndsWith("ErrorMessageBox.tpl")))).Returns(new Template("{Title}{Message}", false));

			var st = new Mock<IStringTable>();
			st.Setup(x => x.GetItem(It.Is<string>(c => c == "FormTitleMessageBox"))).Returns("Foo title");
			st.Setup(x => x.GetItem(It.Is<string>(c => c == "StTestItem"))).Returns("Foo data");

			var dc = new Mock<IDataCollector>();
			dc.Setup(x => x.Add(It.IsAny<string>())).Callback((string str) => Assert.AreEqual("2Foo data", str));
			dc.Setup(x => x.AddTitle(It.IsAny<string>())).Callback((string str) => Assert.AreEqual("2", str));

			var mb = new MessageBox(tf.Object, st.Object, dc.Object);
			mb.ShowSt("StTestItem", MessageBoxStatus.Error, "2");

			dc.Verify(x => x.Add(It.IsAny<string>()), Times.Once);
		}

		[Test]
		public void GetInlineSt_ErrorMessageBox_GeneratedCorrectly()
		{
			var tf = new Mock<ITemplateFactory>();
			tf.Setup(x => x.Load(It.Is<string>(c => c.EndsWith("InlineErrorMessageBox.tpl")))).Returns(new Template("{Message}", false));

			var st = new Mock<IStringTable>();
			st.Setup(x => x.GetItem(It.Is<string>(c => c == "StTestItem"))).Returns("Foo data");

			var dc = new Mock<IDataCollector>();

			var mb = new MessageBox(tf.Object, st.Object, dc.Object);
			Assert.AreEqual("Foo data", mb.GetInlineSt("StTestItem"));
		}

		[Test]
		public void GetInline_InfoMessageBox_GeneratedCorrectly()
		{
			var tf = new Mock<ITemplateFactory>();
			tf.Setup(x => x.Load(It.Is<string>(c => c.EndsWith("InlineInfoMessageBox.tpl")))).Returns(new Template("{Message}", false));

			var st = new Mock<IStringTable>();
			var dc = new Mock<IDataCollector>();

			var mb = new MessageBox(tf.Object, st.Object, dc.Object);
			Assert.AreEqual("Foo data", mb.GetInline("Foo data", MessageBoxStatus.Information));
		}

		[Test]
		public void GetInline_OkMessageBox_GeneratedCorrectly()
		{
			var tf = new Mock<ITemplateFactory>();
			tf.Setup(x => x.Load(It.Is<string>(c => c.EndsWith("InlineOkMessageBox.tpl")))).Returns(new Template("{Message}", false));

			var st = new Mock<IStringTable>();
			var dc = new Mock<IDataCollector>();

			var mb = new MessageBox(tf.Object, st.Object, dc.Object);
			Assert.AreEqual("Foo data", mb.GetInline("Foo data", MessageBoxStatus.Ok));
		}
	}
}