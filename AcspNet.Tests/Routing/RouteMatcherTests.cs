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
		public void Match_SourceEmptyAndNull_False()
		{
			// Act
			var result = _matcher.Match(null, "/test");
			var result2 = _matcher.Match("", "/test");

			// Assert
			Assert.IsFalse(result.Success);
			Assert.IsFalse(result2.Success);
		}

		[Test]
		public void Match_EmptyStringWithNormalRoute_False()
		{
			// Act
			var result = _matcher.Match("/test", "");

			// Assert
			Assert.IsFalse(result.Success);
		}

		[Test]
		public void Match_NullStringWithNormalRoute_True()
		{
			// Act
			var result = _matcher.Match("/test", null);

			// Assert
			Assert.IsTrue(result.Success);
		}

		[Test]
		public void Match_RootWithRoot_True()
		{
			// Act
			var result = _matcher.Match("/", "/");

			// Assert
			Assert.IsTrue(result.Success);
		}

		[Test]
		public void Match_SingleActionWithSingleAction_True()
		{
			// Act
			var result = _matcher.Match("/test", "/test");

			// Assert
			Assert.IsTrue(result.Success);
		}

		[Test]
		public void Match_SingleActionWithSlashInTheEndWithSingleAction_False()
		{
			// Act
			var result = _matcher.Match("/test", "/test/");

			// Assert
			Assert.IsFalse(result.Success);
		}

		[Test]
		public void Match_SingleActionWithoutSlashWithSingleAction_True()
		{
			// Act
			var result = _matcher.Match("/test", "test");

			// Assert
			Assert.IsTrue(result.Success);
		}

		[Test]
		public void Match_MultipleActionsWithMultipleActions_True()
		{
			// Act
			var result = _matcher.Match("/foo/bar", "/foo/bar");

			// Assert
			Assert.IsTrue(result.Success);
		}

		[Test]
		public void Match_ActionAndParameterWithNormal2partsRoute_TrueValueParsed()
		{
			// Act
			var result = _matcher.Match("/user/testuser", "/user/{userName}");

			// Assert
			Assert.IsTrue(result.Success);
			Assert.AreEqual("testuser", result.Value);
		}

		[Test]
		public void Match_ActionAndTwoParameterWithNormal2partsRoute_False()
		{
			// Act
			var result = _matcher.Match("/user/testuser", "/foo/{test}/{userName}");

			// Assert
			Assert.IsFalse(result.Success);
		}

		[Test]
		public void Match_ActionAndParameterWithNormal2partsRouteButNotMatched_False()
		{
			// Act
			var result = _matcher.Match("/user/testuser", "/foo/{userName}");

			// Assert
			Assert.IsFalse(result.Success);
		}

		[Test]
		public void Match_ActionAndParameterUndefinedParameterTypeWithNormal2partsRoute_TrueValueAsString()
		{
			// Act
			var result = _matcher.Match("/user/testuser", "/user/{userName:zxc}");

			// Assert
			Assert.IsTrue(result.Success);
			Assert.AreEqual("testuser", result.Value);
		}

		[Test]
		public void Match_ActionAndIntParameterWithNormal2partsRoute_TrueValueParsed()
		{
			// Act
			var result = _matcher.Match("/foo/15", "/foo/{id:int}");

			// Assert
			Assert.IsTrue(result.Success);
			Assert.AreEqual(15, result.Value);
		}

		[Test]
		public void Match_ParameterWithSingleActionRoute_True()
		{
			// Act
			var result = _matcher.Match("/foo", "/{name}");

			// Assert
			Assert.IsTrue(result.Success);
			Assert.AreEqual("foo", result.Value);
		}

		[Test]
		public void Match_TwoParametersWithSingleActionRoute_False()
		{
			// Act
			var result = _matcher.Match("/foo", "/{test}/{name}");

			// Assert
			Assert.IsFalse(result.Success);
		}

		[Test]
		public void Match_ParameterWithSpecialSymbols_False()
		{
			// Act
			var result = _matcher.Match("/%&{sd231}6^6", "/{name}");

			// Assert
			Assert.IsFalse(result.Success);
		}

		[Test] public void Match_BadParameterWithWSingleActionRoute_False()
		{
			// Act
			var result = _matcher.Match("/foo", "/name}");

			// Assert
			Assert.IsFalse(result.Success);
		}
	}
}