using System;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests
{
	[TestFixture]
	public class DataCollectorTests
	{
		[Test]
		public void Constructor_NullsPassed_ArgumentNullExceptionsThrown()
		{
			Assert.Throws<ArgumentNullException>(() => new DataCollector(null, null, null));
			Assert.Throws<ArgumentNullException>(() => new DataCollector("Test", null, null));
			Assert.Throws<ArgumentNullException>(() => new DataCollector("Test", "Test", null));
		}

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
			Assert.AreEqual("Foo 3 textbar", dc.Items["MainContent"]);

			dc.AddTitle("FooTitle");

			Assert.AreEqual(4, dc.Items.Count);
			Assert.AreEqual("FooTitle", dc.Items["Title"]);

			Assert.IsFalse(dc.IsDataExist("Not"));

			dc.AddTitleSt("Foo3");
			Assert.AreEqual(4, dc.Items.Count);
			Assert.AreEqual("FooTitleFoo 3 text", dc.Items["Title"]);
		}
	}
}
