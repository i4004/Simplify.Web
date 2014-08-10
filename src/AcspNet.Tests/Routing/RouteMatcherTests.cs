using System;
using AcspNet.Routing;
using Moq;
using NUnit.Framework;

namespace AcspNet.Tests.Routing
{
	[TestFixture]
	public class RouteMatcherTests
	{
		private IRouteMatcher _matcher;
		private Mock<IControllerPathParser> _controllerPathParser;

		[SetUp]
		public void Initialize()
		{
			_controllerPathParser = new Mock<IControllerPathParser>();
			_matcher = new RouteMatcher(_controllerPathParser.Object);
		}

		[Test]
		public void Match_SourceEmptyOrNull_False()
		{
			// Act
			var result = _matcher.Match(null, "/test");
			var result2 = _matcher.Match("", "/test");

			// Assert
			Assert.IsFalse(result.Success);
			Assert.IsFalse(result2.Success);
		}

		[Test]
		public void Match_EmptyStringWithNormalRouteAnyPageController_False()
		{
			// Act
			var result = _matcher.Match("/test", "");

			// Assert
			Assert.IsFalse(result.Success);
		}

		[Test]
		public void Match_NullStringWithNormalRouteAnyPageController_True()
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
			var result = _matcher.Match("/foo/bar/test", "/foo/bar/test");

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
			Assert.AreEqual("testuser", result.RouteParameters.userName);
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
		public void Match_ActionAndParameterUndefinedParameterTypeWithNormal2partsRoute_ExceptionThrown()
		{
			// Act & Assert
			Assert.Throws<ControllerRouteException>(() => _matcher.Match("/user/testuser", "/user/{userName:zxc}"));
		}

		[Test]
		public void Match_ActionAndIntParameterWithNormal2partsRoute_TrueValueParsed()
		{
			// Act
			var result = _matcher.Match("/foo/15", "/foo/{id:int}");

			// Assert
			Assert.IsTrue(result.Success);
			Assert.AreEqual(15, result.RouteParameters.id);
		}

		[Test]
		public void Match_ParameterWithSingleActionRoute_True()
		{
			// Act
			var result = _matcher.Match("/foo", "/{name}");

			// Assert
			Assert.IsTrue(result.Success);
			Assert.AreEqual("foo", result.RouteParameters.name);
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

		[Test]
		public void Match_BadParameterWithWSingleActionRoute_ExceptionThrown()
		{
			// Act & Assert
			Assert.Throws<ControllerRouteException>(() => _matcher.Match("/foo", "/name}"));
		}

		[Test]
		public void Match_TwoParametersWithTwoActions_TrueParsed()
		{
			// Act
			var result = _matcher.Match("/foo/bar", "/{test}/{name}");

			// Assert
			Assert.IsFalse(result.Success);
			Assert.AreEqual("foo", result.RouteParameters.test);
			Assert.AreEqual("bar", result.RouteParameters.name);
		}

		[Test]
		public void Match_TwoParametersWithThreeActions_TrueParsed()
		{
			// Act
			var result = _matcher.Match("/foo/bar/15", "/foo/{name}/{id:int}");

			// Assert
			Assert.IsFalse(result.Success);
			Assert.AreEqual("bar", result.RouteParameters.test);
			Assert.AreEqual(15, result.RouteParameters.name);
		}
	}
}