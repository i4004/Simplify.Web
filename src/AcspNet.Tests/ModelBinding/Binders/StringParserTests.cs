using AcspNet.ModelBinding.Binders;
using NUnit.Framework;

namespace AcspNet.Tests.ModelBinding.Binders
{
	[TestFixture]
	public class StringParserTests
	{
		[Test]
		public void ParseBool_True_True()
		{
			Assert.IsTrue(StringParser.ParseBool("True"));
		}

		[Test]
		public void ParseBool_False_False()
		{
			Assert.IsFalse(StringParser.ParseBool("False"));
		}

		[Test]
		public void ParseBool_on_True()
		{
			Assert.IsTrue(StringParser.ParseBool("on"));
		}

		[Test]
		public void ParseBool_true_True()
		{
			Assert.IsTrue(StringParser.ParseBool("true"));
		}
	}
}