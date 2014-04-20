using Moq;
using NUnit.Framework;
using Simplify.Templates;

namespace AcspNet.Tests
{
	[TestFixture]
	public class DataCollectorTests
	{
		[Test]
		public void Add_DifferentData_InsertedCorrectly()
		{
			var st = new Mock<IStringTable>();
			st.Setup(x => x[It.Is<string>(c => c == "Foo3")]).Returns("Foo 3 text");

			var dc = new DataCollector("MainContent", "Title", st.Object);

			dc.Add(null, null);

			Assert.AreEqual(0, dc.Items.Count);
			Assert.IsFalse(dc.IsDataExist("Foo"));

			dc.Add("Foo", null);

			Assert.IsTrue(dc.IsDataExist("Foo"));
			Assert.AreEqual(1, dc.Items.Count);

			dc.Add("Foo", "val");
			Assert.AreEqual(1, dc.Items.Count);

			dc.Add("Foo2", "val");

			Assert.AreEqual(2, dc.Items.Count);
			Assert.IsTrue(dc.IsDataExist("Foo2"));

			dc.AddSt("Foo3");

			Assert.AreEqual(3, dc.Items.Count);
			Assert.AreEqual("Foo 3 text", dc.Items["MainContent"]);

			dc.Add("bar");

			Assert.AreEqual(3, dc.Items.Count);
			Assert.AreEqual("Foo 3 textbar", dc["MainContent"]);

			dc.AddTitle("FooTitle");

			Assert.AreEqual(4, dc.Items.Count);
			Assert.AreEqual("FooTitle", dc.Items["Title"]);

			Assert.IsFalse(dc.IsDataExist("Not"));

			dc.AddTitleSt("Foo3");
			Assert.AreEqual(4, dc.Items.Count);
			Assert.AreEqual("FooTitleFoo 3 text", dc.Items["Title"]);

			dc.Add(new Template("Hello!", false));

			Assert.AreEqual(4, dc.Items.Count);
			Assert.AreEqual("Foo 3 textbarHello!", dc.Items["MainContent"]);

			dc.Add((ITemplate)null);

			Assert.AreEqual(4, dc.Items.Count);
			Assert.AreEqual("Foo 3 textbarHello!", dc.Items["MainContent"]);
		}
	}
}
