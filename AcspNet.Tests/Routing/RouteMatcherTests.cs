using AcspNet.Routing;
using NUnit.Framework;

namespace AcspNet.Tests.Routing
{
	[TestFixture]
	public class RouteMatcherTests
	{
		private IRouteMatcher _matcher;
		[SetUp]
		public void Initialize()
		{
			_matcher = new RouteMatcher();
		}
		
		[Test]
		public void Match_SourceEmptyNull_False()
		{
			// Act
			var result = _matcher.Match(null, "/test");
			var result2 = _matcher.Match("", "/test");

			// Assert
			Assert.IsFalse(result.Success);
			Assert.IsFalse(result2.Success);
		}

		[Test]
		public void Match_EmptyNullString_False()
		{
			// Act
			var result = _matcher.Match("/test", null);
			var result2 = _matcher.Match("/test", "");

			// Assert
			Assert.IsFalse(result.Success);
			Assert.IsFalse(result2.Success);
		}

		[Test]
		public void Match_Root_True()
		{
			// Act
			var result = _matcher.Match("/", "/");

			// Assert
			Assert.IsTrue(result.Success);
		}

		[Test]
		public void Match_SingleAction_True()
		{
			// Act
			var result = _matcher.Match("/test", "/test");

			// Assert
			Assert.IsTrue(result.Success);
		}

		[Test]
		public void Match_SingleActionSlashInTheEnd_False()
		{
			// Act
			var result = _matcher.Match("/test", "/test/");

			// Assert
			Assert.IsFalse(result.Success);
		}

		[Test]
		public void Match_SingleActionWithoutSlash_True()
		{
			// Act
			var result = _matcher.Match("/test", "test");

			// Assert
			Assert.IsTrue(result.Success);
		}

		[Test]
		public void Match_MultipleActions_True()
		{
			// Act
			var result = _matcher.Match("/foo/bar", "/foo/bar");

			// Assert
			Assert.IsTrue(result.Success);
		}

		[Test]
		public void Match_ActionAndParameter_TrueValueParsed()
		{
			// Act
			var result = _matcher.Match("/user/testuser", "/user/{userName}");

			// Assert
			Assert.IsTrue(result.Success);
			Assert.AreEqual("testuser", result.Value);
		}

		[Test]
		public void Match_ActionAndTwoParameter_False()
		{
			// Act
			var result = _matcher.Match("/user/testuser", "/foo/{test}/{userName}");

			// Assert
			Assert.IsFalse(result.Success);
		}

		[Test]
		public void Match_ActionAndIntParameter_TrueValueParsed()
		{
			// Act
			var result = _matcher.Match("/foo/15", "/foo/{id:int}");

			// Assert
			Assert.IsTrue(result.Success);
			Assert.AreEqual(15, result.Value);
		}

		[Test]
		public void Match_Parameter_True()
		{
			// Act
			var result = _matcher.Match("/foo", "/{name}");

			// Assert
			Assert.IsTrue(result.Success);
			Assert.AreEqual("foo", result.Value);
		}

		[Test]
		public void Match_TwoParameters_False()
		{
			// Act
			var result = _matcher.Match("/foo", "/{test}/{name}");

			// Assert
			Assert.IsFalse(result.Success);
		}

		[Test]
		public void Match_SpecialParameterSymbols_False()
		{
			// Act
			var result = _matcher.Match("/%&{sd231}6^6", "/{name}");

			// Assert
			Assert.IsFalse(result.Success);
		}

		[Test]
		public void Match_BadParameter2_False()
		{
			// Act
			var result = _matcher.Match("/foo", "/name}");

			// Assert
			Assert.IsFalse(result.Success);
		}
	}
}