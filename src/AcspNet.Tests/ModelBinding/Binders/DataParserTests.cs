using AcspNet.ModelBinding.Binders;
using NUnit.Framework;

namespace AcspNet.Tests.ModelBinding.Binders
{
	[TestFixture]
	public class DataParserTests
	{
		[Test]
		public void ParseBool_True_True()
		{
			Assert.IsTrue(DataParser.ParseBool("True"));
		}

		[Test]
		public void ParseBool_False_False()
		{
			Assert.IsFalse(DataParser.ParseBool("False"));
		}

		[Test]
		public void ParseBool_on_True()
		{
			Assert.IsTrue(DataParser.ParseBool("on"));
		}

		[Test]
		public void ParseBool_true_True()
		{
			Assert.IsTrue(DataParser.ParseBool("true"));
		}
	}
}