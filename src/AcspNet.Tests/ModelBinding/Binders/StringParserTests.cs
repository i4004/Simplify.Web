using AcspNet.ModelBinding.Binders;
using AcspNet.Tests.TestEntities;
using NUnit.Framework;

namespace AcspNet.Tests.ModelBinding.Binders
{
	[TestFixture]
	public class StringParserTests
	{
		[Test]
		public void ParseBool_True_True()
		{
			Assert.IsTrue(StringToSpecifiedObjectParser.ParseBool("True"));
		}

		[Test]
		public void ParseBool_False_False()
		{
			Assert.IsFalse(StringToSpecifiedObjectParser.ParseBool("False"));
		}

		[Test]
		public void ParseBool_on_True()
		{
			Assert.IsTrue(StringToSpecifiedObjectParser.ParseBool("on"));
		}

		[Test]
		public void ParseBool_true_True()
		{
			Assert.IsTrue(StringToSpecifiedObjectParser.ParseBool("true"));
		}

		[Test]
		public void ParseEnum_True_True()
		{
			Assert.AreEqual(TestEnum.Value1, StringToSpecifiedObjectParser.ParseEnum("1", typeof(TestEnum)));
		}
	}
}